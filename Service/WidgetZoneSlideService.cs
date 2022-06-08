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
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone slide relations service
    /// </summary>
    public class WidgetZoneSlideService : IWidgetZoneSlideService
    {
        #region Fields

        private readonly IRepository<WidgetZoneSlide> _widgetZoneSlideRepository;

        #endregion

        #region Constructor

        public WidgetZoneSlideService(IRepository<WidgetZoneSlide> widgetZoneSlideRepository)
        {
            _widgetZoneSlideRepository = widgetZoneSlideRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get widget zone slide by id
        /// </summary>
        /// <param name="id">Slide entity id number</param>
        /// <returns>Widget zone entity</returns>
        public virtual async Task<WidgetZoneSlide> GetWidgetZoneSlideAsync(int id)
        {
            return await _widgetZoneSlideRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Get widget zones slide relations
        /// </summary>
        /// <param name="widgetZoneId">Widget zone id number</param>
        /// <param name="slideId">Slide id number</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zone slides collection</returns>
        public virtual IPagedList<WidgetZoneSlide> GetWidgetZoneSlides(int? widgetZoneId = null, int? slideId = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _widgetZoneSlideRepository.Table;

            //filter mappings by widget zone
            if (widgetZoneId.HasValue)
                query = query.Where(x => x.WidgetZoneId == widgetZoneId.Value);

            //filter mappings by slide
            if (slideId.HasValue)
                query = query.Where(x => x.SlideId == slideId.Value);

            query = query.OrderBy(x => x.Id);

            return new PagedList<WidgetZoneSlide>(query.ToList(), pageIndex, pageSize);
        }

        /// <summary>
        /// Insert new widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual async Task InsertWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide)
        {
            await _widgetZoneSlideRepository.InsertAsync(widgetZoneSlide);
        }

        /// <summary>
        /// Update widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual async Task UpdateWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide)
        {
            await _widgetZoneSlideRepository.UpdateAsync(widgetZoneSlide);
        }

        /// <summary>
        /// Delete widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual async Task DeleteWidgetZoneSlideAsync(WidgetZoneSlide widgetZoneSlide)
        {
            await _widgetZoneSlideRepository.DeleteAsync(widgetZoneSlide);
        }

        #endregion
    }
}
