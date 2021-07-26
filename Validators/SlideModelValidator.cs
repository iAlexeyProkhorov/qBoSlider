//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using FluentValidation;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.qBoSlider.Validators
{
    /// <summary>
    /// Represents slide model validator
    /// </summary>
    public class SlideModelValidator : BaseNopValidator<SlideModel>
    {
        public SlideModelValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.PictureId).NotEqual(0).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.PictureId.IsRequired"));
            RuleFor(x => x.EndDateUtc).Must((model, endDate) =>
            {
                if (endDate.HasValue && model.StartDateUtc.HasValue && model.StartDateUtc.Value > endDate.Value)
                    return false;

                return true;
            }).WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Baroque.Widgets.qBoSlider.Admin.Slide.EndDateMustBeLaterThanStartDate"));
        }
    }
}
