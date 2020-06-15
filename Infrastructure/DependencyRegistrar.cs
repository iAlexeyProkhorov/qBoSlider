using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
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
            //services
            builder.RegisterType<SlideService>().As<ISlideService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
