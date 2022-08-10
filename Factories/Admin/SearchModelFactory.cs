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
using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents search boxes model factory
    /// </summary>
    public class SearchModelFactory : ISearchModelFactory
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Constructor

        public SearchModelFactory(
            ILocalizationService localizationService,
            IWidgetZoneService widgetZoneService)
        {
            _localizationService = localizationService;
            _widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares slide search model
        /// </summary>
        /// <param name="model">Slide search model</param>
        /// <returns>Slide search model</returns>
        public virtual async Task PrepareSlideSearchModelAsync<TModel>(TModel model) where TModel : BaseSearchModel, ISlideSearchModel
        {
            model.AvailableWidgetZones = _widgetZoneService.GetWidgetZones().Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name} ({x.SystemName})"
                };
            }).ToList();
            model.AvailablePublicationStates = (await PublicationState.All.ToSelectListAsync(useLocalization: true)).ToList();
            model.SetGridPageSize();
            model.AvailableWidgetZones.Insert(0,
                new SelectListItem()
                {
                    Value = "0",
                    Text = await _localizationService.GetResourceAsync("admin.common.all")
                });
        }

        #endregion
    }
}
