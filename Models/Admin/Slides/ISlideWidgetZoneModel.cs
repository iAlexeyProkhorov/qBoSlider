namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents slide widget model list item interface
    /// </summary>
    public interface ISlideWidgetZoneModel
    {
        /// <summary>
        /// Gets or sets widget zone friendly name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets widget zone system name
        /// </summary>
        string SystemName { get; set; }

        /// <summary>
        /// Gets or sets value indicating widget zone availability
        /// </summary>
        bool Published { get; set; }
    }
}
