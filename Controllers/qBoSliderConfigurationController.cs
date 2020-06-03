using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Plugin.Widgets.qBoSlider.Validators;
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
    public class qBoSliderConfigurationController : BasePluginController
    {
        #region Fields

        private readonly ICacheManager _cacheManager;

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideModelFactory _slideModelFactory;
        private readonly ISlideService _slideService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public qBoSliderConfigurationController(ICacheManager cacheManager,
            IAclService aclService,
            ICustomerService customerService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            INotificationService notificationService,
            IPermissionService permissionService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideModelFactory slideModelFactory,
            ISlideService slideService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            ForseDefaultCulture();
            this._cacheManager = cacheManager;

            this._aclService = aclService;
            this._customerService = customerService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._notificationService = notificationService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._slideModelFactory = slideModelFactory;
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

        #region Cofiguration

        public IActionResult Configure()
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //load settings for a chosen store scope
            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var qBoSliderSettings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

            var model = new ConfigurationModel()
            {
                ActiveStoreScopeConfiguration = storeScope
            };

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var storeScope = _storeContext.ActiveStoreScopeConfiguration;
            var qBoSliderSettings = _settingService.LoadSetting<qBoSliderSettings>(storeScope);

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
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var slides = _slideService.GetAllSlides(showHidden: true, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var gridModel = _slideModelFactory.P

            return Json(gridModel);
        }

        public IActionResult CreateSlidePopup()
        {
            //redirect customer on accessdenied view, if client has no permissions
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
            //redirect customer on accessdenied view, if client has no permissions
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
            //redirect customer on accessdenied view, if client has no permissions
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

            //process store mapping
            PrepareStoreMapping(slide, model);

            //ACL
            PrepareAclModel(model, slide, false);

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/EditSlidePopup.cshtml", model);
        }

        [HttpPost]
        public IActionResult EditSlidePopup(SlideModel model, string btnId, string formId)
        {
            //redirect customer on accessdenied view, if client has no permissions
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
            //redirect customer on accessdenied view, if client has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var slide = _slideService.GetSlideById(id);

            if (slide != null)
            {
                var allLanguages = _languageService.GetAllLanguages(true);

                //delete slide localized resources
                if (allLanguages.Count > 1)
                    foreach (var language in allLanguages)
                    {
                        var pictureIdLocalizaedValue = _localizedEntityService.GetLocalizedValue(language.Id, slide.Id, "Slide", "PictureId");
                        var isPictureValid = int.TryParse(pictureIdLocalizaedValue, out int localizePictureId);

                        //delete localized values
                        _localizedEntityService.SaveLocalizedValue(slide, x => x.PictureId, null, language.Id);
                        _localizedEntityService.SaveLocalizedValue(slide, x => x.HyperlinkAddress, null, language.Id);
                        _localizedEntityService.SaveLocalizedValue(slide, x => x.Description, null, language.Id);

                        //remove localized picture
                        if (!string.IsNullOrEmpty(pictureIdLocalizaedValue) && isPictureValid)
                        {
                            var localizedPicture = _pictureService.GetPictureById(localizePictureId);

                            //go to next picture if current picture aren't exist
                            if (localizedPicture == null)
                                continue;

                            _pictureService.DeletePicture(localizedPicture);
                        }
                    }

                //delete slide base picture
                var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
                if (picture != null)
                    _pictureService.DeletePicture(picture);

                _slideService.DeleteSlide(slide);
            }

            return new NullJsonResult();
        }

        #endregion
    }
}