using Baroque.Plugin.Widgets.qBoSlider.Domain;
using Baroque.Plugin.Widgets.qBoSlider.Domain.Slider;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Baroque.Plugin.Widgets.qBoSlider.Service;

/// <summary>
/// Represents widget zone property service
/// </summary>
public partial class WidgetZoneSliderPropertyService : IWidgetZoneSlidePropertyService
{
    #region Fields

    private readonly IRepository<WidgetZoneSliderProperty> _widgetZonePropertyRepository;

    #endregion

    #region Constructor

    public WidgetZoneSliderPropertyService(IRepository<WidgetZoneSliderProperty> widgetZonePropertyRepository)
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
    public virtual async Task<IPagedList<WidgetZoneSliderProperty>> GetAllWidgetZonePropertiesAsync(int? widgetZoneId = null, string sliderSystemName = null, int pageIndex = 0, int pageSize = int.MaxValue)
    {
        var query = _widgetZonePropertyRepository.Table;

        if(widgetZoneId.HasValue ) 
            query = query.Where(x => x.WindgetZoneId == widgetZoneId.Value);

        if(!string.IsNullOrEmpty(sliderSystemName) )
            query = query.Where(x => x.SystemName == sliderSystemName);

        return await query.ToPagedListAsync(pageIndex, pageSize);
    }

    /// <summary>
    /// Loads slider configuration for widget zone
    /// </summary>
    /// <typeparam name="TSliderConfiguration">Slider extension type</typeparam>
    /// <param name="widgetZone">Widget zone instance</param>
    /// <returns>Widget zone slider configuration instance</returns>
    public virtual async Task<TSliderConfiguration> LoadWidgetZoneSliderConfigurationAsync<TSliderConfiguration>(WidgetZone widgetZone) where TSliderConfiguration : ISliderConfiguration, new()
    {
        var sliderProperties = await GetAllWidgetZonePropertiesAsync(widgetZone.Id, widgetZone.SliderSystemName);
        var configuration = new TSliderConfiguration();

        foreach (var property in typeof(TSliderConfiguration).GetProperties())
        {
            if (!property.CanRead || !property.CanWrite)
                continue;

            var sliderProperty = sliderProperties.FirstOrDefault(x => x.SystemName.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
            if (sliderProperty == null)
                continue;

            property.SetValue(configuration, sliderProperty.Value);
        }

        return configuration;
    }

    /// <summary>
    /// Get widget zone property instance by unique id number
    /// </summary>
    /// <param name="id">Widget zone instance unique id number</param>
    /// <returns></returns>
    public virtual async Task<WidgetZoneSliderProperty> GetWidgetZonePropertyAsync(int id)
    {
        return await _widgetZonePropertyRepository.GetByIdAsync(id);
    }

    /// <summary>
    /// Saves slider configuration into qBoSlider slider properties special storage
    /// </summary>
    /// <typeparam name="TSliderConfiguration">Slider configuration instance</typeparam>
    /// <param name="sliderConfiguration">Slider configuration instance</param>
    /// <returns></returns>
    public virtual async Task SaveWidgetZoneSliderConfigurationAsync<TSliderConfiguration>(TSliderConfiguration configuration, WidgetZone widgetZone) where TSliderConfiguration : ISliderConfiguration, new()
    {
        var configurationType = typeof(TSliderConfiguration);
        foreach (var property in configurationType.GetProperties())
        {
            if (!property.CanRead || !property.CanWrite)
                continue;

            var sliderProperty = _widgetZonePropertyRepository.Table.FirstOrDefault(x => x.WindgetZoneId == widgetZone.Id && x.SliderSystemName == configurationType.FullName && x.SystemName == property.Name);
            if (sliderProperty == null)
                await InsertWidgetZonePropertyAsync(new WidgetZoneSliderProperty()
                {
                    SliderSystemName = typeof(TSliderConfiguration).FullName,
                    WindgetZoneId = widgetZone.Id,
                    SystemName = property.Name,
                    Value = sliderProperty.Value
                });
            else
            {
                sliderProperty.Value = property.GetValue(configuration)?.ToString();
                await UpdateWidgetZoneProperty(sliderProperty);
            }
        }
    }

    /// <summary>
    /// Creates new widget zone property instance in database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task InsertWidgetZonePropertyAsync(WidgetZoneSliderProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.InsertAsync(widgetZoneProperty);
    }

    /// <summary>
    /// Updates already existing in database widget zone property instance
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task UpdateWidgetZoneProperty(WidgetZoneSliderProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.UpdateAsync(widgetZoneProperty);
    }

    /// <summary>
    /// Deletes already existing widget zone property from database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    public virtual async Task DeleteWidgetZoneProperty(WidgetZoneSliderProperty widgetZoneProperty)
    {
        await _widgetZonePropertyRepository.DeleteAsync(widgetZoneProperty);
    }

    #endregion
}
