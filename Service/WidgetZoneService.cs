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
using Nop.Web.Framework.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone service for main data operations
    /// </summary>
    public class WidgetZoneService : IWidgetZoneService
    {
        #region Fields

        private readonly IRepository<Slide> _slideRepository;
        private readonly IRepository<WidgetZone> _widgetZoneRepository;
        private readonly IRepository<WidgetZoneSlide> _widgetZoneSlideRepository;

        #endregion

        #region Constructor

        public WidgetZoneService(IRepository<Slide> slideRepository,
            IRepository<WidgetZone> widgetZoneRepository,
            IRepository<WidgetZoneSlide> widgetZoneSlideRepository)
        {
            this._slideRepository = slideRepository;
            this._widgetZoneRepository = widgetZoneRepository;
            this._widgetZoneSlideRepository = widgetZoneSlideRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get widget zone by id number
        /// </summary>
        /// <param name="id">Widget zone id number</param>
        /// <returns>Widget zone</returns>
        public virtual async Task<WidgetZone> GetWidgetZoneByIdAsync(int id)
        {
            return await _widgetZoneRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Get widget zone by system name
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <returns>Widget zone entity</returns>
        public virtual WidgetZone GetWidgetZoneBySystemName(string systemName)
        {
            return _widgetZoneRepository.Table.FirstOrDefault(x => x.SystemName.Equals(systemName) && x.Published);
        }

        /// <summary>
        /// Get list of all existing nopCommerce widget zones
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of widget zone system names</returns>
        public virtual IPagedList<string> GetNopCommerceWidgetZones(string systemName = null, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var publicWidgetZoneProperties = typeof(PublicWidgetZones).GetProperties();
            var publicWidgetZones = publicWidgetZoneProperties.Select(property =>
            {
                var value = property.GetValue(null);
                return value != null ? value.ToString() : string.Empty;
            }).Where(x => !string.IsNullOrEmpty(x) && x.Contains(systemName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            return new PagedList<string>(publicWidgetZones, pageIndex, pageSize);
        }

        /// <summary>
        /// Get widget zones collection
        /// </summary>
        /// <param name="name">Widget zone name</param>
        /// <param name="systemName">Widget zone system name</param>
        /// <param name="showHidden">GEt only published widget zones</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zones collection</returns>
        public virtual IPagedList<WidgetZone> GetWidgetZones(string name = null, string systemName = null, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _widgetZoneRepository.Table;

            //filter widget zones by zone name
            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            //filter widget zones by system name
            if (!string.IsNullOrEmpty(systemName))
                query = query.Where(x => x.SystemName.Contains(systemName));

            //display only published widget zones
            if (!showHidden)
                query = query.Where(x => x.Published);

            query = query.OrderBy(x => x.Id);

            return new PagedList<WidgetZone>(query.ToList(), pageIndex, pageSize);
        }

        /// <summary>
        /// Get widget zone slides
        /// </summary>
        /// <param name="widgetZoneId">Widget zone id number</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zone slides</returns>
        public virtual IPagedList<Slide> GetWidgetZoneSlides(int widgetZoneId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = (from zoneSlide in _widgetZoneSlideRepository.Table
                        join slide in _slideRepository.Table on zoneSlide.SlideId equals slide.Id
                        join zone in _widgetZoneRepository.Table on zoneSlide.WidgetZoneId equals zone.Id
                        where zoneSlide.WidgetZoneId == widgetZoneId && slide.Published && zone.Published
                        orderby zoneSlide.DisplayOrder
                        select slide).ToList();

            return new PagedList<Slide>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Insert new widget zone in database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual async Task InsertWidgetZoneAsync(WidgetZone widgetZone)
        {
            await _widgetZoneRepository.InsertAsync(widgetZone);
        }

        /// <summary>
        /// Update already existing widget zone entity
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual async Task UpdateWidgetZoneAsync(WidgetZone widgetZone)
        {
            await _widgetZoneRepository.UpdateAsync(widgetZone);
        }

        /// <summary>
        /// Delete already existing widget zone entity from database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual async Task DeleteWidgetZoneAsync(WidgetZone widgetZone)
        {
            await _widgetZoneRepository.DeleteAsync(widgetZone);
        }

        #endregion
    }
}
