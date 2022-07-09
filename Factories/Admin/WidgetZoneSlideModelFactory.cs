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
    /// Represents widget zone slide model factory. Using for widget zone editing.
    /// </summary>
    public partial class WidgetZoneSlideModelFactory : IWidgetZoneSlideModelFactory
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

        public WidgetZoneSlideModelFactory(ILanguageService languageService,
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
                return slides.Select(widgetZoneSlide =>
                {
                    var slide = _slideService.GetSlideById(widgetZoneSlide.SlideId);
                    var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
                    var pictureUrl = _pictureService.GetPictureUrl(picture.Id, 300);

                    return new WidgetZoneSlideSearchModel.SlideListItem()
                    {
                        Id = widgetZoneSlide.Id,
                        Name = slide.Name,
                        PictureUrl = pictureUrl,
                        StartDateUtc = slide.StartDateUtc,
                        EndDateUtc = slide.EndDateUtc,
                        Published = slide.Published,
                        DisplayOrder = widgetZoneSlide.DisplayOrder
                    };
                }).OrderBy(x => x.DisplayOrder);
            });

            return gridModel;
        }

        /// <summary>
        /// Prepare add slide model search list
        /// </summary>
        /// <param name="searchModel">Add widget zone slide search model</param>
        /// <returns>Add widget zone slide model</returns>
        public virtual AddWidgetZoneSlideModel.SlidePagedListModel PrepareAddWidgetZoneSlideModel(AddWidgetZoneSlideModel searchModel)
        {
            var slides = _slideService.GetAllSlides(name: searchModel.SearchName,
               widgetZoneIds: searchModel.SearchWidgetZoneId > 0 ? new int[1] { searchModel.SearchWidgetZoneId } : null,
               startDate: searchModel.SearchStartDateOnUtc,
               endDate: searchModel.SearchFinishDateOnUtc,
               publicationState: (PublicationState)searchModel.SearchPublicationStateId,
               pageIndex: searchModel.Page - 1,
               pageSize: searchModel.PageSize);

            var gridModel = new AddWidgetZoneSlideModel.SlidePagedListModel().PrepareToGrid(searchModel, slides, () =>
            {
                return slides.Select(slide =>
                {
                    var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
                    var pictureUrl = _pictureService.GetPictureUrl(picture.Id, 300);

                    return new AddWidgetZoneSlideModel.SlideModel()
                    {
                        Id = slide.Id,
                        PictureUrl = pictureUrl,
                        Name = slide.Name,
                        StartDateUtc = slide.StartDateUtc,
                        EndDateUtc = slide.EndDateUtc,
                        Published = slide.Published
                    };
                });
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
            var slide = _slideService.GetSlideById(widgetZoneSlide.SlideId);
            var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));

            if (picture == null)
                throw new Exception("Picture aren't exist");

            var model = new WidgetZoneSlideModel()
            {
                Id = widgetZoneSlide.Id,
                PictureId = picture.Id,
                PictureUrl = _pictureService.GetPictureUrl(picture.Id, 200),
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
