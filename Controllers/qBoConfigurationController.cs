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

using Baroque.Plugin.Widgets.qBoSlider.Service.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Infrastructure;
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
    [Area(AreaNames.ADMIN)]
    public class qBoConfigurationController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly INotificationService _notificationService;
        private readonly ISettingService _settingService;
        private readonly ITypeFinder _typeFinder;

        private readonly IStoreContext _storeContext;

        #endregion

        #region Constructor

        public qBoConfigurationController(ILocalizationService localizationService,
            INotificationService notificationService,
            ISettingService settingService,
            ITypeFinder typeFinder,
            IStoreContext storeContext)
        {
            _localizationService = localizationService;
            _notificationService = notificationService;
            _settingService = settingService;
            _typeFinder = typeFinder;
            _storeContext = storeContext;
        }

        #endregion

        #region Methods

        [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
        public virtual async Task<IActionResult> Configure()
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<qBoSliderSettings>(storeScope);
            var availableSliders = _typeFinder.FindClassesOfType<ISlider>();

            var model = new ConfigurationModel()
            {
                ActiveStoreScopeConfiguration = storeScope,
                AvailableSliders = availableSliders.Select(x =>
                {
                    var instance = (ISlider)EngineContext.Current.Resolve(x);
                    return new SelectListItem()
                    {
                        Value = x.FullName,
                        Text = instance.Name,
                    };
                }).ToList(),
                UseStaticCache = settings.UseStaticCache,
                SelectedDefaultSliderSystemName = settings.SelectedDefaultSliderSystemName
            };

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Configure.cshtml", model);
        }

        [HttpPost]
        [CheckPermission(StandardPermission.Configuration.MANAGE_WIDGETS)]
        public virtual async Task<IActionResult> Configure(ConfigurationModel model)
        {
            var storeScope = await _storeContext.GetActiveStoreScopeConfigurationAsync();
            var settings = await _settingService.LoadSettingAsync<qBoSliderSettings>(storeScope);

            settings.UseStaticCache = model.UseStaticCache;
            settings.SelectedDefaultSliderSystemName = model.SelectedDefaultSliderSystemName;

            await _settingService.SaveSettingAsync(settings, storeScope);

            //now clear settings cache
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return await Configure();
        }

        #endregion
    }
}
