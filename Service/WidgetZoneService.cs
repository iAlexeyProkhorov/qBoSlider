using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Events;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone service for main data operations
    /// </summary>
    public class WidgetZoneService : IWidgetZoneService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<WidgetZone> _widgetZoneRepository;

        #endregion

        #region Constructor

        public WidgetZoneService(IEventPublisher eventPublisher,
            IRepository<WidgetZone> widgetZoneRepository)
        {
            this._eventPublisher = eventPublisher;
            this._widgetZoneRepository = widgetZoneRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get widget zone by id number
        /// </summary>
        /// <param name="id">Widget zone id number</param>
        /// <returns>Widget zone</returns>
        public virtual WidgetZone GetWidgetZoneById(int id)
        {
            return _widgetZoneRepository.GetById(id);
        }

        /// <summary>
        /// Get widget zone by system name
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <returns>Widget zone entity</returns>
        public virtual WidgetZone GetWidgetZoneBySystemName(string systemName)
        {
            return _widgetZoneRepository.Table.FirstOrDefault(x => x.SystemName.Equals(systemName, StringComparison.InvariantCultureIgnoreCase));
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

            return new PagedList<WidgetZone>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Insert new widget zone in database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual void InsertWidgetZone(WidgetZone widgetZone)
        {
            _widgetZoneRepository.Insert(widgetZone);

            _eventPublisher.EntityInserted(widgetZone);
        }

        /// <summary>
        /// Update already existing widget zone entity
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual void UpdateWidgetZone(WidgetZone widgetZone)
        {
            _widgetZoneRepository.Update(widgetZone);

            _eventPublisher.EntityUpdated(widgetZone);
        }

        /// <summary>
        /// Delete already existing widget zone entity from database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual void DeleteWidgetZone(WidgetZone widgetZone)
        {
            _widgetZoneRepository.Delete(widgetZone);

            _eventPublisher.EntityDeleted(widgetZone);
        }

        #endregion
    }
}
