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
using Nop.Plugin.Widgets.qBoSlider.Factories.Admin;
using Nop.Plugin.Widgets.qBoSlider.Factories.Public;
using Nop.Plugin.Widgets.qBoSlider.Service;
using Nop.Web.Framework.Infrastructure.Extensions;

namespace Nop.Plugin.Widgets.qBoSlider.Infrastructure
{
    /// <summary>
    /// Represents plugin dependencies
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public const string ContextName = "nop_object_context_slide";

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //data context
            builder.RegisterPluginDataContext<qBoSliderContext>(ContextName);

            //repositories
            builder.RegisterType<EfRepository<WidgetZone>>()
                .As<IRepository<WidgetZone>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<Slide>>()
                .As<IRepository<Slide>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
                .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<WidgetZoneSlide>>()
                .As<IRepository<WidgetZoneSlide>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ContextName))
                .InstancePerLifetimeScope();

            //associate services
            builder.RegisterType<SlideService>().As<ISlideService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneService>().As<IWidgetZoneService>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneSlideService>().As<IWidgetZoneSlideService>().InstancePerLifetimeScope();
            builder.RegisterType<GarbageManager>().As<IGarbageManager>().InstancePerLifetimeScope();

            //factories
            builder.RegisterType<SlideModelFactory>().As<ISlideModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneModelFactory>().As<IWidgetZoneModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<WidgetZoneSlideModelFactory>().As<IWidgetZoneSlideModelFactory>().InstancePerLifetimeScope();
            builder.RegisterType<PublicModelFactory>().As<IPublicModelFactory>().InstancePerLifetimeScope();

            //cache manager
            builder.RegisterType<qBoSlideController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));    
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
