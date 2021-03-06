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

using Autofac;
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
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //associate services
            builder.RegisterType<SlideService>().As<ISlideService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneService>().As<IWidgetZoneService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneSlideService>().As<IWidgetZoneSlideService>().InstancePerLifetimeScope();
            builder.RegisterType<GarbageManager>().As<IGarbageManager>().InstancePerLifetimeScope();

            //factories
            builder.RegisterType<SlideModelFactory>().As<ISlideModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<SlideWidgetZoneModelFactory>().As<ISlideWidgetZoneModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneModelFactory>().As<IWidgetZoneModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneSlideModelFactory>().As<IWidgetZoneSlideModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<PublicModelFactory>().As<IPublicModelFactory>().InstancePerLifetimeScope(); 
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
