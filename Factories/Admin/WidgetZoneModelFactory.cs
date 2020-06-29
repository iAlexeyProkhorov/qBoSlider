using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services;
using Nop.Services.Customers;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone model factory
    /// </summary>
    public partial class WidgetZoneModelFactory : IWidgetZoneModelFactory
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;
        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Constructor

        public WidgetZoneModelFactory(IAclService aclService,
            ICustomerService customerService,
            IStoreMappingService storeMappingService,
            IStoreService storeService,
            IWidgetZoneService widgetZoneService)
        {
            this._aclService = aclService;
            this._customerService = customerService;
            this._storeMappingService = storeMappingService;
            this._storeService = storeService;
            this._widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare widget zone paged list model
        /// </summary>
        /// <param name="searchModel">Search model</param>
        /// <returns>Paged list model</returns>
        public virtual WidgetZoneSearchModel.WidgetZoneList PrepareWidgetZonePagedListModel(WidgetZoneSearchModel searchModel)
        {
            var widgetZones = _widgetZoneService.GetWidgetZones(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var gridModel = new WidgetZoneSearchModel.WidgetZoneList().PrepareToGrid(searchModel, widgetZones, () =>
            {
                return widgetZones.Select(widgetZone =>
                {
                    return new WidgetZoneSearchModel.WidgetZoneListItem()
                    {
                        Id = widgetZone.Id,
                        Name = widgetZone.Name,
                        SystemName = widgetZone.SystemName
                    };
                });
            });

            return gridModel;
        }

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="model">Widget zone admin model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Prepared widget zone model</returns>
        public virtual WidgetZoneModel PrepareWidgetZoneModel(WidgetZoneModel model, WidgetZone widgetZone)
        {
            if (model == null)
                throw new Exception("Widget zone model are nullable");

            //prepare widget zone model if widget zone entity are exist
            if (widgetZone != null)
                model = new WidgetZoneModel()
                {
                    ArrowNavigationDisplayingTypeId = widgetZone.ArrowNavigationDisplayingTypeId,
                    AvailableArrowNavigations = NavigationType.Always.ToSelectList().ToList(),
                    AutoPlay = widgetZone.AutoPlay,
                    AutoPlayInterval = widgetZone.AutoPlayInterval,
                    BulletNavigationDisplayingTypeId = widgetZone.BulletNavigationDisplayingTypeId,
                    AvailableBulletNavigations = NavigationType.Always.ToSelectList().ToList(),
                    LimitedToStores = widgetZone.LimitedToStores,
                    Id = widgetZone.Id,
                    MinDragOffsetToSlide = widgetZone.MinDragOffsetToSlide,
                    Name = widgetZone.Name,
                    Published = widgetZone.Published,
                    SlideDuration = widgetZone.SlideDuration,
                    SlideSpacing = widgetZone.SlideSpacing,
                    SubjectToAcl = widgetZone.SubjectToAcl,
                    SystemName = widgetZone.SystemName
                };

            //prepare list of availbale naviagation types
            var naviagationTypes = NavigationType.Always.ToSelectList(false).ToList();
            model.AvailableArrowNavigations = naviagationTypes;
            model.AvailableBulletNavigations = naviagationTypes;

            return model;
        }

        /// <summary>
        /// Prepare widget zone ACL model
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual void PrepareAclModel(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone)
        {
            if (widgetZoneModel == null)
                throw new Exception("Widget zone model are null.");

            //prepare widget zone customer roles
            if (widgetZone != null)
                widgetZoneModel.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(widgetZone).ToList();

            //prepare available customer roles list
            var customerRoles = _customerService.GetAllCustomerRoles(true);
            widgetZoneModel.AvailableCustomerRoles = customerRoles.Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = widgetZoneModel.SelectedCustomerRoleIds.Contains(x.Id)
                };
            }).ToList();
        }

        /// <summary>
        /// Prepare widget zone store mappings
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        public virtual void PrepareStoreMappings(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone)
        {
            if (widgetZoneModel == null)
                throw new Exception("Widget zone model are null.");

            //load selected stores for current widget zone
            if (widgetZone != null)
                widgetZoneModel.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(widgetZone);

            //get all available stores
            var availableStores = _storeService.GetAllStores();
            widgetZoneModel.AvailableStores = availableStores.Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name,
                    Selected = widgetZoneModel.SelectedStoreIds.Contains(x.Id)
                };
            }).ToList();
        }

        #endregion
    }
}
