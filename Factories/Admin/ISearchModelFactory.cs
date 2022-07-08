using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// Represents search model factory interface.
    /// Stores methods which prepares common search box properties
    /// </summary>
    public interface ISearchModelFactory
    {
        /// <summary>
        /// Prepares slide search box model
        /// </summary>
        /// <typeparam name="TModel">Slide search model</typeparam>
        /// <param name="searchModel">Search box model instance</param>
        /// <returns>Model instance</returns>
        void PrepareSlideSearchModel<TModel>(TModel searchModel) where TModel : BaseSearchModel, ISlideSearchModel;
    }
}
