using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.qBoSlider.Controllers;
using Nop.Plugin.Widgets.qBoSlider.Domain;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Widgets.qBoSlider.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public const string ContextName = "nop_object_context_slide";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //data context
            builder.RegisterPluginDataContext<qBoSliderContext>(ContextName);

            //associate services
            builder.RegisterType<SlideService>().As<ISlideService>().InstancePerLifetimeScope();


            builder.RegisterType<EfRepository<Slide>>()
                .As<IRepository<Slide>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
                .InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<qBoSliderController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));    
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
