using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone slide model factory interface
    /// </summary>
    public partial class WidgetZoneSlideModelFactory : IWidgetZoneSlideModelFactory
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        #endregion

        #region Constructor

        public WidgetZoneSlideModelFactory(ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService,
            IWidgetZoneSlideService widgetZoneSlideService)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _widgetZoneSlideService = widgetZoneSlideService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare widget zone slide list model
        /// </summary>
        /// <param name="searchModel">Widget zone slide search model</param>
        /// <returns>Widget zone slides paged list</returns>
        public virtual WidgetZoneSlideSearchModel.SlideList PrepareSlidePagedListModel(WidgetZoneSlideSearchModel searchModel)
        {
            var slides = _widgetZoneSlideService.GetWidgetZoneSlides(searchModel.WidgetZoneId, null, searchModel.Page - 1, searchModel.PageSize);
            var gridModel = new WidgetZoneSlideSearchModel.SlideList().PrepareToGrid(searchModel, slides, () =>
            {
                return slides.Select(slide =>
                {
                    var picture = _pictureService.GetPictureById(slide.Slide.PictureId.GetValueOrDefault(0));
                    var pictureUrl = _pictureService.GetPictureUrl(picture, 300);

                    return new WidgetZoneSlideSearchModel.SlideListItem()
                    {
                        Id = slide.Id,
                        PictureUrl = pictureUrl,
                        StartDateUtc = slide.Slide.StartDateUtc,
                        EndDateUtc = slide.Slide.EndDateUtc,
                        Published = slide.Slide.Published,
                        DisplayOrder = slide.DisplayOrder
                    };
                }).OrderBy(x => x.DisplayOrder);
            });

            return gridModel;
        }

        /// <summary>
        /// Prepare widget zone slide model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns></returns>
        public virtual WidgetZoneSlideModel PrepareEditWidgetZoneSlideModel(WidgetZoneSlide widgetZoneSlide)
        {
            var allLanguages = _languageService.GetAllLanguages(true);
            var slide = widgetZoneSlide.Slide;
            var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));

            if (picture == null)
                throw new Exception("Picture aren't exist");

            var model = new WidgetZoneSlideModel()
            {
                Id = widgetZoneSlide.Id,
                PictureId = picture.Id,
                PictureUrl = _pictureService.GetPictureUrl(picture, 200),
                SlideId = slide.Id,
                DisplayOrder = widgetZoneSlide.DisplayOrder,
                OverrideDescription = widgetZoneSlide.OverrideDescription
            };
            
            //add locales
            foreach(var language in allLanguages)
                model.Locales.Add(new WidgetZoneSlideModel.LocalizationModel()
                {
                    LanguageId = language.Id,
                    OverrideDescription = _localizationService.GetLocalized(widgetZoneSlide, x => x.OverrideDescription, language.Id)
                });

            return model;
        }

        #endregion
    }
}
