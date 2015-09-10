using System.IO;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Data.BaseRepositories;
using Entidades;
using Negocio;
using _4___Web.Controllers;

namespace UnitTests.DependencyModule
{
    public class UnitTestDependencyModule
    {
        public static void Run()
        {
            RegisterContainer();
        }

        private static void RegisterContainer()
        {
            var builder = ObterContainerBuilder();

            RegisterWebAssembly(builder);
            builder.RegisterFilterProvider();
            SetupAutofacDependencyResolver(builder.Build());
        }

        private static void SetupAutofacDependencyResolver(ILifetimeScope container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            HttpContext.Current = new HttpContext(
                                  new HttpRequest("", "http://tempuri.org", ""),
                                  new HttpResponse(new StringWriter()));

            HttpContext.Current.Application["DependencyResolver"] = DependencyResolver.Current;
        }

        private static void RegisterWebAssembly(ContainerBuilder builder)
        {
            var controllers = typeof(HomeController).Assembly;
            builder.RegisterAssemblyTypes(controllers)
                .Where(t => t.Namespace == "_4___Web.Controllers")
                .AsSelf();
        }

        private static ContainerBuilder ObterContainerBuilder()
        {
            var builder = new ContainerBuilder();

            RegisterAssemblies(builder);

            return builder;
        }

        private static void RegisterAssemblies(ContainerBuilder builder)
        {
            RegisterRepositoryAssembly(builder);

            RegisterBusinessAssembly(builder);

            RegisterEntityAssembly(builder);
        }

        private static void RegisterRepositoryAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseRepositoryRepository<>).Assembly)
                .Where(t => t.Name.EndsWith("Data"))
                .AsImplementedInterfaces();
        }

        private static void RegisterBusinessAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseBusinessBusiness<>).Assembly)
                .Where(t => t.Name.EndsWith("Business"))
                .AsImplementedInterfaces();
        }

        private static void RegisterEntityAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseEntity).Assembly)
                .Where(t => t.BaseType == typeof(BaseEntity))
                .AsSelf();
        }
    }
}
