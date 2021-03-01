using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin
{
    /// <summary>
    /// Represents widget zone search model. 
    /// Contains fields for widget zone searching by name and by system name
    /// </summary>
    public interface IWidgetZoneSearchModel
    {
        /// <summary>
        /// Gets or sets searching widget zone friendly name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.List.SearchWidgetZoneName")]
        string SearchWidgetZoneName { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone system name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.List.SearchWidgetZoneSystemName")]
        string SearchWidgetZoneSystemName { get; set; }
    }
}
