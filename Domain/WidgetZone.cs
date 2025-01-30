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
        /// Gets or sets slideshow autoplay value
        /// </summary>
        public bool Autoplay { get; set; }

        /// <summary>
        /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        public int AutoplayInterval { get; set; } = 3000;

        /// <summary>
        /// Gets or sets slider infinite loop flag.
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// Gets or sets slide image lazy loading sign
        /// </summary>
        public bool LazyLoading { get; set; }

        /// <summary>
        /// Gets or sets space between each slide in pixels, default value is 0
        /// </summary>
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Gets or sets how many slides are displaying at slider view in one moment
        /// </summary>
        public int SlidesPerView { get; set; }

        /// <summary>
        /// Gets or sets slider automtically height calculation sign
        /// </summary>
        public bool AutoHeight { get; set; }

        /// <summary>
        /// Gets or sets arrow navigation displaying type id
        /// </summary>
        public bool AllowArrowNavigation { get; set; }

        /// <summary>
        /// Gets or sets bullet navigation displaying type id
        /// </summary>
        public bool AllowBulletNavigation{ get; set; }

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
