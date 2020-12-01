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
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        #region Constructor

        public ConfigurationModel()
        {
            this.AvailableArrowNavigations = new List<SelectListItem>();
            this.AvailableBulletNavigations = new List<SelectListItem>();
            this.SearchModel = new SlideSearchModel();
        }

        #endregion
        public int ActiveStoreScopeConfiguration { get; set; }

        public SlideSearchModel SearchModel { get; set; }

        #region Arrow navigation

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.ArrowDisplayNavigationType")]
        public int ArrowNavigation { get; set; }

        public IList<SelectListItem> AvailableArrowNavigations { get; set; }

        #endregion

        #region Bullet navigation

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.BulletDisplayNavigationType")]
        public int BulletNavigation { get; set; }

        public IList<SelectListItem> AvailableBulletNavigations { get; set; }

        #endregion

        #region Main options

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.WidgetZoneName")]
        public string WidgetZoneName { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.AutoPlayInterval")]
        public int AutoPlayInterval { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.SlideDuration")]
        public int SlideDuration { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.MinDragOffsetToSlide")]
        public int MinDragOffsetToSlide { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.SlideSpacing")]
        public int SlideSpacing { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.qBoSlider.AutoPlay")]
        public bool AutoPlay { get; set; }

        #endregion
    }
}