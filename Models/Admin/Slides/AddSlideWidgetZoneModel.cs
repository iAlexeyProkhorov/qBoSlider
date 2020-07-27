using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents add widget zone for slide editing
    /// </summary>
    public class AddSlideWidgetZoneModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets slide entity id
        /// </summary>
        public int SlideId { get; set; }

        public class WidgetZonePagedList: BasePagedListModel<WidgetZoneModel>
        {

        }

        /// <summary>
        /// Represents widget zone model for slide editing
        /// </summary>
        public class WidgetZoneModel : BaseNopEntityModel
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
            /// Gets or sets value indicating widget zone availability
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
