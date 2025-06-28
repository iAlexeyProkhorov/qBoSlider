using Baroque.Plugin.Widgets.qBoSlider.Domain;
using Baroque.Plugin.Widgets.qBoSlider.Domain.Slider;
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Baroque.Plugin.Widgets.qBoSlider.Service;

/// <summary>
/// Represents widget zone propery service interface
/// </summary>
public partial interface IWidgetZoneSlidePropertyService
{
    /// <summary>
    /// Gets list of all searchable widget zone properties
    /// </summary>
    /// <param name="widgetZoneId">Searchable widget zone unique id number</param>
    /// <param name="sliderSystemName">Searchable slider extension system name</param>
    /// <param name="pageIndex">Page index</param>
    /// <param name="pageSize">Paage size</param>
    /// <returns>Paged list of widget zone properties</returns>
    Task<IPagedList<WidgetZoneSliderProperty>> GetAllWidgetZonePropertiesAsync(int? widgetZoneId = null, string sliderSystemName = null, int pageIndex = 0, int pageSize = int.MaxValue);

    /// <summary>
    /// Loads slider configuration for widget zone
    /// </summary>
    /// <typeparam name="TSliderConfiguration">Slider extension type</typeparam>
    /// <param name="widgetZone">Widget zone instance</param>
    /// <returns>Widget zone slider configuration instance</returns>
    Task<TSliderConfiguration> LoadWidgetZoneSliderConfigurationAsync<TSliderConfiguration>(WidgetZone widgetZone) where TSliderConfiguration : ISliderConfiguration, new();

    /// <summary>
    /// Get widget zone property instance by unique id number
    /// </summary>
    /// <param name="id">Widget zone instance unique id number</param>
    /// <returns></returns>
    Task<WidgetZoneSliderProperty> GetWidgetZonePropertyAsync(int id);


    /// <summary>
    /// Saves slider configuration into qBoSlider slider properties special storage
    /// </summary>
    /// <typeparam name="TSliderConfiguration">Slider configuration instance</typeparam>
    /// <param name="sliderConfiguration">Slider configuration instance</param>
    /// <returns></returns>
    Task SaveWidgetZoneSliderConfigurationAsync<TSliderConfiguration>(TSliderConfiguration configuration, WidgetZone widgetZone) where TSliderConfiguration : ISliderConfiguration, new();

    /// <summary>
    /// Creates new widget zone property instance in database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task InsertWidgetZonePropertyAsync(WidgetZoneSliderProperty widgetZoneProperty);

    /// <summary>
    /// Updates already existing in database widget zone property instance
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task UpdateWidgetZoneProperty(WidgetZoneSliderProperty widgetZoneProperty);

    /// <summary>
    /// Deletes already existing widget zone property from database
    /// </summary>
    /// <param name="widgetZoneProperty">Widget zone property instance</param>
    /// <returns></returns>
    Task DeleteWidgetZoneProperty(WidgetZoneSliderProperty widgetZoneProperty);
}
