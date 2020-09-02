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
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Factories.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Security;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.qBoSlider.Components
{
    [ViewComponent(Name = "Baroque.qBoSlider.PublicInfo")]
    public class PublicInfoComponent : NopViewComponent
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IPublicModelFactory _publicModelFactory;
        private readonly IWidgetZoneService _widgetZoneService;

        private readonly IStoreContext _storeContext;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public PublicInfoComponent(IAclService aclService,
            IPublicModelFactory publicModelFactory,
            IWidgetZoneService widgetZoneService,

            IStoreContext storeContext,
            IWorkContext workContext)
        {
            this._aclService = aclService;
            this._publicModelFactory = publicModelFactory;
            this._widgetZoneService = widgetZoneService;

            this._storeContext = storeContext;
            this._workContext = workContext;
        }

        #endregion

        #region Methods

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var widget = _widgetZoneService.GetWidgetZoneBySystemName(widgetZone);

            //return empty result if widget zone has no slider
            if (widget == null)
                return Content(string.Empty);

            //return empty page if widget zone aren't authorized
            if (!_aclService.Authorize(widget))
                return Content(string.Empty);


            var model = _publicModelFactory.PrepareWidgetZoneModel(widget);

            return View("~/Plugins/Widgets.qBoSlider/Views/Public/PublicInfo.cshtml", model);
        }

        #endregion
    }
}
