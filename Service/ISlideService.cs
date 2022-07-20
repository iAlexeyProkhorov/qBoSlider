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
using System;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents qBoSlider slide service interface
    /// </summary>
    public interface ISlideService
    {
        /// <summary>
        /// Get slide by individual id number
        /// </summary>
        /// <param name="id">Slide individual number</param>
        /// <returns>Slide</returns>
        Task<Slide> GetSlideByIdAsync(int id);

        /// <summary>
        /// Get all slides for current store
        /// </summary>
        /// <param name="name">Searching slider name</param>
        /// <param name="widgetZoneIds">Slides for widget zones</param>
        /// <param name="startDate">Publication start date</param>
        /// <param name="endDate">Publication end date</param>
        /// <param name="publicationState">Searching slides publication state</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page sizer</param>
        /// <param name="storeId">Store id number</param>
        /// <returns>Paged slides list</returns>
        Task<IPagedList<Slide>> GetAllSlidesAsync(string name = null,
            int[] widgetZoneIds = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            PublicationState publicationState = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            int storeId = 0);

        /// <summary>
        /// Insert slide
        /// </summary>
        /// <param name="slide">Inserting slide</param>
        Task InsertSlideAsync(Slide slide);

        /// <summary>
        /// Update already exist slide
        /// </summary>
        /// <param name="slide">Updating slide</param>
        Task UpdateSlideAsync(Slide slide);

        /// <summary>
        /// Delete slide from database
        /// </summary>
        /// <param name="slide">Deleting slide</param>
        Task DeleteSlideAsync(Slide slide);
    }
}
