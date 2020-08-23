using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents slide model widget zone factory. Using for slide editing
    /// </summary>
    public interface ISlideWidgetZoneModelFactory
    {
        /// <summary>
        /// Prepare slide widget zone list
        /// </summary>
        /// <param name="searchModel">Widget zone search model</param>
        /// <returns>Slide widget zones list</returns>
        SlideWidgetZoneSearchModel.WidgetZonePagedList PrepareWidgetZoneList(SlideWidgetZoneSearchModel searchModel);

        /// <summary>
        /// Prepare slide widget zones list for 'Add widget zone' model
        /// </summary>
        /// <param name="searchModel">Add slide widget zone model</param>
        /// <returns>Slide widget zone list</returns>
        AddSlideWidgetZoneModel.WidgetZonePagedList PrepareWidgetZoneList(AddSlideWidgetZoneModel searchModel);

        /// <summary>
        /// Prepare edit slide widget zone edit model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns>Edit slide widget zone model</returns>
        EditSlideWidgetZoneModel PrepareEditSlideWidgetZoneModel(WidgetZoneSlide widgetZoneSlide);
    }
}
