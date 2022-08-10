using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents slide model for editing
    /// </summary>
    public record SlideModel : BaseNopEntityModel, ILocalizedModel<SlideLocalizedModel>
    {
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.PictureId")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Hyperlink")]
        public string Hyperlink { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.StartDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDateUtc { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.EndDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDateUtc { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Published")]
        public bool Published { get; set; } = true;

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.LimitedToStores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; } = new List<int>();

        public IList<SelectListItem> AvailableStores { get; set; } = new List<SelectListItem>();

        public IList<SlideLocalizedModel> Locales { get; set; } = new List<SlideLocalizedModel>();

        //1.0.5
        //ACL (customer roles)
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.AclCustomerRoles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; } = new List<int>();

        public IList<SelectListItem> AvailableCustomerRoles { get; set; } = new List<SelectListItem>();

        public SlideWidgetZoneSearchModel WidgetZoneSearchModel { get; set; } = new SlideWidgetZoneSearchModel();
    }

    /// <summary>
    /// Represents slide localization model
    /// </summary>
    public class SlideLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.PictureId")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.Fields.Hyperlink")]
        public string Hyperlink { get; set; }
    }
}
