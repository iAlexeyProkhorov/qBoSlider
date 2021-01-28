using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Represents plugin settings
    /// </summary>
    public class qBoSliderSettings : ISettings
    {
        /// <summary>
        /// Gets or sets value indicating plugin static cache usage
        /// </summary>
        public bool UseStaticCache { get; set; }

        /// <summary>
        /// Gets or sets value indicating static cache clearing after saving widget zone parameters.
        /// </summary>
        public bool ClearCacheAfterWidgetZoneChanging { get; set; }

        /// <summary>
        /// Gets or sets value indicating about static cache clearing after slide saving.
        /// </summary>
        public bool ClearCacheAfterSlideChanging { get; set; }

        /// <summary>
        /// Gets or sets value indicating about static cache clearing after widget zone slide saving.
        /// </summary>
        public bool ClearCacheAfterWidgetZoneSlideChanging { get; set; }
    }
}