using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones
{
    /// <summary>
    /// Represents widget zone admin side model
    /// </summary>
    public partial record WidgetZoneModel : BaseNopEntityModel
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
        /// Gets or sets slider autoplay flag
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Autoplay")]
        public bool Autoplay { get; set; }

        /// <summary>
        /// Gets or sets interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoplayInterval")]
        public int AutoplayInterval { get; set; } = 3000;

        /// <summary>
        /// Gets or sets arrow navigation displaying type id
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AllowArrowNavigation")]
        public bool AllowArrowNavigation { get; set; }

        /// <summary>
        /// Gets or sets bullet navigation displaying type id
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AllowBulletNavigation")]
        public bool AllowBulletNavigation { get; set; }

        /// <summary>
        /// Gets or sets slider looping
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Loop")]
        public bool Loop { get; set; }

        /// <summary>
        /// Gets or sets slides content lazy loading
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.LazyLoading")]
        public bool LazyLoading { get; set; }

        /// <summary>
        /// Gets or sets slider wrapper height adaptation to the height of the currently active slide
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.AutoHeight")]
        public bool AutoHeight { get; set; }

        /// <summary>
        /// Gets or sets space between nearest slides at view
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlideSpacing")]
        public int SlideSpacing { get; set; }

        /// <summary>
        /// Gets or sets how many slides are displaying at slider view in one moment
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlidesPerView")]
        public int SlidesPerView { get; set; } = 1;

        /// <summary>
        /// Gets or sets slides per group authomatical calculation. This param intended to be used only with slidesPerView: 'auto' and slidesPerGroup: 1. When enabled, it will skip all slides in view on .slideNext() & .slidePrev() methods calls, on Navigation "buttons" clicks and in autoplay.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlidesPerGroupAuto")]
        public bool SlidesPerGroupAuto { get; set; }

        /// <summary>
        /// Gets or sets numbers of slides to define and enable group sliding. Useful to use with slidesPerView > 1
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.SlidesPerGroup")]
        public int SlidesPerGroup { get; set; } = 1;

        /// <summary>
        /// Gets or sets slide displaying for current widget zone. 'true' - display slider.
        /// </summary>
        [NopResourceDisplayName("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Fields.Published")]
        public bool Published { get; set; }

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

        /// <summary>
        /// Gets or sets slide search model
        /// </summary>
        public WidgetZoneSlideSearchModel SlideSearchModel { get; set; } = new WidgetZoneSlideSearchModel();
    }
}
