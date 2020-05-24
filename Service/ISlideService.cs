using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using System;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents slide service interface
    /// </summary>
    public interface ISlideService
    {
        /// <summary>
        /// Get slide by individual id number
        /// </summary>
        /// <param name="id">Slide individual number</param>
        /// <returns>Slide</returns>
        Slide GetSlideById(int id);

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
        IPagedList<Slide> GetAllSlides(DateTime? startDate = null, DateTime? endDate = null, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue, int storeId = 0);

        /// <summary>
        /// Insert slide
        /// </summary>
        /// <param name="slide">Inserting slide</param>
        void InsertSlide(Slide slide);

        /// <summary>
        /// Update already exist slide
        /// </summary>
        /// <param name="slide">Updating slide</param>
        void UpdateSlide(Slide slide);

        /// <summary>
        /// Delete slide from database
        /// </summary>
        /// <param name="slide">Deleting slide</param>
        void DeleteSlide(Slide slide);
    }
}
