using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Widgets.qBoSlider
{
    public class RouteProvider: IRouteProvider
    {
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Nop.Plugin.Widgets.qBoSlider.EditSlidePopup", 
                "plugins/qboslider/editslide/{id}", 
                new { controller = "qBoSlider", action = "EditSlidePopup", area = "Admin" });

            routeBuilder.MapRoute("Nop.Plugin.Widgets.qBoSlider.CreateSlidePopup",
                 "plugins/qboslider/createslide",
                 new { controller = "qBoSlider", action = "CreateSlidePopup", area = "Admin" });
        }

        public int Priority
        {
            get { return 0; }
        }
    }
}
