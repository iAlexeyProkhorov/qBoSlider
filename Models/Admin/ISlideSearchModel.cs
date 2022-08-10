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
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchName")]
        string SearchName { get; set; }

        /// <summary>
        /// Gets or sets search slide widget zone 
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchWidgetZoneId")]
        int SearchWidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets available widget zones
        /// </summary>
        IList<SelectListItem> AvailableWidgetZones { get; set; }

        /// <summary>
        /// Gets or sets slide search start date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchStartDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchStartDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search finish date
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.SearchFinishDateOnUtc")]
        [UIHint("DateTimeNullable")]
        DateTime? SearchFinishDateOnUtc { get; set; }

        /// <summary>
        /// Gets or sets slide search publication status
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideSearch.PublicationStateId")]
        int SearchPublicationStateId { get; set; }

        /// <summary>
        /// Gets or sets list of available publication states
        /// </summary>
        IList<SelectListItem> AvailablePublicationStates { get; set; }
    }
}
