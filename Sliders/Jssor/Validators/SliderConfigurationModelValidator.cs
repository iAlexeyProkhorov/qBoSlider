using Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor.Models;
using FluentValidation;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor.Validators;

/// <summary>
/// Represents Jssor slider configuration model validator
/// </summary>
public partial class SliderConfigurationModelValidator : BaseNopValidator<SliderConfigurationModel>
{
    public SliderConfigurationModelValidator(ILocalizationService localizationService)
    {
        //validate slider properties
        RuleFor(x => x.AutoPlayInterval).GreaterThan(0).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.AutoPlayInterval.MustBeGreaterThanZero"));
        RuleFor(x => x.SlideDuration).GreaterThan(0).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SlideDuration.MustBeGreaterThanZero"));
        RuleFor(x => x.MinDragOffsetToSlide).GreaterThan(0).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.MinDragOffsetToSlide.MustBeGreaterThanZero"));
        RuleFor(x => x.SlideSpacing).GreaterThanOrEqualTo(0).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SlideSpacing.MustBeGreaterThanOrEqualsZero"));
        RuleFor(x => x.MinSlideWidgetZoneWidth).GreaterThanOrEqualTo(200).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.MinSlideWidgetZoneWidth.MustBeGreaterThan200"));
        RuleFor(x => x.MaxSlideWidgetZoneWidth).Must((model, maxWidth) =>
        {
            return model.MaxSlideWidgetZoneWidth > model.MinSlideWidgetZoneWidth;
        }).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.MaxSlideWidgetZoneWidth.MustBeGreaterThanMinimumWidth"));
        RuleFor(x => x.MaxSlideWidgetZoneWidth).Must((model, maxWidth) =>
        {
            return model.MaxSlideWidgetZoneWidth - model.MinSlideWidgetZoneWidth >= 200;
        }).WithMessage(string.Format(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.MaxSlideWidgetZoneWidth.MustBeGreaterThanMinimumValueOnXPixels").Result, 200));
    }
}
