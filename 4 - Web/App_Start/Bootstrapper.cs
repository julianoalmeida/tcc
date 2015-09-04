using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoFacConfig;
using NHibernate;
using _4___Web.Controllers;

namespace _4___Web
{
    public class Bootstrapper
    {
        public static void Run()
        {
            RegisterContainer();
        }

        private static void RegisterContainer()
        {
            var builder = ContainerHelper.ObterContainerBuilder();

            RegisterWebAssembly(builder);
            builder.RegisterFilterProvider();
            SetupAutofacDependencyResolver(ResolveSectionFactory(builder));
        }

        private static void SetupAutofacDependencyResolver(ILifetimeScope container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            System.Web.HttpContext.Current.Application["DependencyResolver"] = DependencyResolver.Current;
        }

        private static IContainer ResolveSectionFactory(ContainerBuilder builder)
        {
            var container = builder.Build();
            container.Resolve<ISessionFactory>();
            return container;
        }

        private static void RegisterWebAssembly(ContainerBuilder builder)
        {
            var controllers = typeof(HomeController).Assembly;
            builder.RegisterAssemblyTypes(controllers)
                .Where(t => t.Namespace == "_4___Web.Controllers")
                .AsSelf();
        }
    }
}