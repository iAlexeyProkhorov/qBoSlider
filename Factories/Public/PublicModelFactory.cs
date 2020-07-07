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

using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Customers;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Extensions;
using Nop.Plugin.Widgets.qBoSlider.Models.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using System;

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
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWidgetZoneService _widgetZoneService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public PublicModelFactory(IAclService aclService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            IPictureService pictureService,
            ISettingService settingService,
            IStoreMappingService storeMappingService,
            IWidgetZoneService widgetZoneService,
            IStoreContext storeContext,
            IWorkContext workContext)
        {
            this._aclService = aclService;
            this._cacheManager = cacheManager;
            this._localizationService = localizationService;
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._storeMappingService = storeMappingService;
            this._widgetZoneService = widgetZoneService;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Utilites

        /// <summary>
        /// Preprw slide model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <param name="languageId">Language entity id number</param>
        /// <returns>Slide model</returns>
        protected virtual WidgetZoneModel.SlideModel PrepareSlideModel(WidgetZoneSlide widgetZoneSlide, int languageId)
        {
            var slide = widgetZoneSlide.Slide;

            var pictureId = _localizationService.GetLocalized(slide, z => z.PictureId, languageId, true, false);
            var picture = _pictureService.GetPictureById(pictureId.GetValueOrDefault(0));

            return new WidgetZoneModel.SlideModel()
            {
                Id = slide.Id,
                PictureUrl = _pictureService.GetPictureUrl(picture),
                Description = !string.IsNullOrEmpty(widgetZoneSlide.OverrideDescription) ? _localizationService.GetLocalized(widgetZoneSlide, x => x.OverrideDescription, languageId) :
                _localizationService.GetLocalized(slide, z => z.Description, languageId),
                Hyperlink = _localizationService.GetLocalized(slide, z => z.HyperlinkAddress, languageId)
            };
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
            var customerRoles = _workContext.CurrentCustomer.GetCustomerRoleIds();
            var customerRolesString = string.Join(",", customerRoles);

            //prepare widget zone model with slide and prepare cache key to load slider faster next time
            var model = _cacheManager.Get($"qbo-slider-publicinfo-{widgetZone.Id}-{languageId}-{storeId}-{DateTime.UtcNow.ToShortDateString()}-{customerRolesString}", () =>
            {
                //prepare slider model
                var result = new WidgetZoneModel()
                {
                    Id = widgetZone.Id,
                    AutoPlay = widgetZone.AutoPlay,
                    AutoPlayInterval = widgetZone.AutoPlayInterval,
                    MinDragOffsetToSlide = widgetZone.MinDragOffsetToSlide,
                    SlideDuration = widgetZone.SlideDuration,
                    SlideSpacing = widgetZone.SlideSpacing,
                    ArrowNavigation = widgetZone.ArrowNavigationDisplayingTypeId,
                    BulletNavigation = widgetZone.BulletNavigationDisplayingTypeId,
                };

                //add slide models to widget zone slider
                foreach(var widgetSlide in widgetZone.WidgetZoneSlides)
                {
                    var slide = widgetSlide.Slide;

                    //don't display unpublished slides
                    if (!slide.Published)
                        continue;

                    //don't display slides, which shouldn't displays today or not authorized via ACL or not authorized in store
                    if (!slide.PublishToday() && !_aclService.Authorize(slide) && !_storeMappingService.Authorize(slide, storeId))
                        continue;

                    //prepare slide model
                    var slideModel = PrepareSlideModel(widgetSlide, languageId);

                    //add slide model to slider
                    result.Slides.Add(slideModel);
                }

                return result;
            });

            return model;
        }

        #endregion
    }
}
