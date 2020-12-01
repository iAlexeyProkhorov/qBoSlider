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


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.qBoSlider
{
    /// <summary>
    /// Represents plugin routes
    /// </summary>
    public class RouteProvider: IRouteProvider
    {
        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("Nop.Plugin.Widgets.qBoSlider.EditSlidePopup", 
                "plugins/qboslider/editslide/{id}", 
                new { controller = "qBoSlider", action = "EditSlidePopup", area = "Admin" });

            endpointRouteBuilder.MapControllerRoute("Nop.Plugin.Widgets.qBoSlider.CreateSlidePopup",
                 "plugins/qboslider/createslide",
                 new { controller = "qBoSlider", action = "CreateSlidePopup", area = "Admin" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
