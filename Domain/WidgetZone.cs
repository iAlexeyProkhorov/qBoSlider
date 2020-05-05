using Nop.Core;
using Nop.Core.Domain.Security;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Domain
{
    /// <summary>
    /// Represents slider widget zone
    /// </summary>
    public class WidgetZone : BaseEntity, IAclSupported
    {
        private ICollection<WidgetZoneSlide> _widgetZoneSlides;

        /// <summary>
        /// Gets or sets the widget zone name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets widget zone system name
        /// </summary>
        public string SystemName { get; set; }

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
        /// Gets or sets slideshow autoplay value
        /// </summary>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets arrow navigation displaying type id
        /// </summary>
        public int ArrowNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets arrow navigation displaying type
        /// </summary>
        public NavigationType ArrowNavigationDisplayingType
        {
            get { return (NavigationType)ArrowNavigationDisplayingTypeId; }
            set { ArrowNavigationDisplayingTypeId = (int)value; }
        }

        /// <summary>
        /// Gets or sets bullet navigation displaying type id
        /// </summary>
        public int BulletNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets bullet navigation displaying type
        /// </summary>
        public NavigationType BulletNavigationDisplayingType
        {
            get { return (NavigationType)BulletNavigationDisplayingTypeId; }
            set { BulletNavigationDisplayingTypeId = (int)value; }
        }

        /// <summary>
        /// Gets or sets slide displaying for current widget zone. 'true' - display slider.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets widget zone slides collection
        /// </summary>
        public virtual ICollection<WidgetZoneSlide> WidgetZoneSlides
        {
            get { return _widgetZoneSlides ?? (_widgetZoneSlides = new List<WidgetZoneSlide>()); }
            set { _widgetZoneSlides = value; }
        }
    }
}
