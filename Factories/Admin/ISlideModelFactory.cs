using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin.Slides;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents slide model factory interface.
    /// </summary>
    public interface ISlideModelFactory
    {
        /// <summary>
        /// Prepare slide store mappings
        /// </summary>
        /// <param name="slide">Slide entity</param>
        /// <param name="model">Slide model</param>
        void PrepareStoreMapping(SlideModel model, Slide slide);

        /// <summary>
        /// Prepare acl models
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties"></param>
        void PrepareAclModel(SlideModel model, Slide slide, bool excludeProperties);

        /// <summary>
        /// Prepare slide paged list model
        /// </summary>
        /// <param name="searchModel">Slide search model</param>
        /// <returns>Slide paged list model</returns>
        SlideSearchModel.SlidePagedListModel PrepareSlideListPagedModel(SlideSearchModel searchModel);

        /// <summary>
        /// Prepare slide model
        /// </summary>
        /// <param name="model">Slide model</param>
        /// <param name="slide">Slide entity</param>
        /// <param name="excludeProperties">Prepare localized values or not</param>
        /// <returns>Slide model</returns>
        SlideModel PrepareSlideModel(SlideModel model, Slide slide, bool excludeProperties = false);
    }
}
