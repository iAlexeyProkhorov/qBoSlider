using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents slide model widget zone factory. Using for slide editing
    /// </summary>
    public class SlideWidgetZoneModelFactory : ISlideWidgetZoneModelFactory
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly ISlideService _slideService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        #endregion

        #region Constructor

        public SlideWidgetZoneModelFactory(ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService,
            ISlideService slideService,
            IWidgetZoneSlideService widgetZoneSlideService)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _slideService = slideService;
            _widgetZoneSlideService = widgetZoneSlideService;
        }

        #endregion

        #region Utilites

        /// <summary>
        /// Prepare model inherits slide model interface
        /// </summary>
        /// <typeparam name="TModel">Slide widget zone model interface</typeparam>
        /// <param name="widgetZoneSlide">Widget zone slide</param>
        /// <returns>Slide widget zone model</returns>
        protected virtual TModel PrepareSlideWidgetZoneModel<TModel>(WidgetZoneSlide widgetZoneSlide) where TModel: BaseNopEntityModel, ISlideWidgetZoneModel, new()
        {
            var model = new TModel();
            var widgetZone = widgetZoneSlide.WidgetZone;

            model.Id = widgetZoneSlide.Id;
            model.Name = widgetZone.Name;
            model.SystemName = widgetZone.SystemName;
            model.Published = widgetZone.Published;

            return model;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare slide widget zone list
        /// </summary>
        /// <param name="searchModel">Widget zone search model</param>
        /// <returns>Slide widget zones list</returns>
        public virtual SlideWidgetZoneSearchModel.WidgetZonePagedList PrepareWidgetZoneList(SlideWidgetZoneSearchModel searchModel)
        {
            var allSlideWidgetZones = _widgetZoneSlideService.GetWidgetZoneSlides(slideId: searchModel.SlideId);
            var gridModel = new SlideWidgetZoneSearchModel.WidgetZonePagedList().PrepareToGrid(searchModel, allSlideWidgetZones, () =>
            {
                return allSlideWidgetZones.Select(slideWidgetZone =>
                {
                    var widgetZone = slideWidgetZone.WidgetZone;
                    return PrepareSlideWidgetZoneModel<SlideWidgetZoneSearchModel.WidgetZoneModel>(slideWidgetZone);
                });
            });

            return gridModel;
        }

        #endregion
    }
}
