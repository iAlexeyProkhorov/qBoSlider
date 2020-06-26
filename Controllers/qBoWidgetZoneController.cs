using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.qBoSlider.Controllers
{
    /// <summary>
    /// Represents plugin widget zone controller
    /// </summary>
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class qBoWidgetZoneController : BasePluginController
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly IPermissionService _permissionService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IWidgetZoneModelFactory _widgetZoneModelFactory;
        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Constructor

        public qBoWidgetZoneController(IAclService aclService,
            IPermissionService permissionService,
            IStoreMappingService storeMappingService,
            IWidgetZoneModelFactory widgetZoneModelFactory,
            IWidgetZoneService widgetZoneService)
        {
            _aclService = aclService;
            _permissionService = permissionService;
            _storeMappingService = storeMappingService;
            _widgetZoneModelFactory = widgetZoneModelFactory;
            _widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Utilites

        #endregion

        #region Methods

        public virtual IActionResult List()
        {
            //return access denied page if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = new WidgetZoneSearchModel();

            return View("~/Plugins/Widgets.qBoSlider/Views/Admin/WidgetZone/List.cshtml", model);
        }

        [HttpPost]
        public virtual IActionResult List(WidgetZoneSearchModel searchModel)
        {
            //return access denied result if customer has no permissions
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedDataTablesJson();

            var gridModel = _widgetZoneModelFactory.PrepareWidgetZonePagedListModel(searchModel);

            return Json(gridModel);
        }

        #endregion
    }
}
