using Baroque.Plugin.Widgets.qBoSlider.Domain.Slider;

namespace Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor;

/// <summary>
/// Represents JSSOR slider configurations
/// </summary>
public partial class JssorSliderConfiguration : ISliderConfiguration
{
    /// <summary>
    /// Gets or sets widget zone slides transition effects collection
    /// </summary>
    public string TransitionEffects { get; set; }

    /// <summary>
    /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
    /// </summary>
    public int AutoPlayInterval { get; set; } = 3000;

    /// <summary>
    /// Gets or sets slide displaying time. In milliseconds. Default value is 500.
    /// </summary>
    public int SlideDuration { get; set; } = 500;

    /// <summary>
    /// Gets or sets minimal drag offset to trigger slide, default value is 20
    /// </summary>
    public int MinDragOffsetToSlide { get; set; } = 20;

    /// <summary>
    /// Gets or sets space between each slide in pixels, default value is 0
    /// </summary>
    public int SlideSpacing { get; set; }

    /// <summary>
    /// Gets or sets slider alignment type
    /// </summary>
    public int SliderAlignmentId { get; set; }

    /// <summary>
    /// Gets or sets minimum slider width
    /// </summary>
    public int MinSlideWidgetZoneWidth { get; set; } = 200;

    /// <summary>
    /// Gets or sets maximum slider width
    /// </summary>
    public int MaxSlideWidgetZoneWidth { get; set; } = 1920;

    /// <summary>
    /// Gets or sets slideshow autoplay value
    /// </summary>
    public bool AutoPlay { get; set; }

    /// <summary>
    /// Gets or sets arrow navigation displaying type id
    /// </summary>
    public int ArrowNavigationDisplayingTypeId { get; set; }

    /// <summary>
    /// Gets or sets bullet navigation displaying type id
    /// </summary>
    public int BulletNavigationDisplayingTypeId { get; set; }
}
