using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor.Models;

/// <summary>
/// Represents jssor slider extension configuration model
/// </summary>
public partial record SliderConfigurationModel : BaseNopModel
{
    /// <summary>
    /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoPlayInterval")]
    public int AutoPlayInterval { get; set; } = 3000;

    /// <summary>
    /// Gets or sets slide displaying time. In milliseconds. Default value is 500.
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlideDuration")]
    public int SlideDuration { get; set; } = 500;

    /// <summary>
    /// Gets or sets minimal drag offset to trigger slide, default value is 20
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.MinDragOffsetToSlide")]
    public int MinDragOffsetToSlide { get; set; } = 20;

    /// <summary>
    /// Gets or sets space between each slide in pixels, default value is 0
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlideSpacing")]
    public int SlideSpacing { get; set; }

    /// <summary>
    /// Gets or sets slider alignment type
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SliderAlignment")]
    public int SliderAlignmentId { get; set; }

    /// <summary>
    /// Gets or sets list of available slider alignments
    /// </summary>
    public SelectList AvailableSliderAlignments { get; set; } = new SelectList(new List<SelectListItem>());

    /// <summary>
    /// Gets or sets minimum slider width
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.MinSlideWidgetZoneWidth")]
    public int MinSlideWidgetZoneWidth { get; set; } = 200;

    /// <summary>
    /// Gets or sets maximum slider width
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.MaxSlideWidgetZoneWidth")]
    public int MaxSlideWidgetZoneWidth { get; set; } = 1920;

    /// <summary>
    /// Gets or sets slideshow autoplay value
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoPlay")]
    public bool AutoPlay { get; set; } = true;

    /// <summary>
    /// Gets or sets arrow navigation displaying type id
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.ArrowNavigationDisplayingTypeId")]
    public int ArrowNavigationDisplayingTypeId { get; set; }

    /// <summary>
    /// Gets or sets list of available slider arrow navigation types
    /// </summary>
    public SelectList AvailableArrowNavigations { get; set; } = new SelectList(new List<SelectListItem>());

    /// <summary>
    /// Gets or sets bullet navigation displaying type id
    /// </summary>
    [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.BulletNavigationDisplayingTypeId")]
    public int BulletNavigationDisplayingTypeId { get; set; }

    /// <summary>
    /// Gets or sets list of available slider bullet navigation types
    /// </summary>
    public SelectList AvailableBulletNavigations { get; set; } = new SelectList(new List<SelectListItem>());
}
