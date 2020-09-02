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

using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Widgets.qBoSlider.Controllers
{
    /// <summary>
    /// Represents plugin configuration controller
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class qBoConfigurationController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;

        private readonly IStoreContext _storeContext;

        #endregion

        #region Constructor

        public qBoConfigurationController(ILocalizationService localizationService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods

        public virtual IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

            var model = new ConfigurationModel()
            {
                ActiveStoreScopeConfiguration = storeScope
            };

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Configure.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var settings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

            _settingService.SaveSetting(settings, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}
