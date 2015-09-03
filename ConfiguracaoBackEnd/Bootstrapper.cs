using Autofac;
using Autofac.Extras.DynamicProxy2;
using Autofac.Integration.Mvc;
using ConfiguracaoBackEnd.Interceptadores;
using Data.Inicializacao;
using Data.Repositorio;
using Entidades;
using Negocio.Servico;
using NHibernate;

namespace AutoFacConfig
{
    public class ContainerHelper
    {
        public static ContainerBuilder ObterContainerBuilder()
        {
            //Inicializar o container
            var builder = new ContainerBuilder();

            #region FLUENT
            //Registra no container a sessão do Fluent de acordo com a conexão e definido o contexto da sessão.
            builder.Register(x => FluentSessionFactoryFactory.GetSessionFactory("thread_static", "coonStringTcc"))
                .SingleInstance();
            #endregion

            #region NHIBERNATE
            builder.RegisterType<NHibernateInterceptor>()
                .SingleInstance()
                .AsSelf();
            builder.Register(x =>
            {
                var session = x.Resolve<ISessionFactory>().OpenSession(x.Resolve<NHibernateInterceptor>());
                session.FlushMode = FlushMode.Commit;
                return session;
            })
                .InstancePerHttpRequest()
                .OnRelease(x => x.Dispose());
            #endregion

            #region REPOSITORIOS
            // Registrando no container os repositórios implementados com NHibernate            
            var dataAccess = typeof(RepositorioNHibernate<>).Assembly;

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Data"))
                .AsImplementedInterfaces();

            #endregion

            #region SERVICOS
            // Registrando no container os Serviços e seus interceptadores
            //TODO : alterar para typeof (ServicoCrud<> )
            var servicos = typeof(NegocioBase<>).Assembly;

            builder.RegisterAssemblyTypes(servicos)
                .Where(t => t.Name.EndsWith("Business"))
                .AsImplementedInterfaces()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(ServiceTransactionInterceptor));

            builder.RegisterType<ServiceTransactionInterceptor>()
                .InstancePerHttpRequest()
                .AsSelf();

            #endregion

            #region ENTIDADES
            //Registra as entidades no container pelo seu próprio tipo concreto
            //Registrando no container as entidades
            var entidades = typeof(Entidade).Assembly;

            builder.RegisterAssemblyTypes(entidades)
                .Where(t => t.BaseType == typeof(Entidade))
                .AsSelf();
            #endregion

            return builder;

        }
    }
}
