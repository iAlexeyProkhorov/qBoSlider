using Nop.Data.Mapping;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Mapping
{
    /// <summary>
    /// Represents qBoSlider database table names and columns compatibilities
    /// </summary>
    public class qBoSliderNamesCompatibility : INameCompatibility
    {
        #region Properties

        /// <summary>
        /// Gets table name for mapping with the type
        /// </summary>
        public Dictionary<Type, string> TableNames => new Dictionary<Type, string>()
        {
            { typeof(WidgetZone), "Baroque_qBoSlider_WidgetZone" },
            { typeof(Slide), "Baroque_qBoSlider_Slide" },
            { typeof(WidgetZoneSlide), "Baroque_qBoSlider_WidgetZone_Slide_Mapping" }
        };

        /// <summary>
        ///  Gets column name for mapping with the entity's property and type
        /// </summary>
        public Dictionary<(Type, string), string> ColumnName { get; } = new Dictionary<(Type, string), string>();

        #endregion
    }
}
