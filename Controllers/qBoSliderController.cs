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
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Plugin.Widgets.qBoSlider.Validators;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class qBoSliderController : BasePluginController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideService _slideService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public qBoSliderController(IAclService aclService,
            ICustomerService customerService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideService slideService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            this._aclService = aclService;
            this._customerService = customerService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._notificationService = notificationService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._slideService = slideService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Utilites

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

        protected virtual void PrepareStoreMapping(Slide slide, SlideModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService.GetAllStores().Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                };
            }).OrderBy(x => x.Text).ToList();

            if (slide != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(slide).ToList();
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

        protected virtual void PrepareAclModel(SlideModel model, Slide slide, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && slide != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(slide).ToList();

            var allRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var role in allRoles)
            {
                model.AvailableCustomerRoles.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                });
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

        #region Cofiguration

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var qBoSliderSettings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

            var model = new ConfigurationModel()
            {
                AutoPlay = qBoSliderSettings.AutoPlay,
                AutoPlayInterval = qBoSliderSettings.AutoPlayInterval,
                MinDragOffsetToSlide = qBoSliderSettings.MinDragOffsetToSlide,
                SlideDuration = qBoSliderSettings.SlideDuration,
                SlideSpacing = qBoSliderSettings.SlideSpacing,
                ActiveStoreScopeConfiguration = storeScope,
                ArrowNavigation = qBoSliderSettings.ArrowNavigationDisplay,
                AvailableArrowNavigations = DisplayNavigation.OnMouseDrag.ToSelectList().ToList(),
                BulletNavigation = qBoSliderSettings.BulletNavigationDisplay,
                AvailableBulletNavigations = DisplayNavigation.Always.ToSelectList().ToList(),
                WidgetZoneName = qBoSliderSettings.WidgetZoneName
            };

            model.SearchModel.SetGridPageSize();

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var qBoSliderSettings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

            qBoSliderSettings.AutoPlay = model.AutoPlay;
            qBoSliderSettings.AutoPlayInterval = model.AutoPlayInterval;
            qBoSliderSettings.MinDragOffsetToSlide = model.MinDragOffsetToSlide;
            qBoSliderSettings.SlideDuration = model.SlideDuration;
            qBoSliderSettings.SlideSpacing = model.SlideSpacing;
            qBoSliderSettings.ArrowNavigationDisplay = model.ArrowNavigation;
            qBoSliderSettings.BulletNavigationDisplay = model.BulletNavigation;
            qBoSliderSettings.WidgetZoneName = model.WidgetZoneName;

            _settingService.SaveSetting(qBoSliderSettings, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }

        #endregion

        #region List / Create / Update / Delete 

        [HttpPost]
        public IActionResult SlideList(SlideSearchModel searchModel)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var slides = _slideService.GetAllSlides(showHidden: true, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var gridModel = new SlideSearchModel.SlidePagedListModel().PrepareToGrid(searchModel, slides, () =>
            {
                return slides.Select(slide =>
                {
                    var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
                    return new SlideSearchModel.SlideListItemModel()
                    {
                        Id = slide.Id,
                        Picture = _pictureService.GetPictureUrl(slide.PictureId.GetValueOrDefault(0), 300),
                        Hyperlink = slide.HyperlinkAddress,
                        StartDateUtc = slide.StartDateUtc,
                        EndDateUtc = slide.EndDateUtc,
                        DisplayOrder = slide.DisplayOrder,
                        Published = slide.Published
                    };
                });
            });

            return Json(gridModel);
        }

        public IActionResult CreateSlidePopup()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new SlideModel();
            AddLocales(_languageService, model.Locales);

            //Prepare store mappings
            PrepareStoreMapping(null, model);

            //prepare acl
            PrepareAclModel(model, null, false);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/CreateSlidePopup.cshtml", model);
        }

        [HttpPost]
        public IActionResult CreateSlidePopup(SlideModel model, string btnId, string formId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var validator = new SlideValidator(_localizationService);
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                var slide = new Slide()
                {
                    Description = model.Description,
                    HyperlinkAddress = model.Hyperlink,
                    PictureId = model.PictureId,
                    DisplayOrder = model.DisplayOrder,
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

                ViewBag.btnId = btnId;
                ViewBag.formId = formId;
                ViewBag.RefreshPage = true;
            }

            //ACL
            PrepareAclModel(model, null, true);

            //store mapping
            PrepareStoreMapping(null, model);
            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/CreateSlidePopup.cshtml", model);
        }

        public IActionResult EditSlidePopup(int id, string btnId, string formId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new SlideModel();
            var slide = _slideService.GetSlideById(id);

            //set localized values
            AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Hyperlink = _localizationService.GetLocalized(slide, x => x.HyperlinkAddress, languageId, false, false);
                locale.Description = _localizationService.GetLocalized(slide, x => x.Description, languageId, false, false);
                locale.PictureId = _localizationService.GetLocalized(slide, x => x.PictureId, languageId, false, false).GetValueOrDefault(0);
            });

            //set default values
            model.Description = slide.Description;
            model.Hyperlink = slide.HyperlinkAddress;
            model.PictureId = slide.PictureId.GetValueOrDefault(0);
            model.StartDateUtc = slide.StartDateUtc;
            model.EndDateUtc = slide.EndDateUtc;
            model.Published = slide.Published;
            model.DisplayOrder = slide.DisplayOrder;

            //process store mapping
            PrepareStoreMapping(slide, model);

            //ACL
            PrepareAclModel(model, slide, false);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/EditSlidePopup.cshtml", model);
        }

        [HttpPost]
        public IActionResult EditSlidePopup(SlideModel model, string btnId, string formId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var slide = _slideService.GetSlideById(model.Id);

            //parent form data for refresh
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;

            if (slide == null)
            {
                ViewBag.RefreshPage = true;
                View("~/Plugins/Widgets.qBoSlider/Views/qBoSlider/EditSlidePopup.cshtml", model);
            }

            var validator = new SlideValidator(_localizationService);
            var validationResult = validator.Validate(model);

            if (validationResult.IsValid)
            {
                //set default values
                slide.Description = model.Description;
                slide.HyperlinkAddress = model.Hyperlink;
                slide.PictureId = model.PictureId;
                slide.StartDateUtc = model.StartDateUtc;
                slide.EndDateUtc = model.EndDateUtc;
                slide.Published = model.Published;
                slide.DisplayOrder = model.DisplayOrder;
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
            PrepareAclModel(model, slide, true);

            //Store mappings
            PrepareStoreMapping(slide, model);


            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/EditSlidePopup.cshtml", model);
        }

        [HttpPost]
        public IActionResult SlideDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var slide = _slideService.GetSlideById(id);

            if (slide != null)
                _slideService.DeleteSlide(slide);

            return new NullJsonResult();
        }

        #endregion
    }
}