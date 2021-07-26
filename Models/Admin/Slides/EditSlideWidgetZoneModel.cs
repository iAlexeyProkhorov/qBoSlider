using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents slide widget zone editing model
    /// </summary>
    public record EditSlideWidgetZoneModel : BaseNopEntityModel, ILocalizedModel<EditSlideWidgetZoneModel.LocalizationModel>
    {
        /// <summary>
        /// Gets or sets slide id number
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Gets or sets widget zone id number
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideWidgetZone.Fields.WidgetZoneId")]
        public int WidgetZoneId { get; set; }

        /// <summary>
        /// Gets or sets list of available widget zones
        /// </summary>
        public IList<SelectListItem> AvailableWidgetZones { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets overrideble HTML description
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideWidgetZone.Fields.OverrideDescription")]
        public string OverrideDescription { get; set; }

        /// <summary>
        /// Gets or sets slide widget zone localized values
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
            [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.SlideWidgetZone.Fields.OverrideDescription")]
            public string OverrideDescription { get; set; }

            /// <summary>
            /// Gets or sets the language identifier
            /// </summary>
            public int LanguageId { get; set; }
        }
    }
}
