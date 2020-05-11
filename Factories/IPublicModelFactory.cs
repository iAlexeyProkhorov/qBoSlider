using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Public;

namespace Nop.Plugin.Widgets.qBoSlider.Factories
{
    /// <summary>
    /// Represents public model factory.
    /// Added to Multiwidget qBoSlider from version 1.1.0
    /// </summary>
    public interface IPublicModelFactory
    {
        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Widget zone model</returns>
        WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone);

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <param name="storeId">Store id number</param>
        /// <param name="languageId">Language entity id number</param>
        /// <returns>Widget zone model</returns>
        WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone, int languageId, int storeId);
    }
}
