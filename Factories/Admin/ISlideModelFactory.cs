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

using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents slide model factory interface.
    /// </summary>
    public interface ISlideModelFactory
    {
        /// <summary>
        /// Prepare slide store mappings
        /// </summary>
        /// <param name="slide">Slide entity</param>
        /// <param name="model">Slide model</param>
        Task PrepareStoreMappingAsync(SlideModel model, Slide slide);

        /// <summary>
        /// Prepare acl models
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties"></param>
        Task PrepareAclModelAsync(SlideModel model, Slide slide, bool excludeProperties);

        /// <summary>
        /// Prepare slide paged list model
        /// </summary>
        /// <param name="searchModel">Slide search model</param>
        /// <returns>Slide paged list model</returns>
        Task<SlideSearchModel.SlidePagedListModel> PrepareSlideListPagedModelAsync(SlideSearchModel searchModel);

        /// <summary>
        /// Prepare slide model
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties">Prepare localized values or not</param>
        /// <returns>Slide model</returns>
        Task<SlideModel> PrepareSlideModelAsync(SlideModel model, Slide slide, bool excludeProperties = false);
    }
}
