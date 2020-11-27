using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Web.Framework.Controllers;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Controllers
{
    public class qBoWidgetZoneSearchController: BasePluginController
    {
        #region Fields

        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Contructor

        public qBoWidgetZoneSearchController(IWidgetZoneService widgetZoneService)
        {
            _widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Methods

        public virtual IActionResult FindSliderWidgetZoneByName(string name)
        {
            var widgetZones = _widgetZoneService.GetWidgetZones(name, null, true, 0, 10).Select(wz =>
            {
                return wz.Name;
            }).ToList();

            return Json(widgetZones);
        }

        public virtual IActionResult FindSliderWidgetZoneBySystemName(string systemName)
        {
            var widgetZones = _widgetZoneService.GetWidgetZones(null, systemName, true, 0, 10).Select(wz =>
            {
                return wz.SystemName;
            }).ToList();

            return Json(widgetZones);
        }

        #endregion
    }
}
