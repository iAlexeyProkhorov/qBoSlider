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
using Nop.Core.Caching;
using Nop.Plugin.Widgets.qBoSlider.Extensions;
using Nop.Plugin.Widgets.qBoSlider.Infrastructure.Cache;
using Nop.Plugin.Widgets.qBoSlider.Models;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Caching;
using Nop.Services.Configuration;
using Nop.Services.Customers;
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
        private readonly ICacheKeyService _cacheKeyService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ISlideService _slideService;
        private readonly IStaticCacheManager _staticCacheManager;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public PublicInfoComponent(IAclService aclService,
            ICacheKeyService cacheKeyService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideService slideService,
            IStaticCacheManager staticCacheManager,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            this._aclService = aclService;
            this._cacheKeyService = cacheKeyService;
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._slideService = slideService;
            this._staticCacheManager = staticCacheManager;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke()
        {
            var settings = _settingService.LoadSetting<qBoSliderSettings>(_storeContext.CurrentStore.Id);
            var customer = _workContext.CurrentCustomer;

            //1.0.5 all with Alc
            var customerRoleIds = _customerService.GetCustomerRoleIds(customer);
            var customerRolesString = string.Join(",", customerRoleIds);
            //create cache key
            var cacheKey = _cacheKeyService.PrepareKeyForDefaultCache(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, _workContext.WorkingLanguage.Id, _storeContext.CurrentStore.Id, DateTime.UtcNow.ToShortDateString(), customerRolesString);

            var model = _staticCacheManager.Get(cacheKey, () =>
            {
                var result = new PublicInfoModel()
                {
                    AutoPlay = settings.AutoPlay,
                    AutoPlayInterval = settings.AutoPlayInterval,
                    MinDragOffsetToSlide = settings.MinDragOffsetToSlide,
                    SlideDuration = settings.SlideDuration,
                    SlideSpacing = settings.SlideSpacing,
                    ArrowNavigation = settings.ArrowNavigationDisplay,
                    BulletNavigation = settings.BulletNavigationDisplay
                };

                result.Slides = _slideService.GetAllSlides(storeId: _storeContext.CurrentStore.Id)
                .Where(x => x.PublishToday()
                    //1.0.5 all with Alc
                    //Set catalogsettings.ignoreacl = True to use ALC
                    //&& ((customerRoleIds.Except(_aclService.GetCustomerRoleIdsWithAccess(x).ToList()).ToList().Count < customerRoleIds.Count) || (_aclService.GetCustomerRoleIdsWithAccess(x).ToList().Count == 0)))
                    && (_aclService.Authorize(x)))
                .OrderBy(x => x.DisplayOrder).Select(slide =>
                {
                    var pictureId = _localizationService.GetLocalized(slide, z => z.PictureId, _workContext.WorkingLanguage.Id, true, false);

                    return new PublicInfoModel.PublicSlideModel()
                    {
                        Picture = _pictureService.GetPictureUrl(pictureId.GetValueOrDefault(0)),
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
