//    This file is part of qBoSlider.

//    qBoSlider is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    qBoSlider is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with qBoSlider.  If not, see<https://www.gnu.org/licenses/>.

using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

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
        protected virtual void SaveWidgetZoneAcl(WidgetZoneModel model, WidgetZone widgetZone)
        {
            //mark entity like subject to ACL
            widgetZone.SubjectToAcl = model.SelectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(widgetZone);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);

            //processing customer roles dependencies
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //add new role dependecy
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(widgetZone, customerRole.Id);
                }
                else
                {
                    //remove role dependency
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        _aclService.DeleteAclRecord(aclRecordToDelete);
                }
            }
        }

        /// <summary>
        /// Save widget zone store mappings
        /// </summary>
        /// <param name="model">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        protected virtual void SaveWidgetZoneStoreMappings(WidgetZoneModel model, WidgetZone widgetZone)
        {
            //mark entity like limited to stores
            widgetZone.LimitedToStores = model.SelectedStoreIds.Any();

            var existingStoreMappings = _storeMappingService.GetStoreMappings(widgetZone);
            var allStores = _storeService.GetAllStores();

            //process store mappings
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //add new store mapping
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(widgetZone, store.Id);
                }
                else
                {
                    //remove store mapping
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        _storeMappingService.DeleteStoreMapping(storeMappingToDelete);
                }
            }
        }

        #endregion

        #region Widget Zone

        public virtual IActionResult List()
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new WidgetZoneSearchModel();
            model.SetGridPageSize();

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/List.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult List(WidgetZoneSearchModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var gridModel = _widgetZoneModelFactory.PrepareWidgetZonePagedListModel(searchModel);

            return Json(gridModel);
        }

        public virtual IActionResult Create()
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new WidgetZoneModel();

            //prepare widget zone model
            _widgetZoneModelFactory.PrepareWidgetZoneModel(model, null);
            //prepare widget zone ACL
            _widgetZoneModelFactory.PrepareAclModel(model, null);
            //prepare widget zone store mappings
            _widgetZoneModelFactory.PrepareStoreMappings(model, null);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Create.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(WidgetZoneModel model, bool continueEditing)
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //return view if model state isn't valid
            if (!ModelState.IsValid)
            {
                //prepare model values
                model = _widgetZoneModelFactory.PrepareWidgetZoneModel(model, null);
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
                SlideDuration = model.SlideDuration,
                SlideSpacing = model.SlideSpacing,
                //put widget zone properties
                Name = model.Name,
                SystemName = model.SystemName,
                LimitedToStores = model.LimitedToStores,
                SubjectToAcl = model.SubjectToAcl,
                Published = model.Published,
            };

            //insert new widget zone
            _widgetZoneService.InsertWidgetZone(widgetZone);

            //save acl
            SaveWidgetZoneAcl(model, widgetZone);

            //save store mappings
            SaveWidgetZoneStoreMappings(model, widgetZone);

            //notify admin
            _notificationService.SuccessNotification(_localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.CreatedSuccessfully"));

            //redirect on widget zone list page if customer don't want's to contiu editing
            if(!continueEditing)
                return RedirectToAction("List");

            return RedirectToAction("Edit", new { id = widgetZone.Id });
        }

        public virtual IActionResult Edit(int id)
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = _widgetZoneService.GetWidgetZoneById(id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{id}' isn't exist.");

            var model = _widgetZoneModelFactory.PrepareWidgetZoneModel(new WidgetZoneModel(), widgetZone);

            //prepare widget zone ACL
            _widgetZoneModelFactory.PrepareAclModel(model, widgetZone);
            //prepare widget zone store mappings
            _widgetZoneModelFactory.PrepareStoreMappings(model, widgetZone);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/Edit.cshtml", model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(WidgetZoneModel model, bool continueEditing)
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = _widgetZoneService.GetWidgetZoneById(model.Id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{model.Id}' isn't exist.");

            if (!ModelState.IsValid)
            {
                //prepare widget zone ACL
                _widgetZoneModelFactory.PrepareAclModel(model, widgetZone);
                //prepare widget zone store mappings
                _widgetZoneModelFactory.PrepareStoreMappings(model, widgetZone);

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
            widgetZone.SlideDuration = model.SlideDuration;
            widgetZone.SlideSpacing = model.SlideSpacing;

            //update entity
            _widgetZoneService.UpdateWidgetZone(widgetZone);

            //save acl
            SaveWidgetZoneAcl(model, widgetZone);

            //save store mappings
            SaveWidgetZoneStoreMappings(model, widgetZone);

            //notify admin
            _notificationService.SuccessNotification(_localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.ChangedSuccessfully"));

            //redirect on widget zone list page if customer don't want's to contiu editing
            if (!continueEditing)
                return RedirectToAction("List");

            return Edit(model.Id);
        }

        public virtual IActionResult Delete(int id)
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //get widget zone entity and validate entity existing
            var widgetZone = _widgetZoneService.GetWidgetZoneById(id);
            if (widgetZone == null)
                throw new Exception($"Widget zone by '{id}' isn't exist.");

            //delete entity
            _widgetZoneService.DeleteWidgetZone(widgetZone);

            //notify admin
            _notificationService.SuccessNotification(_localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.DeletedSuccessfully"));

            return RedirectToAction("List");
        }

        #endregion

        #region Slide list

        [HttpPost]
        public virtual IActionResult SlideList(WidgetZoneSlideSearchModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var gridModel = _widgetZoneSlideModelFactory.PrepareSlidePagedListModel(searchModel);

            return Json(gridModel);
        }

        public virtual IActionResult EditWidgetZoneSlidePopup(int id)
        {
            //return access denied result if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var widgetZoneSlide = _widgetZoneSlideService.GetWidgetZoneSlide(id);
            if (widgetZoneSlide == null)
                throw new Exception($"Widget zone slide with '{id}' id aren't exist");

            var model = _widgetZoneSlideModelFactory.PrepareEditWidgetZoneSlideModel(widgetZoneSlide);
            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/EditWidgetZoneSlidePopup.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult EditWidgetZoneSlidePopup(WidgetZoneSlideModel model)
        {
            //return access denied result if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var widgetZoneSlide = _widgetZoneSlideService.GetWidgetZoneSlide(model.Id);
            if (widgetZoneSlide == null)
                throw new Exception($"Widget zone slide with '{model.Id}' id aren't exist");

            //apply values
            widgetZoneSlide.DisplayOrder = model.DisplayOrder;
            widgetZoneSlide.OverrideDescription = model.OverrideDescription;

            //save localizaed values
            foreach(var locale in model.Locales)
                _localizedEntityService.SaveLocalizedValue(widgetZoneSlide, x => x.OverrideDescription, locale.OverrideDescription, locale.LanguageId);

            //update widget zone slide
            _widgetZoneSlideService.UpdateWidgetZoneSlide(widgetZoneSlide);

            //close popup window
            ViewBag.RefreshPage = true;

            return EditWidgetZoneSlidePopup(model.Id);
        }

        #endregion
    }
}
