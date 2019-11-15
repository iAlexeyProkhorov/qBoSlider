using System.Collections.Generic;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        #region Arrow navigation

        public int ArrowNavigation { get; set; }

        #endregion

        #region Bullet navigation

        public int BulletNavigation { get; set; }

        #endregion

        #region Main options

        public int AutoPlayInterval { get; set; }

        public int SlideDuration { get; set; }

        public int MinDragOffsetToSlide { get; set; }

        public int SlideSpacing { get; set; }

        public bool AutoPlay { get; set; }

        #endregion

        public IList<PublicSlideModel> Slides { get; set; }

        #region Constructor

        public PublicInfoModel()
        {
            Slides = new List<PublicSlideModel>();
        }

        #endregion

        #region Nested classes

        public class PublicSlideModel
        {
            public string Picture { get; set; }

            public string Hyperlink { get; set; }

            public string Description { get; set; }
        }

        #endregion
    }
}