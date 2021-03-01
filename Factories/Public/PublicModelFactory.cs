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
using Nop.Core.Caching;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Extensions;
using Nop.Plugin.Widgets.qBoSlider.Infrastructure.Cache;
using Nop.Plugin.Widgets.qBoSlider.Models.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Caching;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Public
{
    /// <summary>
    /// Represents public model factory.
    /// Added to Multiwidget qBoSlider from version 1.1.0
    /// </summary>
    public class PublicModelFactory : IPublicModelFactory
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
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWidgetZoneService _widgetZoneService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public PublicModelFactory(IAclService aclService,
            ICacheKeyService cacheKeyService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            ISlideService slideService,
            IStaticCacheManager staticCacheManager,
            IStoreMappingService storeMappingService,
            IWidgetZoneService widgetZoneService,
            IWidgetZoneSlideService widgetZoneSlideService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            _aclService = aclService;
            _cacheKeyService = cacheKeyService;
            _customerService = customerService;
            _localizationService = localizationService;
            _pictureService = pictureService;
            _settingService = settingService;
            _slideService = slideService;
            _staticCacheManager = staticCacheManager;
            _storeMappingService = storeMappingService;
            _widgetZoneService = widgetZoneService;
            _widgetZoneSlideService = widgetZoneSlideService;

            _storeContext = storeContext;
            _workContext = workContext;
        }

        #endregion

        #region Utilites

        /// <summary>
        /// Preprw slide model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <param name="languageId">Language entity id number</param>
        /// <returns>Slide model</returns>
        protected virtual WidgetZoneModel.SlideModel PrepareSlideModel(WidgetZoneSlide widgetZoneSlide, Slide slide, int languageId)
        {
            var pictureId = _localizationService.GetLocalized(slide, z => z.PictureId, languageId, true, false);

            return new WidgetZoneModel.SlideModel()
            {
                Id = slide.Id,
                PictureUrl = _pictureService.GetPictureUrl(pictureId.GetValueOrDefault(0)),
                Description = !string.IsNullOrEmpty(widgetZoneSlide.OverrideDescription) ? _localizationService.GetLocalized(widgetZoneSlide, x => x.OverrideDescription, languageId) :
                _localizationService.GetLocalized(slide, z => z.Description, languageId),
                Hyperlink = _localizationService.GetLocalized(slide, z => z.HyperlinkAddress, languageId)
            };
        }

        protected virtual WidgetZoneModel PrepareSliderModel(WidgetZone widgetZone, int languageId, int storeId)
        {
            //prepare slider model
            var model = new WidgetZoneModel()
            {
                Id = widgetZone.Id,
                AutoPlay = widgetZone.AutoPlay,
                AutoPlayInterval = widgetZone.AutoPlayInterval,
                MinDragOffsetToSlide = widgetZone.MinDragOffsetToSlide,
                SlideDuration = widgetZone.SlideDuration,
                SlideSpacing = widgetZone.SlideSpacing,
                ArrowNavigation = widgetZone.ArrowNavigationDisplayingTypeId,
                BulletNavigation = widgetZone.BulletNavigationDisplayingTypeId,
                MinSliderWidth = widgetZone.MinSlideWidgetZoneWidth,
                MaxSliderWidth = widgetZone.MaxSlideWidgetZoneWidth
            };

            //add slide models to widget zone slider
            var widgetZoneSlides = _widgetZoneSlideService.GetWidgetZoneSlides(widgetZone.Id).OrderBy(s => s.DisplayOrder);
            foreach (var widgetSlide in widgetZoneSlides)
            {
                var slide = _slideService.GetSlideById(widgetSlide.SlideId);

                //don't display unpublished slides
                if (!slide.Published)
                    continue;

                var today = slide.PublishToday();
                var acl = _aclService.Authorize(slide);
                var store = _storeMappingService.Authorize(slide, storeId);

                //don't display slides, which shouldn't displays today or not authorized via ACL or not authorized in store
                var display = slide.PublishToday() && _aclService.Authorize(slide) && _storeMappingService.Authorize(slide, storeId);
                if (!display)
                    continue;

                //prepare slide model
                var slideModel = PrepareSlideModel(widgetSlide, slide, languageId);

                //add slide model to slider
                model.Slides.Add(slideModel);
            }

            return model;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Widget zone model</returns>
        public virtual WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone)
        {
            var languageId = _workContext.WorkingLanguage.Id;
            var storeId = _storeContext.CurrentStore.Id;

            return PrepareWidgetZoneModel(widgetZone, languageId, storeId);
        }

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <param name="storeId">Store id number</param>
        /// <param name="languageId">Language entity id number</param>
        /// <returns>Widget zone model</returns>
        public virtual WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone, int languageId, int storeId)
        {
            if (widgetZone == null)
                throw new Exception("Widget zone can't be null");

            var settings = _settingService.LoadSetting<qBoSliderSettings>(storeId);

            //1.0.5 all with Alc
            var customer = _workContext.CurrentCustomer;
            var customerRoles = _customerService.GetCustomerRoleIds(customer);
            var customerRolesString = string.Join(",", customerRoles);

            //prepare widget zone model with slide and prepare cache key to load slider faster next time
            var cacheKey = _cacheKeyService.PrepareKey(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, widgetZone.Id, languageId, storeId, DateTime.UtcNow.ToShortDateString(), customerRolesString);
            //load model from cache or process it
            var model = settings.UseStaticCache ? _staticCacheManager.Get(cacheKey, () =>
            {
                return PrepareSliderModel(widgetZone, languageId, storeId);
            }) : PrepareSliderModel(widgetZone, languageId, storeId);

            return model;
        }

        #endregion
    }
}
