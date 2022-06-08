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

using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Models.Extensions;
using System.Linq;
using System.Threading.Tasks;

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
        protected virtual async Task<TModel> PrepareSlideWidgetZoneModelAsync<TModel>(WidgetZoneSlide widgetZoneSlide) where TModel: BaseNopEntityModel, ISlideWidgetZoneModel, new()
        {
            var model = new TModel();
            var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(widgetZoneSlide.WidgetZoneId);

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
        public virtual async Task<SlideWidgetZoneSearchModel.WidgetZonePagedList> PrepareWidgetZoneListAsync(SlideWidgetZoneSearchModel searchModel)
        {
            var allSlideWidgetZones = _widgetZoneSlideService.GetWidgetZoneSlides(slideId: searchModel.SlideId);
            var gridModel = await new SlideWidgetZoneSearchModel.WidgetZonePagedList().PrepareToGridAsync(searchModel, allSlideWidgetZones, () =>
            {
                return allSlideWidgetZones.SelectAwait(async slideWidgetZone =>
                {
                    var widgetZone = await _widgetZoneService.GetWidgetZoneByIdAsync(slideWidgetZone.WidgetZoneId);
                    return await PrepareSlideWidgetZoneModelAsync<SlideWidgetZoneSearchModel.WidgetZoneModel>(slideWidgetZone);
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
            var allWidgetZones = _widgetZoneService.GetWidgetZones(searchModel.SearchWidgetZoneName, searchModel.SearchWidgetZoneSystemName, true, searchModel.Page - 1, searchModel.PageSize);
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
        public virtual async Task<EditSlideWidgetZoneModel> PrepareEditSlideWidgetZoneModelAsync(WidgetZoneSlide widgetZoneSlide)
        {
            var allLanguages = await _languageService.GetAllLanguagesAsync();
            var allWidgetZones = _widgetZoneService.GetWidgetZones();

            var model = new EditSlideWidgetZoneModel()
            {
                Id = widgetZoneSlide.Id,
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
                    OverrideDescription = await _localizationService.GetLocalizedAsync(widgetZoneSlide, x => x.OverrideDescription, language.Id)
                });

            return model;
        }

        #endregion
    }
}
