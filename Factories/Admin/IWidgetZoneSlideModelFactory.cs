using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone slide model factory interface
    /// </summary>
    public interface IWidgetZoneSlideModelFactory
    {
        /// <summary>
        /// Prepare widget zone slide list model
        /// </summary>
        /// <param name="searchModel">Widget zone slide search model</param>
        /// <returns>Widget zone slides paged list</returns>
        WidgetZoneSlideSearchModel.SlideList PrepareSlidePagedListModel(WidgetZoneSlideSearchModel searchModel);

        /// <summary>
        /// Prepare add slide model search list
        /// </summary>
        /// <param name="searchModel">Add widget zone slide search model</param>
        /// <returns>Add widget zone slide model</returns>
        AddWidgetZoneSlideModel.SlidePagedListModel PrepareAddWidgetZoneSlideModel(AddWidgetZoneSlideModel searchModel);

        /// <summary>
        /// Prepare widget zone slide model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns></returns>
        WidgetZoneSlideModel PrepareEditWidgetZoneSlideModel(WidgetZoneSlide widgetZoneSlide);
    }
}
