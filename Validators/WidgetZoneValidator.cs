using FluentValidation;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.qBoSlider.Validators
{
    /// <summary>
    /// Represents admin widget zone model validator
    /// </summary>
    public class WidgetZoneValidator : BaseNopValidator<WidgetZoneModel>
    {
        public WidgetZoneValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.Name.IsRequired"));
            RuleFor(x => x.SystemName).NotEmpty().WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SystemName.IsRequired"));
            RuleFor(x => x.SlideSpacing).GreaterThan(0).WithMessage(localizationService.GetResource("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.WidgetZone.SlideSpacing.MustBeGreaterThanZero"));
        }
    }
}
