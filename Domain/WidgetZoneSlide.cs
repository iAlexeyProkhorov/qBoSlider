using Nop.Core;
using Nop.Core.Domain.Localization;

namespace Nop.Plugin.Widgets.qBoSlider.Domain
{
    /// <summary>
    /// Represents widget zone slide relation
    /// </summary>
    public class WidgetZoneSlide : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the widget zone id number
        /// </summary>
        public int WidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets the slide id number
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Gets or sets overrided slide HTML description
        /// </summary>
        public string OverrideDescription { get; set; }

        /// <summary>
        /// Gets or sets slide displaying order for current widget zone
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets slide entity
        /// </summary>
        public virtual Slide Slide { get; set; }

        /// <summary>
        /// Gets or sets the widget zone entity
        /// </summary>
        public virtual WidgetZone WidgetZone { get; set; }
    }
}
