using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// Gets or sets selected slider system name
        /// </summary>
        public string SelectedSliderSystemName { get; set; }

        /// <summary>
        /// Gets or sets list of available sliders list
        /// </summary>
        public IList<SelectListItem> AvailableSliders { get; set; } = new List<SelectListItem>();
    }
}