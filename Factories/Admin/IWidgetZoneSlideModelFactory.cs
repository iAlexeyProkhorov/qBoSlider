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
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone slide model factory interface
    /// </summary>
    public interface IWidgetZoneSlideModelFactory
    {
        /// <summary>
        /// Prepare widget zone slide list model
        /// </summary>
        /// <param name="searchModel">Widget zone slide search model</param>
        /// <returns>Widget zone slides paged list</returns>
        Task<WidgetZoneSlideSearchModel.SlideList> PrepareSlidePagedListModelAsync(WidgetZoneSlideSearchModel searchModel);

        /// <summary>
        /// Prepare add slide model search list
        /// </summary>
        /// <param name="searchModel">Add widget zone slide search model</param>
        /// <returns>Add widget zone slide model</returns>
        Task<AddWidgetZoneSlideModel.SlidePagedListModel> PrepareAddWidgetZoneSlideModelAsync(AddWidgetZoneSlideModel searchModel);

        /// <summary>
        /// Prepare widget zone slide model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns></returns>
        Task<WidgetZoneSlideModel> PrepareEditWidgetZoneSlideModelAsync(WidgetZoneSlide widgetZoneSlide);
    }
}
