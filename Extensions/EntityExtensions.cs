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

using Nop.Plugin.Widgets.qBoSlider.Domain;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Extensions
{
    public static class EntityExtentions
    {
        /// <summary>
        /// Check slide publication date
        /// </summary>
        /// <param name="slide">Slide entity</param>
        /// <returns>'True' when slide should be published</returns>
        public static bool PublishToday(this Slide slide)
        {
            var publish = (!slide.StartDateUtc.HasValue || (slide.StartDateUtc.HasValue && slide.StartDateUtc <= DateTime.UtcNow)) &&
                (!slide.EndDateUtc.HasValue || (slide.EndDateUtc.HasValue && slide.EndDateUtc >= DateTime.UtcNow));

            return publish;
        }
    }
}
