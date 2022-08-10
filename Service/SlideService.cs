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
using Nop.Data;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents qBoSlider slide service
    /// </summary>
    public partial class SlideService : ISlideService
    {
        #region Fields

        private readonly IRepository<Slide> _slideRepository;
        private readonly IRepository<WidgetZoneSlide> _widgetZoneSlideRepository;
        private readonly IStoreMappingService _storeMappingService;

        #endregion

        #region Constructor

        public SlideService(IRepository<Slide> slideRepository,
            IRepository<WidgetZoneSlide> widgetZoneSlideRepository,
            IStoreMappingService storeMappingService)
        { 
            _slideRepository = slideRepository;
            _widgetZoneSlideRepository = widgetZoneSlideRepository;
            _storeMappingService = storeMappingService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get slide by individual id number
        /// </summary>
        /// <param name="id">Slide individual number</param>
        /// <returns>Slide</returns>
        public virtual async Task<Slide> GetSlideByIdAsync(int id)
        {
            return await _slideRepository.GetByIdAsync(id);
        }

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
        public virtual async Task<IPagedList<Slide>> GetAllSlidesAsync(string name = null,
            int[] widgetZoneIds = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            PublicationState publicationState = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            int storeId = 0)
        {
            var result = await _slideRepository.GetAllPagedAsync(async query =>
            {
                //filter slides, which contains part of searchable slide name
                if (!string.IsNullOrEmpty(name))
                    query = query.Where(x => x.Name.Contains(name));

                //filter by widget zones
                if (widgetZoneIds != null && widgetZoneIds.Any())
                    query = from slide in query
                            join widgetZoneSlide in _widgetZoneSlideRepository.Table on slide.Id equals widgetZoneSlide.SlideId
                            where widgetZoneIds.Contains(widgetZoneSlide.WidgetZoneId)
                            select slide;

                //filter by start date
                if (startDate.HasValue)
                    query = query.Where(x => !x.StartDateUtc.HasValue || x.StartDateUtc >= startDate.Value);

                //filter by end publishing date
                if (endDate.HasValue)
                    query = query.Where(x => !x.EndDateUtc.HasValue || x.EndDateUtc <= endDate.Value);

                //filter unpublished slides
                if (publicationState == PublicationState.Published)
                    query = query.Where(x => x.Published);

                if (publicationState == PublicationState.Unpublished)
                    query = query.Where(x => !x.Published);

                if (storeId > 0)
                    query = await _storeMappingService.ApplyStoreMapping(query, storeId);

                return query;
            });

            return result;
        }

        /// <summary>
        /// Insert slide
        /// </summary>
        /// <param name="slide">Inserting slide</param>
        public virtual async Task InsertSlideAsync(Slide slide)
        {
            await _slideRepository.InsertAsync(slide);
        }

        /// <summary>
        /// Update already exist slide
        /// </summary>
        /// <param name="slide">Updating slide</param>
        public virtual async Task UpdateSlideAsync(Slide slide)
        {
            await _slideRepository.UpdateAsync(slide);
        }

        /// <summary>
        /// Delete slide from database
        /// </summary>
        /// <param name="slide">Deleting slide</param>
        public virtual async Task DeleteSlideAsync(Slide slide)
        {
            //delete slide entity
            await _slideRepository.DeleteAsync(slide);
        }

        #endregion
    }
}
