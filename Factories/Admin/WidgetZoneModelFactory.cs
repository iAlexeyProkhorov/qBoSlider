//    This file is part of qBoSlider.

//    qBoSlider is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.

//    qBoSlider is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with qBoSlider.  If not, see<https://www.gnu.org/licenses/>.

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
                        SystemName = widgetZone.SystemName,
                        Published = widgetZone.Published
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
            {
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
                    SystemName = widgetZone.SystemName,
                };

                //put widget zone id number for slide searhing
                model.SlideSearchModel.WidgetZoneId = widgetZone.Id;
            }

            //prepare list of availbale naviagation types
            var navigationTypes = NavigationType.Always.ToSelectList(false).ToList();
            model.AvailableArrowNavigations = navigationTypes;
            model.AvailableBulletNavigations = navigationTypes;

            //prepare slide search model
            model.SlideSearchModel.SetGridPageSize();

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
                widgetZoneModel.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(widgetZone).ToList();

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
