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
using Nop.Services.Localization;
using Nop.Services.Media;
using System.Threading.Tasks;

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
        public virtual async Task DeleteSlidePictureAsync(Slide slide)
        {
            //delete slide base picture
            var picture = await _pictureService.GetPictureByIdAsync(slide.PictureId.GetValueOrDefault(0));
            if (picture != null)
                await _pictureService.DeletePictureAsync(picture);
        }

        /// <summary>
        /// Delete slide all localized resources include slide pictures
        /// </summary>
        /// <param name="slide"></param>
        public virtual async Task DeleteSlideLocalizedValuesAsync(Slide slide)
        {
            var allLanguages = await _languageService.GetAllLanguagesAsync(true);

            if (allLanguages.Count > 1)
                foreach (var language in allLanguages)
                    await DeleteSlideLocalizedValuesAsync(slide, language.Id);
        }

        /// <summary>
        /// Delete slide localized resources include slide pictures for special language
        /// </summary>
        /// <param name="slide">Slide entitiy</param>
        /// <param name="languageId">Language entitiy unique id number</param>
        public virtual async Task DeleteSlideLocalizedValuesAsync(Slide slide, int languageId)
        {
            var pictureIdLocalizaedValue = await _localizedEntityService.GetLocalizedValueAsync(languageId, slide.Id, "Slide", "PictureId");
            var isPictureValid = int.TryParse(pictureIdLocalizaedValue, out int localizePictureId);

            //delete localized values
            _localizedEntityService.SaveLocalizedValueAsync(slide, x => x.PictureId, null, languageId);
            _localizedEntityService.SaveLocalizedValueAsync(slide, x => x.HyperlinkAddress, null, languageId);
            _localizedEntityService.SaveLocalizedValueAsync(slide, x => x.Description, null, languageId);

            //remove localized picture
            if (!string.IsNullOrEmpty(pictureIdLocalizaedValue) && isPictureValid)
            {
                var localizedPicture = await _pictureService.GetPictureByIdAsync(localizePictureId);

                //go to next picture if current picture aren't exist
                if (localizedPicture == null)
                    return;

                _pictureService.DeletePictureAsync(localizedPicture);
            }
        }

        #endregion
    }
}
