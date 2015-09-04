using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;
using AutoFacConfig.Interceptadores;
using Data;
using Data.SetupSessionFactory;
using Entidades;
using Negocio;
using NHibernate;

namespace AutoFacConfig
{
    public class ContainerHelper
    {
        public static ContainerBuilder ObterContainerBuilder()
        {
            var builder = new ContainerBuilder();

            RegisterAssemblies(builder);

            return builder;
        }

        private static void RegisterAssemblies(ContainerBuilder builder)
        {
            RegisterNHibernatSectionAndFactory(builder);

            RegisterRepositoryAssembly(builder);

            RegisterBusinessAssembly(builder);

            RegisterEntityAssembly(builder);
        }

        private static void RegisterNHibernatSectionAndFactory(ContainerBuilder builder)
        {
            builder.Register(x => FluentSessionFactoryFactory.GetSessionFactory("thread_static", "coonStringTcc"))
                .SingleInstance();

            builder.RegisterType<NHibernateInterceptor>().SingleInstance().AsSelf();

            builder.Register(x =>
            {
                var session = x.Resolve<ISessionFactory>().OpenSession(x.Resolve<NHibernateInterceptor>());
                session.FlushMode = FlushMode.Commit;
                return session;
            }).InstancePerHttpRequest().OnRelease(x => x.Dispose());
        }

        private static void RegisterRepositoryAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(NHibernateRepository<>).Assembly)
                .Where(t => t.Name.EndsWith("Data"))
                .AsImplementedInterfaces();
        }

        private static void RegisterBusinessAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseBusiness<>).Assembly)
                .Where(t => t.Name.EndsWith("Business"))
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ServiceTransactionInterceptor));

            builder.RegisterType<ServiceTransactionInterceptor>()
                .InstancePerHttpRequest()
                .AsSelf();
        }

        private static void RegisterEntityAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseEntity).Assembly)
                .Where(t => t.BaseType == typeof(BaseEntity))
                .AsSelf();
        }
    }
}
