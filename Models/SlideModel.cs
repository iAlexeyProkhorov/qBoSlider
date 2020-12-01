//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.


using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models
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

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.CreateOrEdit.DisplayOrder")]
        public int DisplayOrder { get; set; }

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
