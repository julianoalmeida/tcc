using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;
using AutoFacConfig.Interceptadores;
using Data.BaseRepositories;
using Data.SetupSessionFactory;
using Entidades;
using Negocio.BaseTypes;
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

            RegisterServiceTransactionInterceptor(builder);

            RegisterEntityAssembly(builder);
        }

        private static void RegisterNHibernatSectionAndFactory(ContainerBuilder builder)
        {
            builder.Register(x => FluentSessionFactory.GetSessionFactory("thread_static", "coonStringTcc"))
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
            builder.RegisterAssemblyTypes(typeof(IBaseRepositoryRepository<>).Assembly)
                .AsImplementedInterfaces()
                .AsSelf();
        }

        private static void RegisterBusinessAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IBaseBusiness<>).Assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(ServiceTransactionInterceptor));
        }

        private static void RegisterServiceTransactionInterceptor(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceTransactionInterceptor>().InstancePerHttpRequest().AsSelf();
        }

        private static void RegisterEntityAssembly(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(BaseEntity).Assembly)
                .Where(t => t.BaseType == typeof(BaseEntity))
                .AsSelf();
        }

        public static IContainer ResolveSectionFactory(ContainerBuilder builder)
        {
            var container = builder.Build();
            container.Resolve<ISessionFactory>();
            return container;
        }
    }
}
