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
using System.Threading.Tasks;

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
            var widgetZones = _widgetZoneService.GetWidgetZones(
                name: searchModel.SearchWidgetZoneName,
                systemName: searchModel.SearchWidgetZoneSystemName, 
                showHidden: true, 
                pageIndex: searchModel.Page - 1, 
                pageSize: searchModel.PageSize);
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
        public virtual async Task<WidgetZoneModel> PrepareWidgetZoneModelAsync(WidgetZoneModel model, WidgetZone widgetZone)
        {
            if (model == null)
                throw new Exception("Widget zone model are nullable");

            //prepare widget zone model if widget zone entity are exist
            if (widgetZone != null)
            {
                model = new WidgetZoneModel()
                {
                    ArrowNavigationDisplayingTypeId = widgetZone.ArrowNavigationDisplayingTypeId,
                    AvailableArrowNavigations = await NavigationType.Always.ToSelectListAsync(),
                    AutoPlay = widgetZone.AutoPlay,
                    AutoPlayInterval = widgetZone.AutoPlayInterval,
                    BulletNavigationDisplayingTypeId = widgetZone.BulletNavigationDisplayingTypeId,
                    AvailableBulletNavigations = await NavigationType.Always.ToSelectListAsync(),
                    Id = widgetZone.Id,
                    MinDragOffsetToSlide = widgetZone.MinDragOffsetToSlide,
                    MinSlideWidgetZoneWidth = widgetZone.MinSlideWidgetZoneWidth,
                    MaxSlideWidgetZoneWidth = widgetZone.MaxSlideWidgetZoneWidth,
                    Name = widgetZone.Name,
                    Published = widgetZone.Published,
                    SlideDuration = widgetZone.SlideDuration,
                    SlideSpacing = widgetZone.SlideSpacing,
                    SystemName = widgetZone.SystemName,
                };

                //put widget zone id number for slide searhing
                model.SlideSearchModel.WidgetZoneId = widgetZone.Id;
            }

            //prepare list of availbale navigation types
            var navigationTypes = await NavigationType.Always.ToSelectListAsync(false);
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
        public virtual async Task PrepareAclModelAsync(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone)
        {
            if (widgetZoneModel == null)
                throw new Exception("Widget zone model are null.");

            //prepare widget zone customer roles
            if (widgetZone != null)
                widgetZoneModel.SelectedCustomerRoleIds = (await _aclService.GetCustomerRoleIdsWithAccessAsync(widgetZone)).ToList();

            //prepare available customer roles list
            var customerRoles = await _customerService.GetAllCustomerRolesAsync(true);
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
        public virtual async Task PrepareStoreMappingsAsync(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone)
        {
            if (widgetZoneModel == null)
                throw new Exception("Widget zone model are null.");

            //load selected stores for current widget zone
            if (widgetZone != null)
                widgetZoneModel.SelectedStoreIds = (await _storeMappingService.GetStoresIdsWithAccessAsync(widgetZone)).ToList();

            //get all available stores
            var availableStores = await _storeService.GetAllStoresAsync();
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
