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
    /// Represents slide model widget zone factory. Using for slide editing
    /// </summary>
    public interface ISlideWidgetZoneModelFactory
    {
        /// <summary>
        /// Prepare slide widget zone list
        /// </summary>
        /// <param name="searchModel">Widget zone search model</param>
        /// <returns>Slide widget zones list</returns>
        Task<SlideWidgetZoneSearchModel.WidgetZonePagedList> PrepareWidgetZoneListAsync(SlideWidgetZoneSearchModel searchModel);

        /// <summary>
        /// Prepare slide widget zones list for 'Add widget zone' model
        /// </summary>
        /// <param name="searchModel">Add slide widget zone model</param>
        /// <returns>Slide widget zone list</returns>
        AddSlideWidgetZoneModel.WidgetZonePagedList PrepareWidgetZoneList(AddSlideWidgetZoneModel searchModel);

        /// <summary>
        /// Prepare edit slide widget zone edit model
        /// </summary>
        /// <param name="widgetZoneSlide">Widget zone slide entity</param>
        /// <returns>Edit slide widget zone model</returns>
        Task<EditSlideWidgetZoneModel> PrepareEditSlideWidgetZoneModelAsync(WidgetZoneSlide widgetZoneSlide);
    }
}
