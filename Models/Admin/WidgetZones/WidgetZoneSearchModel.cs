using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone search model
    /// </summary>
    public partial record WidgetZoneSearchModel : BaseSearchModel, IWidgetZoneSearchModel
    {
        /// <summary>
        /// Gets or sets searching widget zone friendly name
        /// </summary>
        public string SearchWidgetZoneName { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone system name
        /// </summary>
        public string SearchWidgetZoneSystemName { get; set; }

        /// <summary>
        /// Represents widget zone paged list model
        /// </summary>
        public record WidgetZoneList : BasePagedListModel<WidgetZoneListItem>
        {

        }

        /// <summary>
        /// Represents widget zone paged list item
        /// </summary>
        public record WidgetZoneListItem : BaseNopEntityModel
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
