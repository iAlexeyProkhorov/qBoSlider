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
