using Baroque.Plugin.Widgets.qBoSlider.Domain;
using Nop.Core;

namespace Baroque.Plugin.Widgets.qBoSlider.Service;

/// <summary>
/// Represents widget zone propery service interface
/// </summary>
public partial interface IWidgetZonePropertyService
{
    /// <summary>
    /// Gets list of all searchable widget zone properties
    /// </summary>
    /// <param name="widgetZoneId">Searchable widget zone unique id number</param>
    /// <param name="sliderSystemName">Searchable slider extension system name</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Paage size</param>
    /// <returns>Paged list of widget zone properties</returns>
    Task<IPagedList<WidgetZoneProperty>> GetAllWidgetZonePropertiesAsync(int? widgetZoneId = null, string sliderSystemName = null, int pageIndex = 0, int pageSize = int.MaxValue);

    /// <summary>
    /// Get widget zone property instance by unique id number
    /// </summary>
    /// <param name="id">Widget zone instance unique id number</param>
    /// <returns></returns>
    Task<WidgetZoneProperty> GetWidgetZonePropertyAsync(int id);

    /// <summary>
    /// Creates new widget zone property instance in database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task InsertWidgetZonePropertyAsync(WidgetZoneProperty widgetZoneProperty);

    /// <summary>
    /// Updates already existing in database widget zone property instance
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task UpdateWidgetZoneProperty(WidgetZoneProperty widgetZoneProperty);

    /// <summary>
    /// Deletes already existing widget zone property from database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task DeleteWidgetZoneProperty(WidgetZoneProperty widgetZoneProperty);
}
