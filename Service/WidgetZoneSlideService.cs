//    This file is part of qBoSlider.

//    qBoSlider is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    qBoSlider is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with qBoSlider.  If not, see<https://www.gnu.org/licenses/>.

using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Events;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone slide relations service
    /// </summary>
    public class WidgetZoneSlideService : IWidgetZoneSlideService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WidgetZoneSlide> _widgetZoneSlideRepository;

        #endregion

        #region Constructor

        public WidgetZoneSlideService(IEventPublisher eventPublisher,
            IRepository<WidgetZoneSlide> widgetZoneSlideRepository)
        {
            this._eventPublisher = eventPublisher;
            this._widgetZoneSlideRepository = widgetZoneSlideRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get widget zone slide by id
        /// </summary>
        /// <param name="id">Slide entity id number</param>
        /// <returns>Widget zone entity</returns>
        public virtual WidgetZoneSlide GetWidgetZoneSlide(int id)
        {
            return _widgetZoneSlideRepository.GetById(id);
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

            return new PagedList<WidgetZoneSlide>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Insert new widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual void InsertWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide)
        {
            _widgetZoneSlideRepository.Insert(widgetZoneSlide);

            _eventPublisher.EntityInserted(widgetZoneSlide);
        }

        /// <summary>
        /// Update widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual void UpdateWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide)
        {
            _widgetZoneSlideRepository.Update(widgetZoneSlide);

            _eventPublisher.EntityUpdated(widgetZoneSlide);
        }

        /// <summary>
        /// Delete widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        public virtual void DeleteWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide)
        {
            _widgetZoneSlideRepository.Delete(widgetZoneSlide);

            _eventPublisher.EntityDeleted(widgetZoneSlide);
        }

        #endregion
    }
}
