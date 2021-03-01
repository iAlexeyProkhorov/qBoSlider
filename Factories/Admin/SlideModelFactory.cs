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
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents slide model factory.
    /// </summary>
    public class SlideModelFactory : ISlideModelFactory
    {
        #region Fields

        private readonly IAclService _aclService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedModelFactory _localizedModelFactory;
        private readonly IPictureService _pictureService;
        private readonly ISlideService _slideService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IStoreService _storeService;

        #endregion

        #region Constructor

        public SlideModelFactory(IAclService aclService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            ILocalizedModelFactory localizedModelFactory,
            IPictureService pictureService,
            ISlideService slideService,
            IStoreMappingService storeMappingService,
            IStoreService storeService)
        {
            this._aclService = aclService;
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._localizedModelFactory = localizedModelFactory;
            this._pictureService = pictureService;
            this._slideService = slideService;
            this._storeMappingService = storeMappingService;
            this._storeService = storeService;
        }

        #endregion

        #region Utilites

        /// <summary>
        /// Prepare slide list model
        /// </summary>
        /// <param name="slide">Slide entity</param>
        /// <returns>Slide list model</returns>
        protected virtual SlideSearchModel.SlideListItemModel PrepareSlideListItem(Slide slide)
        {
            var picture = _pictureService.GetPictureById(slide.PictureId.GetValueOrDefault(0));
            return new SlideSearchModel.SlideListItemModel()
            {
                Id = slide.Id,
                Picture = _pictureService.GetPictureUrl(picture.Id, 300),
                Hyperlink = slide.HyperlinkAddress,
                StartDateUtc = slide.StartDateUtc,
                EndDateUtc = slide.EndDateUtc,
                Published = slide.Published
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepare slide store mappings
        /// </summary>
        /// <param name="slide">Slide entity</param>
        /// <param name="model">Slide model</param>
        public virtual void PrepareStoreMapping(SlideModel model, Slide slide)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableStores = _storeService.GetAllStores().Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                };
            }).OrderBy(x => x.Text).ToList();

            if (slide != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(slide).ToList();
        }

        /// <summary>
        /// Prepare acl models
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties"></param>
        public virtual void PrepareAclModel(SlideModel model, Slide slide, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && slide != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(slide).ToList();

            var allRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var role in allRoles)
            {
                model.AvailableCustomerRoles.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                });
            }
        }

        /// <summary>
        /// Prepare slide paged list model
        /// </summary>
        /// <param name="searchModel">Slide search model</param>
        /// <returns>Slide paged list model</returns>
        public virtual SlideSearchModel.SlidePagedListModel PrepareSlideListPagedModel(SlideSearchModel searchModel)
        {
            var slides = _slideService.GetAllSlides(showHidden: true, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            var gridModel = new SlideSearchModel.SlidePagedListModel().PrepareToGrid(searchModel, slides, () =>
            {
                return slides.Select(slide =>
                {
                    var pictureId = slide.PictureId.GetValueOrDefault(0);
                    return new SlideSearchModel.SlideListItemModel()
                    {
                        Id = slide.Id,
                        Picture = _pictureService.GetPictureUrl(pictureId, 300),
                        Hyperlink = slide.HyperlinkAddress,
                        StartDateUtc = slide.StartDateUtc,
                        EndDateUtc = slide.EndDateUtc,
                        Published = slide.Published
                    };
                });
            });

            return gridModel;
        }

        /// <summary>
        /// Prepare slide model
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties">Prepare localized values or not</param>
        /// <returns>Slide model</returns>
        public virtual SlideModel PrepareSlideModel(SlideModel model, Slide slide, bool excludeProperties = false)
        {
            Action<SlideLocalizedModel, int> localizedModelConfiguration = null;

            if(slide != null)
            {
                model.Id = slide.Id;
                model.Hyperlink = slide.HyperlinkAddress;
                model.PictureId = slide.PictureId.GetValueOrDefault(0);
                model.Description = slide.Description;
                model.StartDateUtc = slide.StartDateUtc;
                model.EndDateUtc = slide.EndDateUtc;
                model.Published = slide.Published;

                //define localized model configuration action
                localizedModelConfiguration = (locale, languageId) =>
                {
                    locale.PictureId = _localizationService.GetLocalized(slide, x => x.PictureId, languageId, false, false).GetValueOrDefault(0);
                    locale.Description = _localizationService.GetLocalized(slide, entity => entity.Description, languageId, false, false);
                    locale.Hyperlink = _localizationService.GetLocalized(slide, entity => entity.HyperlinkAddress, languageId, false, false);
                };
            }

            //prepare slide store mappings
            PrepareStoreMapping(model, slide);

            //prepare slide ACL
            PrepareAclModel(model, slide, false);

            //prepare localized models
            if (!excludeProperties)
                model.Locales = _localizedModelFactory.PrepareLocalizedModels(localizedModelConfiguration);

            //prepare model widget zone search
            model.WidgetZoneSearchModel.SetGridPageSize();

            return model;
        }

        #endregion
    }
}
