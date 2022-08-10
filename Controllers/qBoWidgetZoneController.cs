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
using Nop.Core.Caching;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Controllers
{
    /// <summary>
    /// Represents plugin widget zone controller
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class qBoWidgetZoneController : BasePluginController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISearchModelFactory _searchModelFactory;
        private readonly ISettingService _settingService;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly IWidgetZoneModelFactory _widgetZoneModelFactory;
        private readonly IWidgetZoneSlideModelFactory _widgetZoneSlideModelFactory;
        private readonly IWidgetZoneService _widgetZoneService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        #endregion

        #region Constructor

        public qBoWidgetZoneController(IAclService aclService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISearchModelFactory searchModelFactory,
            ISettingService settingService,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IWidgetZoneModelFactory widgetZoneModelFactory,
            IWidgetZoneSlideModelFactory widgetZoneSlideModelFactory,
            IWidgetZoneService widgetZoneService,
            IWidgetZoneSlideService widgetZoneSlideService)
        {
            _aclService = aclService;
            _customerService = customerService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _searchModelFactory = searchModelFactory;
            _settingService = settingService;
            _storeMappingService = storeMappingService;
            _storeService = storeService;
            _widgetZoneModelFactory = widgetZoneModelFactory;
            _widgetZoneSlideModelFactory = widgetZoneSlideModelFactory;
            _widgetZoneService = widgetZoneService;
            _widgetZoneSlideService = widgetZoneSlideService;
        }

        #endregion

        #region Utilites

        /// <summary>
        /// Save widget zone ACL
        /// </summary>
        /// <param name="model">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        protected virtual async Task SaveWidgetZoneAcl(WidgetZoneModel model, WidgetZone widgetZone)
        {
            var existingAclRecords = await _aclService.GetAclRecordsAsync(widgetZone);
            var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);

            //processing customer roles dependencies
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //add new role dependecy
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        await _aclService.InsertAclRecordAsync(widgetZone, customerRole.Id);
                }
                else
                {
                    //remove role dependency
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        await _aclService.DeleteAclRecordAsync(aclRecordToDelete);
                }
            }
        }

        /// <summary>
        /// Save widget zone store mappings
        /// </summary>
        /// <param name="model">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        protected virtual async Task SaveWidgetZoneStoreMappings(WidgetZoneModel model, WidgetZone widgetZone)
        {
            var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync(widgetZone);
            var allStores = await _storeService.GetAllStoresAsync();

            //process store mappings
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //add new store mapping
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        await _storeMappingService.InsertStoreMappingAsync(widgetZone, store.Id);
                }
                else
                {
                    //remove store mapping
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        await _storeMappingService.DeleteStoreMappingAsync(storeMappingToDelete);
                }
            }
        }

        #endregion

        #region Widget Zone

        public virtual async Task<IActionResult> List()
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new WidgetZoneSearchModel();
            model.SetGridPageSize();

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/List.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(WidgetZoneSearchModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return await AccessDeniedDataTablesJson();

            var gridModel = _widgetZoneModelFactory.PrepareWidgetZonePagedListModel(searchModel);

            return Json(gridModel);
        }

        public virtual async Task<IActionResult> Create()
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new WidgetZoneModel();

            //prepare widget zone model
            await _widgetZoneModelFactory.PrepareWidgetZoneModelAsync(model, null);
            //prepare widget zone ACL
            await _widgetZoneModelFactory.PrepareAclModelAsync(model, null);
            //prepare widget zone store mappings
            await _widgetZoneModelFactory.PrepareStoreMappingsAsync(model, null);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(WidgetZoneModel model, bool continueEditing)
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //return view if model state isn't valid
            if (!ModelState.IsValid)
            {
                //prepare model values
                model = await _widgetZoneModelFactory.PrepareWidgetZoneModelAsync(model, null);
                //prepare widget zone ACL
                await _widgetZoneModelFactory.PrepareAclModelAsync(model, null);
                //prepare widget zone store mappings
                await _widgetZoneModelFactory.PrepareStoreMappingsAsync(model, null);


                return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Create.cshtml", model);
            }

            var widgetZone = new WidgetZone()
            {
                //put slider properties
                ArrowNavigationDisplayingTypeId = model.ArrowNavigationDisplayingTypeId,
                BulletNavigationDisplayingTypeId = model.BulletNavigationDisplayingTypeId,
                AutoPlay = model.AutoPlay,
                AutoPlayInterval = model.AutoPlayInterval,
                MinDragOffsetToSlide = model.MinDragOffsetToSlide,
                MinSlideWidgetZoneWidth = model.MinSlideWidgetZoneWidth,
                MaxSlideWidgetZoneWidth = model.MaxSlideWidgetZoneWidth,
                SlideDuration = model.SlideDuration,
                SlideSpacing = model.SlideSpacing,
                //put widget zone properties
                Name = model.Name,
                SystemName = model.SystemName,
                LimitedToStores = model.SelectedStoreIds.Any(),
                SubjectToAcl = model.SelectedCustomerRoleIds.Any(),
                Published = model.Published,
            };

            //insert new widget zone
            await _widgetZoneService.InsertWidgetZoneAsync(widgetZone);

            //save acl
            await SaveWidgetZoneAcl(model, widgetZone);

            //save store mappings
            await SaveWidgetZoneStoreMappings(model, widgetZone);

            //clear cache
            await _settingService.ClearCacheAsync();

            //notify admin
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.CreatedSuccessfully"));

            //redirect on widget zone list page if customer don't want's to contiu editing
            if (!continueEditing)
                return RedirectToAction("List");

            return RedirectToAction("Edit", new { id = widgetZone.Id });
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{id}' isn't exist.");

            var model = await _widgetZoneModelFactory.PrepareWidgetZoneModelAsync(new WidgetZoneModel(), widgetZone);

            //prepare widget zone ACL
            await _widgetZoneModelFactory.PrepareAclModelAsync(model, widgetZone);
            //prepare widget zone store mappings
            await _widgetZoneModelFactory.PrepareStoreMappingsAsync(model, widgetZone);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Edit.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(WidgetZoneModel model, bool continueEditing)
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(model.Id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{model.Id}' isn't exist.");

            if (!ModelState.IsValid)
            {
                await _widgetZoneModelFactory.PrepareWidgetZoneModelAsync(model, null);
                //prepare widget zone ACL
                await _widgetZoneModelFactory.PrepareAclModelAsync(model, widgetZone);
                //prepare widget zone store mappings
                await _widgetZoneModelFactory.PrepareStoreMappingsAsync(model, widgetZone);

                return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Edit.cshtml", model);
            }

            //apply widget zone properties
            widgetZone.Name = model.Name;
            widgetZone.SystemName = model.SystemName;
            widgetZone.Published = model.Published;

            //apply widget zone slider properties
            widgetZone.ArrowNavigationDisplayingTypeId = model.ArrowNavigationDisplayingTypeId;
            widgetZone.BulletNavigationDisplayingTypeId = model.BulletNavigationDisplayingTypeId;
            widgetZone.AutoPlay = model.AutoPlay;
            widgetZone.AutoPlayInterval = model.AutoPlayInterval;
            widgetZone.MinDragOffsetToSlide = model.MinDragOffsetToSlide;
            widgetZone.MinSlideWidgetZoneWidth = model.MinSlideWidgetZoneWidth;
            widgetZone.MaxSlideWidgetZoneWidth = model.MaxSlideWidgetZoneWidth;
            widgetZone.SlideDuration = model.SlideDuration;
            widgetZone.SlideSpacing = model.SlideSpacing;
            widgetZone.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            widgetZone.LimitedToStores = model.SelectedStoreIds.Any();

            //update entity
            await _widgetZoneService.UpdateWidgetZoneAsync(widgetZone);

            //save acl
            await SaveWidgetZoneAcl(model, widgetZone);

            //save store mappings
            await SaveWidgetZoneStoreMappings (model, widgetZone);

            //clear cache
            await _settingService.ClearCacheAsync();

            //notify admin
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.ChangedSuccessfully"));

            //redirect on widget zone list page if customer don't want's to contiu editing
            if (!continueEditing)
                return RedirectToAction("List");

            return await Edit(model.Id);
        }

        public virtual async Task<IActionResult> Delete(int id)
        {
            //return access denied page if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{id}' isn't exist.");

            //delete entity
            await _widgetZoneService.DeleteWidgetZoneAsync(widgetZone);

            //notify admin
            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.DeletedSuccessfully"));

            return RedirectToAction("List");
        }

        public virtual IActionResult FindNopCommerceWidgetZoneBySystemName(string term)
        {
            var widgetZones = _widgetZoneService.GetNopCommerceWidgetZones(term, 0, 10);

            return Json(widgetZones);
        }

        public virtual async Task<IActionResult> SystemNameReservedWarning(int widgetZoneId, string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
                return Json(new { Result = string.Empty });

            var widgetZone = _widgetZoneService.GetWidgetZoneBySystemName(systemName);
            //back null if widget zone isn't exist
            if (widgetZone == null)
                return Json(new { Result = string.Empty });
            //back null if widget zone is exist and it's already open
            if (widgetZone != null && widgetZone.Id == widgetZoneId)
                return Json(new { Result = string.Empty });

            return Json(new { Result =await _localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.WidgetZoneAlreadyReserved") });
        }

        #endregion

        #region Slide List / Edit / Delete

        [HttpPost]
        public virtual async Task<IActionResult> WidgetZoneSlideList(WidgetZoneSlideSearchModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return await AccessDeniedDataTablesJson();

            var gridModel = await _widgetZoneSlideModelFactory.PrepareSlidePagedListModelAsync(searchModel);

            return Json(gridModel);
        }

        public virtual async Task<IActionResult> EditWidgetZoneSlidePopup(int id)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var widgetZoneSlide = await _widgetZoneSlideService.GetWidgetZoneSlideAsync(id);
            if (widgetZoneSlide == null)
                throw new Exception($"Widget zone slide with '{id}' id aren't exist");

            var model = await _widgetZoneSlideModelFactory.PrepareEditWidgetZoneSlideModelAsync(widgetZoneSlide);
            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/EditWidgetZoneSlidePopup.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> EditWidgetZoneSlidePopup(WidgetZoneSlideModel model)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var widgetZoneSlide = await _widgetZoneSlideService.GetWidgetZoneSlideAsync(model.Id);
            if (widgetZoneSlide == null)
                throw new Exception($"Widget zone slide with '{model.Id}' id aren't exist");

            //apply values
            widgetZoneSlide.DisplayOrder = model.DisplayOrder;
            widgetZoneSlide.OverrideDescription = model.OverrideDescription;

            //save localizaed values
            foreach (var locale in model.Locales)
                await _localizedEntityService.SaveLocalizedValueAsync(widgetZoneSlide, x => x.OverrideDescription, locale.OverrideDescription, locale.LanguageId);

            //update widget zone slide
            await _widgetZoneSlideService.UpdateWidgetZoneSlideAsync(widgetZoneSlide);

            //close popup window
            ViewBag.RefreshPage = true;

            return await EditWidgetZoneSlidePopup(model.Id);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSlide(int id)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return await AccessDeniedDataTablesJson();

            var widgetZoneSlide = await _widgetZoneSlideService.GetWidgetZoneSlideAsync(id);

            if (widgetZoneSlide == null)
                throw new Exception($"Can't delete widget zone slide, because entity by id '{id}' isn't exist.");

            await _widgetZoneSlideService.DeleteWidgetZoneSlideAsync(widgetZoneSlide);

            return new NullJsonResult();
        }

        #endregion

        #region Add slide to widget zone

        public virtual async Task<IActionResult> AddWidgetZoneSlidePopup(int widgetZoneId)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new AddWidgetZoneSlideModel()
            {
                WidgetZoneId = widgetZoneId
            };
            await _searchModelFactory.PrepareSlideSearchModelAsync(model);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/AddWidgetZoneSlidePopup.cshtml", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddWidgetZoneSlidePopup(AddWidgetZoneSlideModel model)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(model.WidgetZoneId);
            if (widgetZone == null)
                throw new Exception($"Widget zone by id '{model.WidgetZoneId}' aren't exist.");

            //calculate maximum dispay order to add slide in end of list
            var widgetZoneSlides = _widgetZoneSlideService.GetWidgetZoneSlides(model.WidgetZoneId);
            var displayOrder = widgetZoneSlides.Any() ? widgetZoneSlides.Max(x => x.DisplayOrder) : 0;

            foreach (var slideId in model.SelecetedSlideIds)
                await _widgetZoneSlideService.InsertWidgetZoneSlideAsync(new WidgetZoneSlide()
                {
                    SlideId = slideId,
                    WidgetZoneId = model.WidgetZoneId,
                    DisplayOrder = ++displayOrder
                });

            ViewBag.RefreshPage = true;
            return await AddWidgetZoneSlidePopup(model.WidgetZoneId);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddSlideList(AddWidgetZoneSlideModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return await AccessDeniedDataTablesJson();

            var gridModel = await _widgetZoneSlideModelFactory.PrepareAddWidgetZoneSlideModelAsync(searchModel);

            return Json(gridModel);
        }

        #endregion
    }
}
