using Nop.Web.Framework.Models;

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
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone system name
        /// </summary>
        public string SystemName { get; set; }

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
        }
    }
}
