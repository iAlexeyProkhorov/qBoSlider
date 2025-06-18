using Nop.Core;
using Nop.Core.Domain.Localization;

namespace Baroque.Plugin.Widgets.qBoSlider.Domain;

/// <summary>
/// Represents properties for specified 'WidgetZone' slider
/// </summary>
public partial class WidgetZoneProperty : BaseEntity, ILocalizedEntity
{
    /// <summary>
    /// Gets or sets widget zone unique id number
    /// </summary>
    public int WindgetZoneId { get; set; }

    /// <summary>
    /// Gets or sets slider extension system name
    /// </summary>
    public string SliderSystemName { get; set; }

    /// <summary>
    /// Gets or sets slider property system name
    /// </summary>
    public string SystemName { get; set; }

    /// <summary>
    /// Gets or sets slider property value
    /// </summary>
    public string Value { get; set; }
}
