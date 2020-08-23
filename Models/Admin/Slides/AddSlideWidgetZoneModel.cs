using Nop.Web.Framework.Models;
using System.Collections.Generic;

namespace Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides
{
    /// <summary>
    /// Represents add widget zone for slide editing
    /// </summary>
    public class AddSlideWidgetZoneModel : BaseSearchModel
    {
        /// <summary>
        /// Gets or sets slide entity id
        /// </summary>
        public int SlideId { get; set; }

        /// <summary>
        /// Gets or sets selected widget zone id numbers
        /// </summary>
        public IList<int> SelectedWidgetZoneIds { get; set; } = new List<int>();

        public class WidgetZonePagedList: BasePagedListModel<WidgetZoneModel>
        {

        }

        /// <summary>
        /// Represents widget zone model for slide editing
        /// </summary>
        public class WidgetZoneModel : BaseNopEntityModel, ISlideWidgetZoneModel
        {
            /// <summary>
            /// Gets or sets widget zone friendly name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets widget zone system name
            /// </summary>
            public string SystemName { get; set; }

            /// <summary>
            /// Gets or sets value indicating widget zone availability
            /// </summary>
            public bool Published { get; set; }
        }
    }
}
