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
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Service
{
    /// <summary>
    /// Represents widget zone service interface
    /// </summary>
    public interface IWidgetZoneService
    {
        /// <summary>
        /// Get widget zone by id number
        /// </summary>
        /// <param name="id">Widget zone id number</param>
        /// <returns>Widget zone</returns>
        Task<WidgetZone> GetWidgetZoneByIdAsync(int id);

        /// <summary>
        /// Get widget zone by system name
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <returns>Widget zone entity</returns>
        WidgetZone GetWidgetZoneBySystemName(string systemName);

        /// <summary>
        /// Get list of all existing nopCommerce widget zones
        /// </summary>
        /// <param name="systemName">Widget zone system name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of widget zone system names</returns>
        IPagedList<string> GetNopCommerceWidgetZones(string systemName = null, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get widget zones collection
        /// </summary>
        /// <param name="name">Widget zone name</param>
        /// <param name="systemName">Widget zone system name</param>
        /// <param name="showHidden">GEt only published widget zones</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zones collection</returns>
        IPagedList<WidgetZone> GetWidgetZones(string name = null, string systemName = null, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get widget zone slides
        /// </summary>
        /// <param name="widgetZoneId">Widget zone id number</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Widget zone slides</returns>
        IPagedList<Slide> GetWidgetZoneSlides(int widgetZoneId, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Insert new widget zone in database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        Task InsertWidgetZoneAsync(WidgetZone widgetZone);

        /// <summary>
        /// Update already existing widget zone entity
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        Task UpdateWidgetZoneAsync(WidgetZone widgetZone);

        /// <summary>
        /// Delete already existing widget zone entity from database
        /// </summary>
        /// <param name="widgetZone">Widget zone entity</param>
        Task DeleteWidgetZoneAsync(WidgetZone widgetZone);
    }
}
