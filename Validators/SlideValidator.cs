using FluentValidation;
using Nop.Plugin.Widgets.qBoSlider.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.qBoSlider.Validators
{
    public class SlideValidator : BaseNopValidator<SlideModel>
    {
        public SlideValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.PictureId).NotEqual(0).WithMessage(localizationService.GetResource("Plugins.Widgets.qBoSlider.SlidePictureIsRequired"));
            RuleFor(x => x.EndDateUtc).Must((model, endDate) =>
            {
                if (endDate.HasValue && model.StartDateUtc.HasValue && model.StartDateUtc.Value > endDate.Value)
                    return false;

                return true;
            }).WithMessage(localizationService.GetResource("Plugins.Widgets.qBoSlider.EndDateMustBeLaterThanStartDate"));
        }
    }
}
