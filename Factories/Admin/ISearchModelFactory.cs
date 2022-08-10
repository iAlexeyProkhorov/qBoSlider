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

using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Web.Framework.Models;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents search model factory interface.
    /// Stores methods which prepares common search box properties
    /// </summary>
    public interface ISearchModelFactory
    {
        /// <summary>
        /// Prepares slide search box model
        /// </summary>
        /// <typeparam name="TModel">Slide search model</typeparam>
        /// <param name="searchModel">Search box model instance</param>
        /// <returns>Model instance</returns>
        Task PrepareSlideSearchModelAsync<TModel>(TModel searchModel) where TModel : BaseSearchModel, ISlideSearchModel;
    }
}
