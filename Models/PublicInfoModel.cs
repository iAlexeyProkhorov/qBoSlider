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


using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        #region Arrow navigation

        public int ArrowNavigation { get; set; }

        #endregion

        #region Bullet navigation

        public int BulletNavigation { get; set; }

        #endregion

        #region Main options

        public int AutoPlayInterval { get; set; }

        public int SlideDuration { get; set; }

        public int MinDragOffsetToSlide { get; set; }

        public int SlideSpacing { get; set; }

        public bool AutoPlay { get; set; }

        #endregion

        public IList<PublicSlideModel> Slides { get; set; }

        #region Constructor

        public PublicInfoModel()
        {
            Slides = new List<PublicSlideModel>();
        }

        #endregion

        #region Nested classes

        public class PublicSlideModel
        {
            public string Picture { get; set; }

            public string Hyperlink { get; set; }

            public string Description { get; set; }
        }

        #endregion
    }
}