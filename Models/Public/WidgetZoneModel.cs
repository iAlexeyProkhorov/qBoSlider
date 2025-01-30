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
        /// Gets or sets slideshow autoplay value
        /// </summary>
        public bool Autoplay { get; set; }

        /// <summary>
        /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        public int AutoplayInterval { get; set; }

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
        public bool AllowBulletNavigation { get; set; }

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