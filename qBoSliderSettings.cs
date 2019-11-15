
using Nop.Core.Configuration;

namespace Nop.Plugin.Widgets.qBoSlider
{
    public class qBoSliderSettings : ISettings
    {
        /// <summary>
        /// Select a widget zone name for slider display
        /// </summary>
        public string WidgetZoneName { get; set; }

        /// <summary>
        /// Interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        public int AutoPlayInterval { get; set; }

        /// <summary>
        /// Specifies default duration (swipe) for slide in milliseconds, default value is 500
        /// </summary>
        public int SlideDuration { get; set; }

        /// <summary>
        ///  Minimum drag offset to trigger slide , default value is 20
        /// </summary>
        public int MinDragOffsetToSlide { get; set; }

        /// <summary>
        /// Space between each slide in pixels, default value is 0
        /// </summary>
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Whether to auto play, to enable slideshow, this option must be set to true, default value is false
        /// </summary>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Display arrows navigation
        /// </summary>
        public int ArrowNavigationDisplay { get; set; }

        /// <summary>
        /// display bullet navigation
        /// </summary>
        public int BulletNavigationDisplay { get; set; }
    }
}