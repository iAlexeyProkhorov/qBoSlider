using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone slide model
    /// </summary>
    public record WidgetZoneSlideModel : BaseNopEntityModel, ILocalizedModel<WidgetZoneSlideModel.LocalizationModel>
    {
        /// <summary>
        /// Gets or sets slide picture id number
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZoneSlide.Fields.PictureId")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        /// <summary>
        /// Gets or sets slide picture url
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets slide entity id number
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Gets or sets slide override HTML content. 
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZoneSlide.Fields.OverrideDescription")]
        public string OverrideDescription { get; set; }

        /// <summary>
        /// Gets or sets slide display order
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZoneSlide.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets localized locale models
        /// </summary>
        public IList<LocalizationModel> Locales { get; set; } = new List<LocalizationModel>();

        /// <summary>
        /// Represents widget zone slide overriding HTML content localization model
        /// </summary>
        public class LocalizationModel : ILocalizedLocaleModel
        {
            /// <summary>
            /// Gets or sets slide localized HTML content. 
            /// </summary>
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZoneSlide.Fields.OverrideDescription")]
            public string OverrideDescription { get; set; }

            /// <summary>
            /// Gets or sets the language identifier
            /// </summary>
            public int LanguageId { get; set; }
        }
    }
}
