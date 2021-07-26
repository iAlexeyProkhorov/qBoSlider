using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin
{
    /// <summary>
    /// Represents plugin configuration model
    /// </summary>
    public record ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        /// <summary>
        /// Gets or sets value indicating plugin static cache usage
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Configuration.UseStaticCache")]
        public bool UseStaticCache { get; set; }

        /// <summary>
        /// Gets or sets value indicating static cache clearing after saving widget zone parameters.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Configuration.ClearCacheAfterWidgetZoneChanging")]
        public bool ClearCacheAfterWidgetZoneChanging { get; set; }

        /// <summary>
        /// Gets or sets value indicating about static cache clearing after slide saving.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Configuration.ClearCacheAfterSlideChanging")]
        public bool ClearCacheAfterSlideChanging { get; set; }

        /// <summary>
        /// Gets or sets value indicating about static cache clearing after widget zone slide saving.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Configuration.ClearCacheAfterWidgetZoneSlideChanging")]
        public bool ClearCacheAfterWidgetZoneSlideChanging { get; set; }
    }
}