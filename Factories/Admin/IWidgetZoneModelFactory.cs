using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone model factory interface
    /// </summary>
    public interface IWidgetZoneModelFactory
    {
        /// <summary>
        /// Prepare widget zone paged list model
        /// </summary>
        /// <param name="searchModel">Search model</param>
        /// <returns>Paged list model</returns>
        WidgetZoneSearchModel.WidgetZoneList PrepareWidgetZonePagedListModel(WidgetZoneSearchModel searchModel);

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="model">Widget zone admin model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Prepared widget zone model</returns>
        WidgetZoneModel PrepareWidgetZoneModel(WidgetZoneModel model, WidgetZone widgetZone);

        /// <summary>
        /// Prepare widget zone ACL model
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        void PrepareAclModel(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone);

        /// <summary>
        /// Prepare widget zone store mappings
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        void PrepareStoreMappings(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone);
    }
}
