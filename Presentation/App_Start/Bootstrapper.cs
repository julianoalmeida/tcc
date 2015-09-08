using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoFacConfig;
using NHibernate;
using Web.Controllers;

namespace Web
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

            #region Controllers

            // Registra os controllers no container pelo seu próprio tipo concreto
            // Registrando no container os controllers
            var controllers = typeof(HomeController).Assembly;

            builder.RegisterAssemblyTypes(controllers)
                .Where(t => t.Namespace == "Web.Controllers")
                .AsSelf();

            #endregion
            
            builder.RegisterFilterProvider();

            var container = builder.Build();
            container.Resolve<ISessionFactory>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            System.Web.HttpContext.Current.Application["DependencyResolver"] = DependencyResolver.Current;
        }
    }
}