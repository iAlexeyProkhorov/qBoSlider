using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone search model
    /// </summary>
    public partial class WidgetZoneSearchModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets searching widget zone friendly name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.List.SearchWidgetZoneName")]
        public string SearchWidgetZoneName { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone system name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.List.SearchWidgetZoneSystemName")]
        public string SearchWidgetZoneSystemName { get; set; }

        /// <summary>
        /// Represents widget zone paged list model
        /// </summary>
        public class WidgetZoneList : BasePagedListModel<WidgetZoneListItem>
        {

        }

        /// <summary>
        /// Represents widget zone paged list item
        /// </summary>
        public class WidgetZoneListItem : BaseNopEntityModel
        {
            /// <summary>
            /// Gets or sets widget zone friendly name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets widget zone system name
            /// </summary>
            public string SystemName { get; set; }

            /// <summary>
            /// Gets or sets value wether indicating widget zone publish state
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
