//Copyright 2020 Alexey Prokhorov

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//    http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Factories.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;

namespace Nop.Plugin.Widgets.qBoSlider.Infrastructure
{
    /// <summary>
    /// Represents plugin dependencies
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="appSettings">App settings</param>
        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            //services
            services.AddScoped<ISlideService, SlideService>();
            services.AddScoped<IWidgetZoneService, WidgetZoneService>();
            services.AddScoped<IWidgetZoneSlideService, WidgetZoneSlideService>();
            services.AddScoped<IGarbageManager, GarbageManager>();

            //factories
            services.AddScoped<ISearchModelFactory, SearchModelFactory>();
            services.AddScoped<ISlideModelFactory, SlideModelFactory>();
            services.AddScoped<ISlideWidgetZoneModelFactory, SlideWidgetZoneModelFactory>();
            services.AddScoped<IWidgetZoneModelFactory, WidgetZoneModelFactory>();
            services.AddScoped<IWidgetZoneSlideModelFactory, WidgetZoneSlideModelFactory>();
            services.AddScoped<IPublicModelFactory, PublicModelFactory>();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
