using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Baroque.Plugin.Widgets.qBoSlider.Service.Sliders;

/// <summary>
/// Represents slider interface
/// </summary>
public partial interface ISlider
{
    /// <summary>
    /// Gets or sets slider configuration type
    /// </summary>
    Type SliderConfigurationType { get; }

    /// <summary>
    /// Gets or sets slider configuration url
    /// </summary>
    string GetSliderConfigurationUrl(WidgetZone widgetZone);

    /// <summary>
    /// Gets or sets slider public side url
    /// </summary>
    string GetSliderPublicUrl(WidgetZone widgetZone);
}
