using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents widget zone searching model for slide editing page
    /// </summary>
    public record SlideWidgetZoneSearchModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets slide entity id number
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Represents list model
        /// </summary>
        public record WidgetZonePagedList: BasePagedListModel<WidgetZoneModel>
        {

        }

        public record WidgetZoneModel : BaseNopEntityModel, ISlideWidgetZoneModel
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
