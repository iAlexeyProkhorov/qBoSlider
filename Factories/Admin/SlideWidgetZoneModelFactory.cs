﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;
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
        private readonly IWidgetZoneService _widgetZoneService;
        private readonly IWidgetZoneSlideService _widgetZoneSlideService;

        #endregion

        #region Constructor

        public SlideWidgetZoneModelFactory(ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService,
            ISlideService slideService,
            IWidgetZoneService widgetZoneService,
            IWidgetZoneSlideService widgetZoneSlideService)
        {
            _languageService = languageService;
            _localizationService = localizationService;
            _localizedEntityService = localizedEntityService;
            _pictureService = pictureService;
            _slideService = slideService;
            _widgetZoneService = widgetZoneService;
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


        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Widget zone model</returns>
        protected virtual AddSlideWidgetZoneModel.WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone)
        {
            return new AddSlideWidgetZoneModel.WidgetZoneModel()
            {
                Id = widgetZone.Id,
                Name = widgetZone.Name,
                SystemName = widgetZone.SystemName,
                Published = widgetZone.Published
            };
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

        /// <summary>
        /// Prepare slide widget zones list for 'Add widget zone' model
        /// </summary>
        /// <param name="searchModel">Add slide widget zone model</param>
        /// <returns>Slide widget zone list</returns>
        public virtual AddSlideWidgetZoneModel.WidgetZonePagedList PrepareWidgetZoneList(AddSlideWidgetZoneModel searchModel)
        {
            var allWidgetZones = _widgetZoneService.GetWidgetZones();
            var gridModel = new AddSlideWidgetZoneModel.WidgetZonePagedList().PrepareToGrid(searchModel, allWidgetZones, () =>
            {
                return allWidgetZones.Select(widgetZone =>
                {
                    return PrepareWidgetZoneModel(widgetZone);
                });
            });

            return gridModel;
        }

        /// <summary>
        /// Prepare edit slide widget zone edit model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns>Edit slide widget zone model</returns>
        public virtual EditSlideWidgetZoneModel PrepareEditSlideWidgetZoneModel(WidgetZoneSlide widgetZoneSlide)
        {
            var allLanguages = _languageService.GetAllLanguages();
            var allWidgetZones = _widgetZoneService.GetWidgetZones();
            var model = new EditSlideWidgetZoneModel()
            {
                AvailableWidgetZones = allWidgetZones.Select(widgetZone =>
                {
                    return new SelectListItem()
                    {
                        Value = widgetZone.Id.ToString(),
                        Text = $"{widgetZone.Name}({widgetZone.SystemName})"
                    };
                }).ToList(),
                SlideId = widgetZoneSlide.SlideId,
                WidgetZoneId = widgetZoneSlide.WidgetZoneId,
                OverrideDescription = widgetZoneSlide.OverrideDescription
            };

            //add locales
            foreach (var language in allLanguages)
                model.Locales.Add(new EditSlideWidgetZoneModel.LocalizationModel()
                {
                    LanguageId = language.Id,
                    OverrideDescription = _localizationService.GetLocalized(widgetZoneSlide, x => x.OverrideDescription, language.Id)
                });

            return model;
        }

        #endregion
    }
}