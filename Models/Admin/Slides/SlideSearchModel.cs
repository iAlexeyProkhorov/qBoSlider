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

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represent slide search model
    /// </summary>
    public record SlideSearchModel : BaseSearchModel, ISlideSearchModel
    {
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
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchStartDateFromOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchStartDateFromOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search start date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchStartDateToOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchStartDateToOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide finish date search period begin
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchFinishDateFromOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchFinishDateFromOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide  finish date search period end
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchFinishDateToOnUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? SearchFinishDateToOnUtc { get; set; }

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
        /// Represent slide page list model
        /// </summary>
        public record SlidePagedListModel :BasePagedListModel<SlideListItemModel>
        {

        }

        /// <summary>
        /// Represents slide in list
        /// </summary>
        public record SlideListItemModel : BaseNopEntityModel
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
