using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin
{
    /// <summary>
    /// Represents slide search model
    /// </summary>
    public interface ISlideSearchModel
    {
        /// <summary>
        /// Gets or sets slide search name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchName")]
        string SearchName { get; set; }

        /// <summary>
        /// Gets or sets search slide widget zone 
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchWidgetZoneId")]
        int SearchWidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets available widget zones
        /// </summary>
        IList<SelectListItem> AvailableWidgetZones { get; set; }

        /// <summary>
        /// Gets or sets slide search start date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchStartDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchStartDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search finish date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchFinishDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchFinishDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search publication status
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.PublicationStateId")]
        int SearchPublicationStateId { get; set; }

        /// <summary>
        /// Gets or sets list of available publication states
        /// </summary>
        IList<SelectListItem> AvailablePublicationStates { get; set; }
    }
}
