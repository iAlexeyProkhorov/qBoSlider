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

using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents add widget zone for slide editing
    /// </summary>
    public record AddSlideWidgetZoneModel : BaseSearchModel, IWidgetZoneSearchModel
    {
        /// <summary>
        /// Gets or sets slide entity id
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone friendly name
        /// </summary>
        public string SearchWidgetZoneName { get; set; }

        /// <summary>
        /// Gets or sets searching widget zone system name
        /// </summary>
        public string SearchWidgetZoneSystemName { get; set; }

        /// <summary>
        /// Gets or sets selected widget zone id numbers
        /// </summary>
        public IList<int> SelectedWidgetZoneIds { get; set; } = new List<int>();

        public record WidgetZonePagedList: BasePagedListModel<WidgetZoneModel>
        {

        }

        /// <summary>
        /// Represents widget zone model for slide editing
        /// </summary>
        public record WidgetZoneModel : BaseNopEntityModel, ISlideWidgetZoneModel
        {
            /// <summary>
            /// Gets or sets widget zone friendly name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets widget zone system name
            /// </summary>
            public string SystemName { get; set; }

            /// <summary>
            /// Gets or sets value indicating widget zone availability
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
