using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Widgets.qBoSlider.Extensions;
using Nop.Plugin.Widgets.qBoSlider.Models;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Web.Framework.Components;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Components
{
    [ViewComponent(Name = "Baroque.qBoSlider.PublicInfo")]
    public class PublicInfoComponent : NopViewComponent
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideService _slideService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public PublicInfoComponent(IAclService aclService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideService slideService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            this._aclService = aclService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._slideService = slideService;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke()
        {
            var qBoSliderSettings = _settingService.LoadSetting<qBoSliderSettings>(_storeContext.CurrentStore.Id);

            //1.0.5 all with Alc
            var customerRolesList = _workContext.CurrentCustomer.GetCustomerRoleIds().ToList();
            var customerRolesString = string.Empty;
            foreach (var roleId in customerRolesList)
                customerRolesString = string.Format("{0},{1}", customerRolesString, roleId);
            customerRolesString = customerRolesString.Remove(0, 1);

            var model = _cacheManager.Get(string.Format("qbo-slider-publicinfo-{0}-{1}-{2}-{3}", _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id, DateTime.UtcNow.ToShortDateString(), customerRolesString), () =>
            {
                var result = new PublicInfoModel()
                {
                    AutoPlay = qBoSliderSettings.AutoPlay,
                    AutoPlayInterval = qBoSliderSettings.AutoPlayInterval,
                    MinDragOffsetToSlide = qBoSliderSettings.MinDragOffsetToSlide,
                    SlideDuration = qBoSliderSettings.SlideDuration,
                    SlideSpacing = qBoSliderSettings.SlideSpacing,
                    ArrowNavigation = qBoSliderSettings.ArrowNavigationDisplay,
                    BulletNavigation = qBoSliderSettings.BulletNavigationDisplay
                };

                result.Slides = _slideService.GetAllSlides(storeId: _storeContext.CurrentStore.Id)
                .Where(x => x.PublishToday()
                    //1.0.5 all with Alc
                    //Set catalogsettings.ignoreacl = True to use ALC
                    //&& ((customerRolesList.Except(_aclService.GetCustomerRoleIdsWithAccess(x).ToList()).ToList().Count < customerRolesList.Count) || (_aclService.GetCustomerRoleIdsWithAccess(x).ToList().Count == 0)))
                    && (_aclService.Authorize(x)))
                .OrderBy(x => x.DisplayOrder).Select(slide =>
                {
                    var id = _localizationService.GetLocalized(slide, z => z.PictureId, _workContext.WorkingLanguage.Id, true, false);
                    var picture = _pictureService.GetPictureById(id.GetValueOrDefault(0));
                    return new PublicInfoModel.PublicSlideModel()
                    {
                        Picture = _pictureService.GetPictureUrl(picture),
                        Description = _localizationService.GetLocalized(slide, z => z.Description, _workContext.WorkingLanguage.Id),
                        Hyperlink = _localizationService.GetLocalized(slide, z => z.HyperlinkAddress, _workContext.WorkingLanguage.Id)
                    };
                }).ToList();
                return result;
            });

            return View("~/Plugins/Widgets.qBoSlider/Views/Public/PublicInfo.cshtml", model);
        }

        #endregion
    }
}
