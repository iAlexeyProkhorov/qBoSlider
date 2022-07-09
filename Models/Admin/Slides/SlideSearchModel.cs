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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [UIHint("DateTimeNullable")]
        public DateTime? SearchStartDateOnUtc { get; set; }

        [UIHint("DateTimeNullable")]
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
            /// Gets or sets slide search name
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
