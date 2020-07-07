using FluentValidation;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.qBoSlider.Validators
{
    /// <summary>
    /// Represents admin widget zone model validator
    /// </summary>
    public class WidgetZoneModelValidator : BaseNopValidator<WidgetZoneModel>
    {
        public WidgetZoneModelValidator(ILocalizationService localizationService)
        {
            //validate widget zone properties
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Name.IsRequired"));
            RuleFor(x => x.SystemName).NotEmpty().WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SystemName.IsRequired"));
            //validate slider properties
            RuleFor(x => x.AutoPlayInterval).GreaterThan(0).WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.AutoPlayInterval.MustBeGreaterThanZero"));
            RuleFor(x => x.SlideDuration).GreaterThan(0).WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SlideDuration.MustBeGreaterThanZero"));
            RuleFor(x => x.MinDragOffsetToSlide).GreaterThan(0).WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.MinDragOffsetToSlide.MustBeGreaterThanZero"));
            RuleFor(x => x.SlideSpacing).GreaterThanOrEqualTo(0).WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SlideSpacing.MustBeGreaterThanOrEqualsZero"));
        }
    }
}
