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

using Nop.Core;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;

namespace Nop.Plugin.Widgets.qBoSlider.Domain
{
    /// <summary>
    /// Represents slider widget zone
    /// </summary>
    public class WidgetZone : BaseEntity, IAclSupported, IStoreMappingSupported
    {
        /// <summary>
        /// Gets or sets the widget zone name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets widget zone system name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets widget zone slides transition effects collection
        /// </summary>
        public string TransitionEffects { get; set; }

        /// <summary>
        /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        public int AutoPlayInterval { get; set; } = 3000;

        /// <summary>
        /// Gets or sets slide displaying time. In milliseconds. Default value is 500.
        /// </summary>
        public int SlideDuration { get; set; } = 500;

        /// <summary>
        /// Gets or sets minimal drag offset to trigger slide, default value is 20
        /// </summary>
        public int MinDragOffsetToSlide { get; set; } = 20;

        /// <summary>
        /// Gets or sets space between each slide in pixels, default value is 0
        /// </summary>
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Gets or sets slider alignment type
        /// </summary>
        public int SliderAlignmentId { get; set; }

        /// <summary>
        /// Gets or sets minimum slider width
        /// </summary>
        public int MinSlideWidgetZoneWidth { get; set; } = 200;

        /// <summary>
        /// Gets or sets maximum slider width
        /// </summary>
        public int MaxSlideWidgetZoneWidth { get; set; } = 1920;

        /// <summary>
        /// Gets or sets slideshow autoplay value
        /// </summary>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets arrow navigation displaying type id
        /// </summary>
        public int ArrowNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets bullet navigation displaying type id
        /// </summary>
        public int BulletNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets slide displaying for current widget zone. 'true' - display slider.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }
    }
}
