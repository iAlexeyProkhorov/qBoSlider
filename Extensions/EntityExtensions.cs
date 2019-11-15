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
            return (!slide.StartDateUtc.HasValue || (slide.StartDateUtc.HasValue && slide.StartDateUtc <= DateTime.UtcNow)) &&
                (!slide.EndDateUtc.HasValue || (slide.EndDateUtc.HasValue && slide.EndDateUtc >= DateTime.UtcNow));
        }
    }
}
