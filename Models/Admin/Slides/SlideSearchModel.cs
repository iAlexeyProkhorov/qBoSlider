using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represent slide search model
    /// </summary>
    public class SlideSearchModel : BaseSearchModel, ISlideSearchModel
    {
        public string SearchName { get; set; }

        public int SearchWidgetZoneId { get; set; }

        public IList<SelectListItem> AvailableWidgetZones { get; set; } = new List<SelectListItem>();

        public DateTime? SearchStartDateOnUtc { get; set; }

        public DateTime? SearchFinishDateOnUtc { get; set; }

        public int SearchPublicationStateId { get; set; }
        public IList<SelectListItem> AvailablePublicationStates { get; set; } = new List<SelectListItem>();

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
            /// Gets or sets slide name
            /// </summary>
            public string Name { get; set; }

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
            /// Gest or sets slide published or not
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
