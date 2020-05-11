using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Factories;
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
