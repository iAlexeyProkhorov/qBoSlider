using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Public
{
    /// <summary>
    /// Represents slider parameters and widgets for current widget zone
    /// </summary>
    public record WidgetZoneModel : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets arrow navigation type id
        /// </summary>
        public int ArrowNavigation { get; set; }

        /// <summary>
        /// Gets or sets bullet navigation type id
        /// </summary>
        public int BulletNavigation { get; set; }

        /// <summary>
        /// Gets or sets slider play interval
        /// </summary>
        public int AutoPlayInterval { get; set; }

        /// <summary>
        /// Gets or sets slide duration
        /// </summary>
        public int SlideDuration { get; set; }

        /// <summary>
        /// Gets or sets minimal drag offset to slide
        /// </summary>
        public int MinDragOffsetToSlide { get; set; }

        /// <summary>
        /// Gets or sets slides spacing
        /// </summary>
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Gets or sets minimal slider width
        /// </summary>
        public int MinSliderWidth { get; set; }

        /// <summary>
        /// Gets or sets maximal slider width
        /// </summary>
        public int MaxSliderWidth { get; set; }

        /// <summary>
        /// Gets or sets autoplay
        /// </summary>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets widget zone slides
        /// </summary>
        public IList<SlideModel> Slides { get; set; } = new List<SlideModel>();

        #region Nested classes

        /// <summary>
        /// Represents slide model
        /// </summary>
        public record SlideModel : BaseNopEntityModel
        {
            /// <summary>
            /// Gets or sets picture url
            /// </summary>
            public string PictureUrl { get; set; }

            /// <summary>
            /// Gets or sets slider localized hyperlink
            /// </summary>
            public string Hyperlink { get; set; }

            /// <summary>
            /// Gets or sets slider localized description HTML
            /// </summary>
            public string Description { get; set; }
        }

        #endregion
    }
}