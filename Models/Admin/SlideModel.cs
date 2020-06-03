using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin
{
    public partial class SlideModel : BaseNopEntityModel, ILocalizedModel<SlideLocalizedModel>
    {
        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Picture")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Hyperlink")]
        public string Hyperlink { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.StartDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? StartDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.EndDateUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? EndDateUtc { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.LimitedToStores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.AvailableStores")]
        public IList<SelectListItem> AvailableStores { get; set; }

        public IList<SlideLocalizedModel> Locales { get; set; }

        public SlideModel()
        {
            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            Locales = new List<SlideLocalizedModel>();

            Published = true;

            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();
        }

        //1.0.5
        //ACL (customer roles)
        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.AclCustomerRoles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }
    }

    public class SlideLocalizedModel : ILocalizedLocaleModel
    {
        public int LanguageId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Picture")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Description")]
        public string Description { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.Hyperlink")]
        public string Hyperlink { get; set; }
    }
}
