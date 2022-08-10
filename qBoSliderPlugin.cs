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
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Base plugin class
    /// </summary>
    public class qBoSliderPlugin : BaseBaroquePlugin, IWidgetPlugin, IAdminMenuPlugin
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IGarbageManager _garbageManager;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideService _slideService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWebHelper _webHelper;
        private readonly IWidgetZoneService _widgetZoneService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        private readonly IStoreContext _storeContext;

        #endregion

        #region Constructor

        public qBoSliderPlugin(IAclService aclService,
            IGarbageManager garbageManager,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideService slideService,
            IStoreMappingService storeMappingService,
            IWebHelper webHelper,
            IWidgetZoneService widgetZoneService,
            IWidgetZoneSlideService widgetZoneSlideService,
            IStoreContext storeContext)
        {
            _aclService = aclService;
            _garbageManager = garbageManager;
            _permissionService = permissionService;
            _pictureService = pictureService;
            _settingService = settingService;
            _slideService = slideService;
            _storeMappingService = storeMappingService;
            _webHelper = webHelper;
            _widgetZoneService = widgetZoneService;
            _widgetZoneSlideService = widgetZoneSlideService;

            _storeContext = storeContext;
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
        public async Task<IList<string>> GetWidgetZonesAsync()
        {
            //need to prepare all available widget zone names, but we can't call widget zone service in plugin constructor
            //that's why we use here 'EngineContext'
            //var widgetZoneService = EngineContext.Current.Resolve<IWidgetZoneService>();

            //get active widget zones system names
            var activeWidgetZones = _widgetZoneService.GetWidgetZones().ToList();
            activeWidgetZones = await activeWidgetZones
                //process only authorized by ACL widget zones 
                .WhereAwait(async widgetZone => await _aclService.AuthorizeAsync(widgetZone)).ToListAsync();
            //prepare of store mapped widget zones
            var widgetZoneSystemNames = await activeWidgetZones
                .WhereAwait(async widgetZone => await _storeMappingService.AuthorizeAsync(widgetZone))
                .Select(x => x.SystemName).Distinct().ToListAsync();

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
        public async Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            //do nothing if customer can't manage plugins
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return;

            //do nothing if menu item not found
            var thirdPartPluginsNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName.Equals("Third party plugins", StringComparison.InvariantCultureIgnoreCase));
            if (thirdPartPluginsNode == null)
                return;

            var pluginNode = new SiteMapNode()
            {
                SystemName = "Baroque-qboSlider",
                Title = this.PluginDescriptor.FriendlyName,
                IconClass = "far fa-dot-circle",
                Visible = true,
                ChildNodes = new List<SiteMapNode>()
                {
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-WidgetZone",
                        Title = "Widget zones",
                        ControllerName ="qBoWidgetZone",
                        ActionName = "List",
                        IconClass = "far fa-circle",
                        Visible = true
                    },
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-Slide",
                        Title = "Slides",
                        ControllerName ="qBoSlide",
                        ActionName = "List",
                        IconClass = "far fa-circle",
                        Visible = true
                    },
                    new SiteMapNode()
                    {
                        SystemName = "Baroque-qBoSlider-Configuration",
                        Title = "Configuration",
                        ControllerName ="qBoConfiguration",
                        ActionName = "Configure",
                        IconClass = "far fa-circle",
                        Visible = true
                    }
                }
            };

            thirdPartPluginsNode.ChildNodes.Add(pluginNode);
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task InstallAsync()
        {
            //settings
            var settings = new qBoSliderSettings
            {
            };

            await _settingService.SaveSettingAsync(settings);

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
            await _widgetZoneService.InsertWidgetZoneAsync(widgetZone);

            //install simple data
            //get sample pictures path
            var sampleImagesPath = CommonHelper.DefaultFileProvider.MapPath("~/Plugins/Widgets.qBoSlider/Content/sample-images/");

            var picture1Id = (await _pictureService.InsertPictureAsync(File.ReadAllBytes(string.Format("{0}banner1.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-1")).Id;
            var slide1 = new Slide()
            {
                Name = "New comfort mouse",
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>NEW COMFORT MOUSE<br /></strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>CHOOSE FROM HUNDREDS<br /></strong></span></p>" +
                "<p style='margin-top: 5px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>OF MODELS</strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>FROM ONLY $59.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p></div>",
                PictureId = picture1Id,
                Published = true
            };
            await _slideService.InsertSlideAsync(slide1);

            var picture2Id = (await _pictureService.InsertPictureAsync(File.ReadAllBytes(string.Format("{0}banner2.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-2")).Id;
            var slide2 = new Slide()
            {
                Name = "HD PRO WEBCAM H320",
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>HD PRO WEBCAM H320<br /></strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>720P FOR TRUE HD-QUALITY<br />VIDEO CHAT<br /></strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>ONLY $79.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p></div>",
                PictureId = picture2Id,
                Published = true,
            };
            await _slideService.InsertSlideAsync(slide2);

            var picture3Id = (await _pictureService.InsertPictureAsync(File.ReadAllBytes(string.Format("{0}banner3.jpg", sampleImagesPath)), "image/pjpeg", "qboslide-3")).Id;
            var slide3 = new Slide()
            {
                Name = "Compact camera SP120",
                Description = "<div style='color: #111; margin-top: 5%; margin-left: 5%; font-size: 16pt; font-family: arial,helvetica,sans-serif;'>" +
                "<p style='margin: 0px;'><span style='font-family: tahoma,arial,helvetica,sans-serif;'><strong>COMPACT CAMERA SP120</strong></span></p>" +
                "<p style='margin-top: 10px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>20X WIDE ZOOM, 2.5 LCD, </strong></span></p>" +
                "<p style='margin-top: 5px; margin-bottom: 0px;'><span style='font-size: 12pt; font-family: tahoma,arial,helvetica,sans-serif;'><strong>720P HD VIDEO</strong></span></p>" +
                "<p style='margin-top: 25px; color: #44b4f4; font-weight: bold;'><span style='font-size: 15pt; font-family: tahoma,arial,helvetica,sans-serif;'>ONLY $159.00</span></p>" +
                "<p style='margin-top: 10px;'><span style='font-size: 10pt; padding: 5px 10px; background: none repeat scroll 0% 0% #44b4f4; color: #ffffff; border-radius: 5px; font-family: tahoma,arial,helvetica,sans-serif;'><strong>SHOP NOW</strong></span></p>" +
                "</div>",
                PictureId = picture3Id,
                Published = true
            };
            await _slideService.InsertSlideAsync(slide3);

            await _widgetZoneSlideService.InsertWidgetZoneSlideAsync(new WidgetZoneSlide()
            {
                SlideId = slide1.Id,
                WidgetZoneId = widgetZone.Id,
                DisplayOrder = 0
            });

            await _widgetZoneSlideService.InsertWidgetZoneSlideAsync(new WidgetZoneSlide()
            {
                SlideId = slide2.Id,
                WidgetZoneId = widgetZone.Id,
                DisplayOrder = 5
            });

            await _widgetZoneSlideService.InsertWidgetZoneSlideAsync(new WidgetZoneSlide()
            {
                SlideId = slide3.Id,
                WidgetZoneId = widgetZone.Id,
                DisplayOrder = 10
            });

            await base.InstallAsync();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task UninstallAsync()
        {
            var allSlides = await _slideService.GetAllSlidesAsync(storeId: _storeContext.GetCurrentStore().Id);

            //delete slide localization resources and pictures
            foreach (var slide in allSlides)
            {
                await _garbageManager.DeleteSlidePictureAsync(slide);
                await _garbageManager.DeleteSlideLocalizedValuesAsync(slide);
            }

            //settings
            await _settingService.DeleteSettingAsync<qBoSliderSettings>();

            await base.UninstallAsync();
        }

        #endregion
    }
}
