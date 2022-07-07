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
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchName")]
        string SearchName { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchWidgetZoneId")]
        int SearchWidgetZoneId { get; set; }

        IList<SelectListItem> AvailableWidgetZones { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchStartDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchStartDateOnUtc { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.SearchFinishDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchFinishDateOnUtc { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.List.PublicationStateId")]
        int SearchPublicationStateId { get; set; }
        IList<SelectListItem> AvailablePublicationStates { get; set; }
    }
}
