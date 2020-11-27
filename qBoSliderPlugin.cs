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

using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Base plugin class
    /// </summary>
    public class qBoSliderPlugin : BaseBaroquePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly qBoSliderContext _sliderContext;

        private readonly IAclService _aclService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWebHelper _webHelper;

        private readonly IStoreContext _storeContext;

        #endregion

        #region Constructor

        public qBoSliderPlugin(qBoSliderContext sliderContext,
            IAclService aclService,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            IStoreMappingService storeMappingService,
            IWebHelper webHelper,
            IStoreContext storeContext)
        {
            this._sliderContext = sliderContext;

            this._aclService = aclService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._storeMappingService = storeMappingService;

            this._webHelper = webHelper;

            this._storeContext = storeContext;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/qBoConfiguration/Configure";
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            //need to prepare all available widget zone names, but we can't call widget zone service in plugin constructor
            //that's why we use here 'EngineContext'
            var widgetZoneService = EngineContext.Current.Resolve<IWidgetZoneService>();

            //get active widget zones system names
            var activeWidgetZones = widgetZoneService.GetWidgetZones();
            var widgetZoneSystemNames = activeWidgetZones
                //process only authorized widget zones 
                .Where(widgetZone => _aclService.Authorize(widgetZone) && _storeMappingService.Authorize(widgetZone))
                .Select(x => x.SystemName).Distinct().ToList();

            return widgetZoneSystemNames;
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "Baroque.qBoSlider.PublicInfo";
        }

        /// <summary>
        /// Manage sitemap. You can use "SystemName" of menu items to manage existing sitemap or add a new menu item.
        /// </summary>
        /// <param name="rootNode">Root node of the sitemap.</param>
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            //do nothing if customer can't manage plugins
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return;

            //do nothing if menu item not found
            var thirdPartPluginsNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName.Equals("Third party plugins", StringComparison.InvariantCultureIgnoreCase));
            if (thirdPartPluginsNode == null)
                return;

            var pluginNode = new SiteMapNode()
            {
                SystemName = "Baroque-qboSlider",
                Title = this.PluginDescriptor.FriendlyName,
                IconClass = "fa-dot-circle-o",
                Visible = true,
                ChildNodes = new List<SiteMapNode>()
                {
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-WidgetZone",
                        Title = "Widget zones",
                        ControllerName ="qBoWidgetZone",
                        ActionName = "List",
                        IconClass = "fa fa-genderless",
                        Visible = true
                    },
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-Slide",
                        Title = "Slides",
                        ControllerName ="qBoSlide",
                        ActionName = "List",
                        IconClass = "fa fa-genderless",
                        Visible = true
                    },
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-Configuration",
                        Title = "Configuration",
                        ControllerName ="qBoConfiguration",
                        ActionName = "Configure",
                        IconClass = "fa fa-genderless",
                        Visible = true
                    }
                }
            };

            thirdPartPluginsNode.ChildNodes.Add(pluginNode);
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //install special slide table
            _sliderContext.Install();

            //settings
            var settings = new qBoSliderSettings
            {
            };

            _settingService.SaveSetting(settings);

            var widgetContext = _sliderContext.Set<WidgetZone>();
            var widgetZone = new WidgetZone()
            {
                AutoPlay = true,
                AutoPlayInterval = 3000,
                SlideDuration = 500,
                MinDragOffsetToSlide = 20,
                MinSlideWidgetZoneWidth = 200,
                MaxSlideWidgetZoneWidth = 1920,
                SlideSpacing = 0,
                BulletNavigationDisplayingTypeId = 2,
                ArrowNavigationDisplayingTypeId = 1,
                Name = "Main homepage slider",
                SystemName = "home_page_top",
                Published = true
            };
            widgetContext.Add(widgetZone);

            //install simple data
            //get sample pictures path
            var sampleImagesPath = CommonHelper.DefaultFileProvider.MapPath("~/Plugins/Widgets.qBoSlider/Content/sample-images/");
            var slides = _sliderContext.Set<Slide>();

            var picture1 = _pictureService.InsertPicture(File.ReadAllBytes(string.Format("{0}banner1.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-1").Id;
            var slide1 = new Slide()
            {
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>NEW COMFORT MOUSE<br /></strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>CHOOSE FROM HUNDREDS<br /></strong></span></p>" +
                "<p style='margin-top: 5px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>OF MODELS</strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>FROM ONLY $59.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p></div>",
                PictureId = picture1,
                Published = true
            };
            slides.Add(slide1);

            var picture2 = _pictureService.InsertPicture(File.ReadAllBytes(string.Format("{0}banner2.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-2").Id;
            var slide2 = new Slide()
            {
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>HD PRO WEBCAM H320<br /></strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>720P FOR TRUE HD-QUALITY<br />VIDEO CHAT<br /></strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>ONLY $79.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p></div>",
                PictureId = picture2,
                Published = true,
            };
            slides.Add(slide2);

            var picture3 = _pictureService.InsertPicture(File.ReadAllBytes(string.Format("{0}banner3.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-3").Id;
            var slide3 = new Slide()
            {
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>COMPACT CAMERA SP120</strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>20X WIDE ZOOM, 2.5 LCD, </strong></span></p>" +
                "<p style='margin-top: 5px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>720P HD VIDEO</strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>ONLY $159.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p>" +
                "</div>",
                PictureId = picture3,
                Published = true
            };
            slides.Add(slide3);

            var widgetSlidesContext = _sliderContext.Set<WidgetZoneSlide>();
            widgetSlidesContext.Add(new WidgetZoneSlide()
            {
                Slide = slide1,
                WidgetZone = widgetZone,
                DisplayOrder = 0
            });

            widgetSlidesContext.Add(new WidgetZoneSlide()
            {
                Slide = slide2,
                WidgetZone = widgetZone,
                DisplayOrder = 5
            });

            widgetSlidesContext.Add(new WidgetZoneSlide()
            {
                Slide = slide3,
                WidgetZone = widgetZone,
                DisplayOrder = 10
            });

            _sliderContext.SaveChanges();
            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            var sliderService = EngineContext.Current.Resolve<ISlideService>();
            var allSlides = sliderService.GetAllSlides(storeId: _storeContext.CurrentStore.Id);

            //settings
            _settingService.DeleteSetting<qBoSliderSettings>();

            //uninstall slider table
            _sliderContext.Uninstall();

            base.Uninstall();
        }

        #endregion
    }
}
