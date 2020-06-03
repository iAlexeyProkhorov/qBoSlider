using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Services.Events;
using Nop.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents qBoSlider slide service
    /// </summary>
    public partial class SlideService : ISlideService
    {
        #region Fields

        private readonly IRepository<Slide> _slideRepository;

        private readonly IEventPublisher _eventPublisher;
        private readonly IStoreMappingService _storeMappingService;

        #endregion

        #region Constructor

        public SlideService(IRepository<Slide> slideRepository,
            IEventPublisher eventPublisher,
            IStoreMappingService storeMappingService)
        { 
            this._slideRepository = slideRepository;

            this._eventPublisher = eventPublisher;
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

            _eventPublisher.EntityInserted(slide);
        }

        /// <summary>
        /// Update already exist slide
        /// </summary>
        /// <param name="slide">Updating slide</param>
        public virtual void UpdateSlide(Slide slide)
        {
            _slideRepository.Update(slide);

            _eventPublisher.EntityUpdated(slide);
        }

        /// <summary>
        /// Delete slide from database
        /// </summary>
        /// <param name="slide">Deleting slide</param>
        public virtual void DeleteSlide(Slide slide)
        {
            //delete slide entity
            _slideRepository.Delete(slide);

            _eventPublisher.EntityDeleted(slide);
        }

        #endregion
    }
}
