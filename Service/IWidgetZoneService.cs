using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone service interface
    /// </summary>
    public interface IWidgetZoneService
    {
        /// <summary>
        /// Get widget zone by id number
        /// </summary>
        /// <param name="id">Widget zone id number</param>
        /// <returns>Widget zone</returns>
        WidgetZone GetWidgetZoneById(int id);

        /// <summary>
        /// Get widget zone by system name
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <returns>Widget zone entity</returns>
        WidgetZone GetWidgetZoneBySystemName(string systemName);

        /// <summary>
        /// Get widget zones collection
        /// </summary>
        /// <param name="name">Widget zone name</param>
        /// <param name="systemName">Widget zone system name</param>
        /// <param name="showHidden">GEt only published widget zones</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zones collection</returns>
        IPagedList<WidgetZone> GetWidgetZones(string name = null, string systemName = null, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Insert new widget zone in database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        void InsertWidgetZone(WidgetZone widgetZone);

        /// <summary>
        /// Update already existing widget zone entity
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        void UpdateWidgetZone(WidgetZone widgetZone);

        /// <summary>
        /// Delete already existing widget zone entity from database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        void DeleteWidgetZone(WidgetZone widgetZone);
    }
}
