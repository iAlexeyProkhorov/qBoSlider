//Copyright 2020 Alexey Prokhorov

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
using Nop.Plugin.Widgets.qBoSlider.Factories.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Components
{
    [ViewComponent(Name = "Baroque.qBoSlider.PublicInfo")]
    public class PublicInfoComponent : NopViewComponent
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IPublicModelFactory _publicModelFactory;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Constructor

        public PublicInfoComponent(IAclService aclService,
            IPublicModelFactory publicModelFactory,
            IStoreMappingService storeMappingService,
            IWidgetZoneService widgetZoneService
            )
        {
            _aclService = aclService;
            _publicModelFactory = publicModelFactory;
            _storeMappingService = storeMappingService;
            _widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var widget = _widgetZoneService.GetWidgetZoneBySystemName(widgetZone);

            //return empty result if widget zone has no slider
            if (widget == null)
                return Content(string.Empty);

            //return empty result if widget zone isn't published
            if (!widget.Published)
                return Content(string.Empty);

            //return empty page if widget zone aren't authorized
            if (!await _aclService.AuthorizeAsync(widget))
                return Content(string.Empty);

            //return nothing if widget zone aren't authorized in current store
            if (!await _storeMappingService.AuthorizeAsync(widget))
                return Content(string.Empty);

            //return empty result, if widget zone has no published slides
            var slides = _widgetZoneService.GetWidgetZoneSlides(widget.Id);
            if (!slides.Any())
                return Content(string.Empty);

            var model = await _publicModelFactory.PrepareWidgetZoneModelAsync(widget);

            return View("~/Plugins/Widgets.qBoSlider/Views/Public/PublicInfo.cshtml", model);
        }

        #endregion
    }
}
