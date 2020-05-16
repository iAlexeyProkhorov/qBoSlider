using System;
using System.Linq;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Data;
using Nop.Services.Media;
using Nop.Services.Stores;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    public partial class SlideService : ISlideService
    {
        #region Fields

        private readonly IRepository<Slide> _slideRepository;

        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IStoreMappingService _storeMappingService;

        #endregion

        #region Constructor

        public SlideService(IRepository<Slide> slideRepository,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureSerivce,
            IStoreMappingService storeMappingService)
        {
            this._slideRepository = slideRepository;

            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._pictureService = pictureSerivce;
            this._storeMappingService = storeMappingService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get slide by individual id number
        /// </summary>
        /// <param name="id">Slide individual number</param>
        /// <returns>Slide</returns>
        public virtual Slide GetSlideById(int id)
        {
            return _slideRepository.GetById(id);
        }

        /// <summary>
        /// Get all slides for current store
        /// </summary>
        /// <param name="startDate">Publication start date</param>
        /// <param name="endDate">Publication end date</param>
        /// <param name="showHidden">Show unpublished slides too</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page sizer</param>
        /// <param name="storeId">Store id number</param>
        /// <returns></returns>
        public virtual IPagedList<Slide> GetAllSlides(DateTime? startDate = null, DateTime? endDate = null, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue, int storeId = 0)
        {
            var query = _slideRepository.Table;

            //filter unpublished slides
            if (!showHidden)
                query = query.Where(x => x.Published);

            //filter by start date
            if (startDate.HasValue)
                query = query.Where(x => x.StartDateUtc <= DateTime.UtcNow);

            //filter by end publishing date
            if (endDate.HasValue)
                query = query.Where(x => x.EndDateUtc >= DateTime.UtcNow);

            var result = new List<Slide>();

            foreach (var s in query)
                if (_storeMappingService.Authorize(s, storeId))
                    result.Add(s);

            return new PagedList<Slide>(result, pageIndex, pageSize);
        }

        /// <summary>
        /// Insert slide
        /// </summary>
        /// <param name="slide">Inserting slide</param>
        public virtual void InsertSlide(Slide slide)
        {
            _slideRepository.Insert(slide);
        }

        /// <summary>
        /// Update already exist slide
        /// </summary>
        /// <param name="slide">Updating slide</param>
        public virtual void UpdateSlide(Slide slide)
        {
            _slideRepository.Update(slide);
        }

        /// <summary>
        /// Delete slide from database
        /// </summary>
        /// <param name="slide">Deleting slide</param>
        public virtual void DeleteSlide(Slide slide)
        {
            var allLanguages = _languageService.GetAllLanguages(true);

            //delete slide localized pictures and values
            foreach (var language in allLanguages)
            {
                var pictureIdLocalizaedValue = _localizedEntityService.GetLocalizedValue(language.Id, slide.Id, "Slide", "PictureId");
                if (!string.IsNullOrEmpty(pictureIdLocalizaedValue) && int.TryParse(pictureIdLocalizaedValue, out int pictureId))
                {
                    //delete localized values
                    _localizedEntityService.SaveLocalizedValue(slide, x => x.PictureId, null, language.Id);
                    _localizedEntityService.SaveLocalizedValue(slide, x => x.HyperlinkAddress, null, language.Id);
                    _localizedEntityService.SaveLocalizedValue(slide, x => x.Description, null, language.Id);


                    var localizedPicture = _pictureService.GetPictureById(pictureId);
                    //go to next picture if current picture aren't exist
                    if (localizedPicture == null)
                        continue;

                    _pictureService.DeletePicture(localizedPicture);
                }
            }

            //delete slide base picture
            var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
            if (picture != null)
                _pictureService.DeletePicture(picture);

            //delete slide entity
            _slideRepository.Delete(slide);
        }

        #endregion
    }
}
