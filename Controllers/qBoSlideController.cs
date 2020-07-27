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
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
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
    /// Represents plugin slide controller
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class qBoSlideController : BasePluginController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly IGarbageManager _garbageManager;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideModelFactory _slideModelFactory;
        private readonly ISlideWidgetZoneModelFactory _slideWidgetZoneModelFactory;
        private readonly ISlideService _slideService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public qBoSlideController(IAclService aclService,
            ICustomerService customerService,
            IGarbageManager garbageManager,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideModelFactory slideModelFactory,
            ISlideWidgetZoneModelFactory slideWidgetZoneModelFactory,
            ISlideService slideService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            ForseDefaultCulture();

            this._aclService = aclService;
            this._customerService = customerService;
            this._garbageManager = garbageManager;            
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._notificationService = notificationService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._slideModelFactory = slideModelFactory;
            this._slideWidgetZoneModelFactory = slideWidgetZoneModelFactory;
            this._slideService = slideService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Utilites

        protected virtual void ForseDefaultCulture()
        {
            CommonHelper.SetTelerikCulture();
        }

        protected virtual void UpdateSlideLocales(Slide slide, SlideModel model)
        {
            foreach (var localized in model.Locales)
            {
                var pictureId = _localizationService.GetLocalized(slide, x => x.PictureId, localized.LanguageId, false, false);
                _localizedEntityService.SaveLocalizedValue(slide, x => x.Description,
                    localized.Description,
                    localized.LanguageId);
                _localizedEntityService.SaveLocalizedValue(slide, x => x.HyperlinkAddress,
                    localized.Hyperlink,
                    localized.LanguageId);
                if (localized.PictureId > 0)
                    _localizedEntityService.SaveLocalizedValue(slide, x => x.PictureId,
                        localized.PictureId,
                        localized.LanguageId);
                else
                    if (pictureId.HasValue && localized.PictureId == 0)
                    _localizedEntityService.SaveLocalizedValue(slide, x => x.PictureId,
                        null,
                        localized.LanguageId);

                //if localized picture was changed
                //plugin remove old picture from database, because it will not needs in future
                if (pictureId != localized.PictureId)
                {
                    var picture = _pictureService.GetPictureById(pictureId.GetValueOrDefault(0));
                    if (picture != null)
                        _pictureService.DeletePicture(picture);
                }
            }
        }

        protected virtual void UpdateStoreMapping(Slide slide, SlideModel model)
        {
            var allStores = _storeService.GetAllStores();
            var limitedToStores = _storeMappingService.GetStoreMappings(slide);

            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds != null && model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store mapping
                    if (!limitedToStores.Any(x => x.StoreId == store.Id))
                        _storeMappingService.InsertStoreMapping(slide, store.Id);
                }
                else
                {
                    //delete mapping
                    var storeMapping = limitedToStores.FirstOrDefault(x => x.StoreId == store.Id);
                    if (storeMapping != null)
                        _storeMappingService.DeleteStoreMapping(storeMapping);
                }
            }
        }

        protected virtual void SaveCustomerRolesAcl(Slide slide, SlideModel model)
        {
            slide.SubjectToAcl = model.SelectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(slide);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(slide, customerRole.Id);
                }
                else
                {
                    //remove role
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        _aclService.DeleteAclRecord(aclRecordToDelete);
                }
            }
        }

        #endregion

        #region Slides List / Create / Update / Delete 

        public virtual IActionResult List()
        {
            var model = new SlideSearchModel();
            model.SetGridPageSize();

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Slide/List.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult List(SlideSearchModel searchModel)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var gridModel = _slideModelFactory.PrepareSlideListPagedModel(searchModel);

            return Json(gridModel);
        }

        public virtual IActionResult Create()
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new SlideModel();
            _slideModelFactory.PrepareSlideModel(model, null);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Slide/Create.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult Create(SlideModel model)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var slide = new Slide()
                {
                    Description = model.Description,
                    HyperlinkAddress = model.Hyperlink,
                    PictureId = model.PictureId,
                    StartDateUtc = model.StartDateUtc,
                    EndDateUtc = model.EndDateUtc,
                    Published = model.Published,
                    LimitedToStores = model.SelectedStoreIds.Any(),
                    //1.0.5 all with Alc
                    SubjectToAcl = model.SelectedCustomerRoleIds.Count > 0
                };
                _slideService.InsertSlide(slide);

                //update slide locales
                UpdateSlideLocales(slide, model);

                //process slide store mappings
                UpdateStoreMapping(slide, model);

                //ACL (customer roles)
                //Set catalogsettings.ignoreacl = True to use ALC 
                SaveCustomerRolesAcl(slide, model);
            }

            //ACL
            _slideModelFactory.PrepareAclModel(model, null, true);

            //store mappings
            _slideModelFactory.PrepareStoreMapping(model, null);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Slide/Create.cshtml", model);
        }

        public virtual IActionResult Edit(int id)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var slide = _slideService.GetSlideById(id);
            if (slide == null)
                throw new Exception($"Slide by id: {id} isn't exist.");

            //prepare slide model
            var model = new SlideModel();
            _slideModelFactory.PrepareSlideModel(model, slide);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Slide/Edit.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult Edit(SlideModel model)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var slide = _slideService.GetSlideById(model.Id);
            if (slide == null)
                throw new Exception($"Slide by id: {model.Id} isn't exist.");

            if (ModelState.IsValid)
            {
                //set values
                slide.Description = model.Description;
                slide.HyperlinkAddress = model.Hyperlink;
                slide.PictureId = model.PictureId;
                slide.StartDateUtc = model.StartDateUtc;
                slide.EndDateUtc = model.EndDateUtc;
                slide.Published = model.Published;
                slide.LimitedToStores = model.SelectedStoreIds.Any();

                //1.0.5 all with Alc
                slide.SubjectToAcl = model.SelectedCustomerRoleIds.Count > 0;

                _slideService.UpdateSlide(slide);

                //update slide locales
                UpdateSlideLocales(slide, model);

                //process slide stores
                UpdateStoreMapping(slide, model);

                //ACL (customer roles)
                //Set catalogsettings.ignoreacl = True to use ALC
                SaveCustomerRolesAcl(slide, model);

                ViewBag.RefreshPage = true;
            }

            //ACL
            _slideModelFactory.PrepareAclModel(model, slide, true);

            //prepare store mappings
            _slideModelFactory.PrepareStoreMapping(model, slide);


            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Slide/Edit.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var slide = _slideService.GetSlideById(id);
            if (slide == null)
                throw new Exception("Slide aren't exist");

            //delete slide localized values
            _garbageManager.DeleteSlideLocalizedValues(slide);
            //delete slide picture
            _garbageManager.DeleteSlidePicture(slide);
            //delete slide entity
            _slideService.DeleteSlide(slide);

            return RedirectToAction("List");
        }

        #endregion

        #region Widget zone List / Add / Delete

        [HttpPost]
        public virtual IActionResult GetWidgetZoneList(SlideWidgetZoneSearchModel searchModel)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var gridModel = _slideWidgetZoneModelFactory.PrepareWidgetZoneList(searchModel);

            return Json(gridModel);
        }

        #endregion
    }
}