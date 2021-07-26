using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents add widget zone slide model
    /// </summary>
    public record AddWidgetZoneSlideModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets widget zone unique id number
        /// </summary>
        public int WidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets seleceted slides id numbers
        /// </summary>
        public IList<int> SelecetedSlideIds { get; set; } = new List<int>();

        /// <summary>
        /// Represents slide list model
        /// </summary>
        public record SlidePagedListModel : BasePagedListModel<SlideModel>
        {

        }

        /// <summary>
        /// Represents slide for slide searching
        /// </summary>
        public record SlideModel : BaseNopEntityModel
        {
            /// <summary>
            /// Gets or sets slide background picture url
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.AddWidgetZoneSlide.Fields.PictureUrl")]
            public string PictureUrl { get; set; }

            /// <summary>
            /// Gets or sets slide publishing start date 
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.AddWidgetZoneSlide.Fields.StartDateUtc")]
            public DateTime? StartDateUtc { get; set; }

            /// <summary>
            /// Gets or sets slide publishing end date
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.AddWidgetZoneSlide.Fields.EndDateUtc")]
            public DateTime? EndDateUtc { get; set; }

            /// <summary>
            /// Gets or sets slide publish value
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.AddWidgetZoneSlide.Fields.Published")]
            public bool Published { get; set; }
        }
    }
}
