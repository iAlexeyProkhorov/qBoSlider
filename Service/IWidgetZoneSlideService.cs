using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone slide mappings service interface
    /// </summary>
    public interface IWidgetZoneSlideService
    {
        /// <summary>
        /// Get widget zone slide by id
        /// </summary>
        /// <param name="id">Slide entity id number</param>
        /// <returns>Widget zone entity</returns>
        WidgetZoneSlide GetWidgetZoneSlide(int id);

        /// <summary>
        /// Get widget zones slide relations
        /// </summary>
        /// <param name="widgetZoneId">Widget zone id number</param>
        /// <param name="slideId">Slide id number</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zone slides collection</returns>
        IPagedList<WidgetZoneSlide> GetWidgetZoneSlides(int? widgetZoneId = null, int? slideId = null, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Insert new widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        void InsertWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide);

        /// <summary>
        /// Update widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        void UpdateWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide);

        /// <summary>
        /// Delete widget zone slide mapping
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide mapping entity</param>
        void DeleteWidgetZoneSlide(WidgetZoneSlide widgetZoneSlide);
    }
}
