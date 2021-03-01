//Copyright 2021 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Microsoft.AspNetCore.Mvc;
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
