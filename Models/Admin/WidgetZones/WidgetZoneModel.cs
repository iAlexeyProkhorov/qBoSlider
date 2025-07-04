using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone admin side model
    /// </summary>
    public partial record WidgetZoneModel : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets the widget zone name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets widget zone system name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SystemName")]
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets slider extension system name
        /// </summary>
        public string SliderSystemName { get; set; }

        /// <summary>
        /// Gets or sets slide displaying for current widget zone. 'true' - display slider.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Published")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets selected customer role ids
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SelectedCustomerRoleIds")]
        public IList<int> SelectedCustomerRoleIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets available customer roles
        /// </summary>
        public IList<SelectListItem> AvailableCustomerRoles { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets selected store entities ids
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SelectedStoreIds")]
        public IList<int> SelectedStoreIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets available stores
        /// </summary>
        public IList<SelectListItem> AvailableStores { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets slide search model
        /// </summary>
        public WidgetZoneSlideSearchModel SlideSearchModel { get; set; } = new WidgetZoneSlideSearchModel();
    }
}
