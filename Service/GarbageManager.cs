using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents plugin garbage manager. Allows to remove media content created during administrator activity.
    /// </summary>
    public class GarbageManager : IGarbageManager
    {
        #region Fields

        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;

        #endregion

        #region Constructor

        public GarbageManager(ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService)
        {
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._pictureService = pictureService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete slide base picture from 'Picture' table.
        /// </summary>
        /// <param name="slide">Slide entitiy</param>
        public virtual void DeleteSlidePicture(Slide slide)
        {
            //delete slide base picture
            var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
            if (picture != null)
                _pictureService.DeletePicture(picture);
        }

        /// <summary>
        /// Delete slide all localized resources include slide pictures
        /// </summary>
        /// <param name="slide"></param>
        public virtual void DeleteSlideLocalizedValues(Slide slide)
        {
            var allLanguages = _languageService.GetAllLanguages(true);

            if (allLanguages.Count > 1)
                foreach (var language in allLanguages)
                    DeleteSlideLocalizedValues(slide, language.Id);
        }

        /// <summary>
        /// Delete slide localized resources include slide pictures for special language
        /// </summary>
        /// <param name="slide">Slide entitiy</param>
        /// <param name="languageId">Language entitiy unique id number</param>
        public virtual void DeleteSlideLocalizedValues(Slide slide, int languageId)
        {
            var pictureIdLocalizaedValue = _localizedEntityService.GetLocalizedValue(languageId, slide.Id, "Slide", "PictureId");
            var isPictureValid = int.TryParse(pictureIdLocalizaedValue, out int localizePictureId);

            //delete localized values
            _localizedEntityService.SaveLocalizedValue(slide, x => x.PictureId, null, languageId);
            _localizedEntityService.SaveLocalizedValue(slide, x => x.HyperlinkAddress, null, languageId);
            _localizedEntityService.SaveLocalizedValue(slide, x => x.Description, null, languageId);

            //remove localized picture
            if (!string.IsNullOrEmpty(pictureIdLocalizaedValue) && isPictureValid)
            {
                var localizedPicture = _pictureService.GetPictureById(localizePictureId);

                //go to next picture if current picture aren't exist
                if (localizedPicture == null)
                    return;

                _pictureService.DeletePicture(localizedPicture);
            }
        }

        #endregion
    }
}
