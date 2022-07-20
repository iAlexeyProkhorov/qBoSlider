using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Widgets.qBoSlider.Models.Admin;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Services;
using Nop.Services.Localization;
using Nop.Web.Framework.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.qBoSlider.Factories.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchModelFactory : ISearchModelFactory
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IWidgetZoneService _widgetZoneService;

        #endregion

        #region Constructor

        public SearchModelFactory(
            ILocalizationService localizationService,
            IWidgetZoneService widgetZoneService)
        {
            _localizationService = localizationService;
            _widgetZoneService = widgetZoneService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Prepares slide search model
        /// </summary>
        /// <param name="model">Slide search model</param>
        /// <returns>Slide search model</returns>
        public virtual async Task PrepareSlideSearchModelAsync<TModel>(TModel model) where TModel : BaseSearchModel, ISlideSearchModel
        {
            model.AvailableWidgetZones = _widgetZoneService.GetWidgetZones().Select(x =>
            {
                return new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.Name} ({x.SystemName})"
                };
            }).ToList();
            model.AvailablePublicationStates = (await PublicationState.All.ToSelectListAsync(useLocalization: true)).ToList();
            model.SetGridPageSize();
            model.AvailableWidgetZones.Insert(0,
                new SelectListItem()
                {
                    Value = "0",
                    Text = await _localizationService.GetResourceAsync("admin.common.all")
                });
        }

        #endregion
    }
}
