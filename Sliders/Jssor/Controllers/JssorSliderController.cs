using Baroque.Plugin.Widgets.qBoSlider.Service;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Web.Framework.Controllers;

namespace Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor.Controllers;

/// <summary>
/// Represents Jssor slider configuration controller
/// </summary>
public partial class JssorSliderController : BasePluginController
{
    #region Fields

    private readonly IWidgetZoneService _widgetZoneService;
    private readonly IWidgetZoneSliderPropertyService _widgetZoneSliderPropertyService;

    #endregion

    #region Contructor

    public JssorSliderController(IWidgetZoneService widgetZoneService,
        IWidgetZoneSliderPropertyService widgetZoneSliderPropertyService)
    {
        _widgetZoneService = widgetZoneService;
        _widgetZoneSliderPropertyService = widgetZoneSliderPropertyService;
    }

    #endregion

    #region Methods



    #endregion
}
