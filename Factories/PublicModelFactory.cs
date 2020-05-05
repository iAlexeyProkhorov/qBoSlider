using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Public;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Widgets.qBoSlider.Factories
{
    public class PublicModelFactory : IPublicModelFactory
    {
        #region Fields

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public virtual WidgetZoneModel PrepareWidgetZoneModel(WidgetZone widgetZone)
        {
            return new WidgetZoneModel();
        }

        #endregion
    }
}
