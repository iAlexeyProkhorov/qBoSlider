using Baroque.Plugin.Widgets.qBoSlider.Service.Sliders;
using Nop.Core;
using Nop.Plugin.Widgets.qBoSlider.Domain;

namespace Baroque.Plugin.Widgets.qBoSlider.Sliders.Jssor;

/// <summary>
/// Represents JSSOR slider 
/// </summary>
public partial class JssorSlider : ISlider
{
    #region Fields

    private readonly IWebHelper _webHelper;

    #endregion

    #region Constructor

    public JssorSlider(IWebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets or sets slider configuration url
    /// </summary>
    public virtual string GetSliderConfigurationUrl(WidgetZone widgetZone)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets slider public side url
    /// </summary>
    public virtual string GetSliderPublicUrl(WidgetZone widgetZone)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets slider configuration type
    /// </summary>
    public Type SliderConfigurationType
    {
        get
        {
            return typeof(JssorSliderConfiguration);
        }
    }

    #endregion
}
