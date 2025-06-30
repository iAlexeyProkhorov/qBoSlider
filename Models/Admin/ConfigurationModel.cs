using Microsoft.AspNetCore.Mvc.Rendering;
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
        /// Gets or sets selected slider system name
        /// </summary>
        public string SelectedDefaultSliderSystemName { get; set; }

        /// <summary>
        /// Gets or sets list of available sliders list
        /// </summary>
        public IList<SelectListItem> AvailableSliders { get; set; } = new List<SelectListItem>();
    }
}