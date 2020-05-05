using Nop.Web.Framework.Models;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin
{
    /// <summary>
    /// Represent slide search model
    /// </summary>
    public class SlideSearchModel : BaseSearchModel
    {
        /// <summary>
        /// Represent slide page list model
        /// </summary>
        public class SlidePagedListModel :BasePagedListModel<SlideListItemModel>
        {

        }

        /// <summary>
        /// Represents slide in list
        /// </summary>
        public class SlideListItemModel : BaseNopEntityModel
        {
            /// <summary>
            /// Gets or sets slide picture Url
            /// </summary>
            public string Picture { get; set; }

            /// <summary>
            /// Gets or sets slide hyperlink
            /// </summary>
            public string Hyperlink { get; set; }

            /// <summary>
            /// Gets or sets slide displaying start date
            /// </summary>
            public DateTime? StartDateUtc { get; set; }

            /// <summary>
            /// Gets or sets slide displaying end date
            /// </summary>
            public DateTime? EndDateUtc { get; set; }

            /// <summary>
            /// Gets or sets slide display order
            /// </summary>
            public int DisplayOrder { get; set; }

            /// <summary>
            /// Gest or sets slide published or not
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
