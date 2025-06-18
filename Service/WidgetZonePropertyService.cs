using Baroque.Plugin.Widgets.qBoSlider.Domain;
using Nop.Core;
using Nop.Data;

namespace Baroque.Plugin.Widgets.qBoSlider.Service;

/// <summary>
/// Represents widget zone property service
/// </summary>
public partial class WidgetZonePropertyService : IWidgetZonePropertyService
{
    #region Fields

    private readonly IRepository<WidgetZoneProperty> _widgetZonePropertyRepository;

    #endregion

    #region Constructor

    public WidgetZonePropertyService(IRepository<WidgetZoneProperty> widgetZonePropertyRepository)
    {
        _widgetZonePropertyRepository = widgetZonePropertyRepository;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets list of all searchable widget zone properties
    /// </summary>
    /// <param name="widgetZoneId">Searchable widget zone unique id number</param>
    /// <param name="sliderSystemName">Searchable slider extension system name</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Paage size</param>
    /// <returns>Paged list of widget zone properties</returns>
    public virtual async Task<IPagedList<WidgetZoneProperty>> GetAllWidgetZonePropertiesAsync(int? widgetZoneId = null, string sliderSystemName = null, int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = _widgetZonePropertyRepository.Table;

        if(widgetZoneId.HasValue ) 
            query = query.Where(x => x.WindgetZoneId == widgetZoneId.Value);

        if(!string.IsNullOrEmpty(sliderSystemName) )
            query = query.Where(x => x.SystemName == sliderSystemName);

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }

    /// <summary>
    /// Get widget zone property instance by unique id number
    /// </summary>
    /// <param name="id">Widget zone instance unique id number</param>
    /// <returns></returns>
    public virtual async Task<WidgetZoneProperty> GetWidgetZonePropertyAsync(int id)
    {
        return await _widgetZonePropertyRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Creates new widget zone property instance in database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task InsertWidgetZonePropertyAsync(WidgetZoneProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.InsertAsync(widgetZoneProperty);
    }

    /// <summary>
    /// Updates already existing in database widget zone property instance
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task UpdateWidgetZoneProperty(WidgetZoneProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.UpdateAsync(widgetZoneProperty);
    }

    /// <summary>
    /// Deletes already existing widget zone property from database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task DeleteWidgetZoneProperty(WidgetZoneProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.DeleteAsync(widgetZoneProperty);
    }

    #endregion
}
