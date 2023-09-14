//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Nop.Core.Infrastructure;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Nop.Plugin.Widgets.qBoSlider
{
    public abstract class BaseBaroquePlugin : BasePlugin
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Properties

        public FileInfo _originalAssemblyFile { get; private set; }

        #endregion

        #region Constructor

        public BaseBaroquePlugin()
        {
            //get plugin descriptor
            var pluginsInfo = Singleton<IPluginsInfo>.Instance;
            var descriptor = pluginsInfo.PluginDescriptors.FirstOrDefault(x => x.pluginDescriptor.PluginType == this.GetType());
            //intialize varialbes
            this._localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            this._languageService = EngineContext.Current.Resolve<ILanguageService>();
            this._originalAssemblyFile = new FileInfo(descriptor.pluginDescriptor.OriginalAssemblyFile);
        }

        #endregion

        #region Utilites

            /// <summary>
            /// Generate localization file path by language culture
            /// </summary>
            /// <param name="culture">Language culture. For example 'en-US'</param>
            /// <returns>Localization Xml file path</returns>
            protected string GenerateLocalizationXmlFilePathByCulture(string culture = "en-US")
            {
                string fileName = string.Format("localization.{0}.xml", culture);
                string contentDirectoryPath = $"{_originalAssemblyFile.DirectoryName}{Path.DirectorySeparatorChar}Content{Path.DirectorySeparatorChar}";

            return $"{contentDirectoryPath}{fileName}";
        }

        /// <summary>
        /// Install available plugin localizations
        /// </summary>
        /// <param name="updateExistingResources">A value indicating whether to update existing resources</param>
        protected virtual async Task InstallLocalizationAsync(bool updateExistingResources = true)
        {
            var allLanguages = await _languageService.GetAllLanguagesAsync();
            var language = allLanguages.FirstOrDefault();

            //if shop have no available languages method generate exception
            if (language == null)
                throw new Exception("Your store has no available language.");

            foreach (var l in allLanguages)
            {
                //check file existing
                bool isLocalizationFileExist = File.Exists(GenerateLocalizationXmlFilePathByCulture(l.LanguageCulture));

                //get localization file path. If no one localization have no appropriate xml file method will install only default english localization
                string localizationPath = isLocalizationFileExist ? GenerateLocalizationXmlFilePathByCulture(l.LanguageCulture) : GenerateLocalizationXmlFilePathByCulture();

                //if file exists it's imports in database
                var localizationFile = File.ReadAllBytes(localizationPath);
                using (var stream = new MemoryStream(localizationFile))
                {
                    using (var sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        await _localizationService.ImportResourcesFromXmlAsync(l, sr, updateExistingResources);
                    }
                }
            }
        }

        /// <summary>
        /// Uninstall plugin localization
        /// </summary>
        protected virtual async Task UninstallLocalizationAsync()
        {
            var localizationPath = GenerateLocalizationXmlFilePathByCulture();

            //if standart english localization file isn't exist method generate exception
            if (!File.Exists(localizationPath))
                throw new Exception("File isn't exist.");

            var localizationFile = File.ReadAllBytes(localizationPath);

            using (var stream = new MemoryStream(localizationFile))
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    var result = await sr.ReadToEndAsync();
                    var xLang = new XmlDocument();
                    xLang.LoadXml(result);

                    //get localization keys
                    var xNodeList = xLang.SelectNodes("Language/LocaleResource");
                    foreach (XmlNode elem in xNodeList)
                        if (elem.Name == "LocaleResource")
                        {
                            var localResource = elem.Attributes["Name"].Value;
                            await _localizationService.DeleteLocaleResourceAsync(localResource);
                        }
                }
            }
        }

        #endregion

        #region Methods

        public override async Task InstallAsync()
        {

            await InstallLocalizationAsync();
            await base.InstallAsync();
        }


        public override async Task UninstallAsync()
        {
            await UninstallLocalizationAsync();
            await base.UninstallAsync();
        }

        #endregion
    }
}

