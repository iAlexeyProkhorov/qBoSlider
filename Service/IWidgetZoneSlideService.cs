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
using Nop.Plugin.Widgets.qBoSlider.Domain;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone slide mappings service interface
    /// </summary>
    public interface IWidgetZoneSlideService
    {
        /// <summary>
        /// Get widget zone slide by id
        /// </summary>
        /// <param name="id">Slide entity id number</param>
        /// <returns>Widget zone entity</returns>
        Task<WidgetZoneSlide> GetWidgetZoneSlideAsync(int id);

        /// <summary>
        /// Get widget zones slide relations
        /// </summary>
        /// <param name="widgetZoneId">Widget zone id number</param>
        /// <param name="slideId">Slide id number</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zone slides collection</returns>
        IPagedList<WidgetZoneSlide> GetWidgetZoneSlides(int? widgetZoneId = null, int? slideId = null, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Insert new widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        Task InsertWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide);

        /// <summary>
        /// Update widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        Task UpdateWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide);

        /// <summary>
        /// Delete widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        Task DeleteWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide);
    }
}
