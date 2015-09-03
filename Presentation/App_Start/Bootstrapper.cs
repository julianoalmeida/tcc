using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoFacConfig;
using AutoFacConfig.Log;
using log4net;
using NHibernate;
using Web.Controllers;

namespace Web
{
    public class Bootstrapper
    {
        private static ILog Logger()
        {
            return LogManager.GetLogger("Web");
        }

        public static void Run()
        {
            try
            {
                Logger().Info("Inicializando o container para injeção de dependencia...");
                RegisterContainer();
                Logger().Info("Inicialização do container realizada com sucesso!");
            }
            catch (Exception ex)
            {
                Logger().Error("Ocorreu um erro ao inicializar o container.", ex);
                throw;
            }
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

            #region Log4Net

            // Registra o modulo responsavel por resolver ILog nos construtores.
            builder.RegisterModule(new LoggingModule());
            // Registra a dependencia para a interface ILog. (Quando não resolvido pelo construtor).
            builder.Register(c => LogManager.GetLogger("Web"))
                .As<ILog>();

            #endregion

            builder.RegisterFilterProvider();

            var container = builder.Build();
            container.Resolve<ISessionFactory>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            System.Web.HttpContext.Current.Application["DependencyResolver"] = DependencyResolver.Current;
        }
    }
}