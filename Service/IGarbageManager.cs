using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents plugin garbage manager. Allows to remove media content created during administrator activity.
    /// </summary>
    public interface IGarbageManager
    {
        /// <summary>
        /// Delete slide base picture from 'Picture' table.
        /// </summary>
        /// <param name="slide">Slide entitiy</param>
        void DeleteSlidePicture(Slide slide);

        /// <summary>
        /// Delete slide all localized resources include slide pictures
        /// </summary>
        /// <param name="slide"></param>
        void DeleteSlideLocalizedValues(Slide slide);

        /// <summary>
        /// Delete slide localized resources include slide pictures for special language
        /// </summary>
        /// <param name="slide">Slide entitiy</param>
        /// <param name="languageId">Language entitiy unique id number</param>
        void DeleteSlideLocalizedValues(Slide slide, int languageId);
    }
}
