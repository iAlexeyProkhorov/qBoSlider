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
    }
}
