using Nop.Web.Framework.Models;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone slide search model
    /// </summary>
    public record WidgetZoneSlideSearchModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets widget zone entity unique id number
        /// </summary>
        public int WidgetZoneId { get; set; }

        /// <summary>
        /// Represents slide list item paged list model
        /// </summary>
        public record SlideList : BasePagedListModel<SlideListItem>
        {

        }

        /// <summary>
        /// Represents slide lsit item
        /// </summary>
        public record SlideListItem : BaseNopEntityModel
        {
            /// <summary>
            /// Gets or sets slide background picture URL
            /// </summary>
            public string PictureUrl { get; set; }

            /// <summary>
            /// Gets or sets slide search name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets slide displaying start date
            /// </summary>
            public DateTime? StartDateUtc { get; set; }

            /// <summary>
            /// Gets or sets slide displaying end date
            /// </summary>
            public DateTime? EndDateUtc { get; set; }

            /// <summary>
            /// Gets or sets value indicating slide publish state
            /// </summary>
            public bool Published { get; set; }

            /// <summary>
            /// Gets or sets slide display order
            /// </summary>
            public int DisplayOrder { get; set; }
        }
    }
}
