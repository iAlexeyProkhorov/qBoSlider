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
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.WidgetZones;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents widget zone model factory interface
    /// </summary>
    public interface IWidgetZoneModelFactory
    {
        /// <summary>
        /// Prepare widget zone paged list model
        /// </summary>
        /// <param name="searchModel">Search model</param>
        /// <returns>Paged list model</returns>
        WidgetZoneSearchModel.WidgetZoneList PrepareWidgetZonePagedListModel(WidgetZoneSearchModel searchModel);

        /// <summary>
        /// Prepare widget zone model
        /// </summary>
        /// <param name="model">Widget zone admin model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        /// <returns>Prepared widget zone model</returns>
        Task<WidgetZoneModel> PrepareWidgetZoneModelAsync(WidgetZoneModel model, WidgetZone widgetZone);

        /// <summary>
        /// Prepare widget zone ACL model
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        Task PrepareAclModelAsync(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone);

        /// <summary>
        /// Prepare widget zone store mappings
        /// </summary>
        /// <param name="widgetZoneModel">Widget zone model</param>
        /// <param name="widgetZone">Widget zone entity</param>
        Task PrepareStoreMappingsAsync(WidgetZoneModel widgetZoneModel, WidgetZone widgetZone);
    }
}
