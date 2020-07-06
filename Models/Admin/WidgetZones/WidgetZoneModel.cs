using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone admin side model
    /// </summary>
    public partial class WidgetZoneModel : BaseNopEntityModel
    {
        /// <summary>
        /// Gets or sets the widget zone name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets widget zone system name
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SystemName")]
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoPlayInterval")]
        public int AutoPlayInterval { get; set; } = 3000;

        /// <summary>
        /// Gets or sets slide displaying time. In milliseconds. Default value is 500.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlideDuration")]
        public int SlideDuration { get; set; } = 500;

        /// <summary>
        /// Gets or sets minimal drag offset to trigger slide, default value is 20
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.MinDragOffsetToSlide")]
        public int MinDragOffsetToSlide { get; set; } = 20;

        /// <summary>
        /// Gets or sets space between each slide in pixels, default value is 0
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlideSpacing")]
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Gets or sets slideshow autoplay value
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoPlay")]
        public bool AutoPlay { get; set; } = true;

        /// <summary>
        /// Gets or sets arrow navigation displaying type id
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.ArrowNavigationDisplayingTypeId")]
        public int ArrowNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets list of available slider arrow navigation types
        /// </summary>
        public IList<SelectListItem> AvailableArrowNavigations { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets bullet navigation displaying type id
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.BulletNavigationDisplayingTypeId")]
        public int BulletNavigationDisplayingTypeId { get; set; }

        /// <summary>
        /// Gets or sets list of available slider bullet navigation types
        /// </summary>
        public IList<SelectListItem> AvailableBulletNavigations { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets slide displaying for current widget zone. 'true' - display slider.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Published")]
        public bool Published { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is subject to ACL
        /// </summary>
        public bool SubjectToAcl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is limited/restricted to certain stores
        /// </summary>
        public bool LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets selected customer role ids
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SelectedCustomerRoleIds")]
        public IList<int> SelectedCustomerRoleIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets available customer roles
        /// </summary>
        public IList<SelectListItem> AvailableCustomerRoles { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets selected store entities ids
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SelectedStoreIds")]
        public IList<int> SelectedStoreIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets available stores
        /// </summary>
        public IList<SelectListItem> AvailableStores { get; set; } = new List<SelectListItem>();
    }
}
