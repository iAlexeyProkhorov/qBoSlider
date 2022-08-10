using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents add widget zone slide model
    /// </summary>
    public record AddWidgetZoneSlideModel : BaseSearchModel, ISlideSearchModel
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
        /// Gets or sets slide search name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchName")]
        public string SearchName { get; set; }

        /// <summary>
        /// Gets or sets search slide widget zone 
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchWidgetZoneId")]
        public int SearchWidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets available widget zones
        /// </summary>
        public IList<SelectListItem> AvailableWidgetZones { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets slide search start date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchStartDateOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchStartDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search finish date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchFinishDateOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchFinishDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search publication status
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.PublicationStateId")]
        public int SearchPublicationStateId { get; set; }

        /// <summary>
        /// Gets or sets list of available publication states
        /// </summary>
        public IList<SelectListItem> AvailablePublicationStates { get; set; } = new List<SelectListItem>();

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
            /// Gets or sets slide search name
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.AddWidgetZoneSlide.Fields.Name")]
            public string Name { get; set; }

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
