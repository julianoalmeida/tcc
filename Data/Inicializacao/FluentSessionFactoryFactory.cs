using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace Data.Inicializacao
{
    public class FluentSessionFactoryFactory
    {
        /// <summary>
        /// Configura toda a sessão do NHibernate via Fluent.
        /// </summary>
        /// <param name="currentSessionContextClass">Sessão do contexto atual</param>
        /// <param name="connectionStringKey">Chave da string de conexão</param>
        /// <returns>SessionFactory do NHibernate</returns>
        public static ISessionFactory GetSessionFactory(string currentSessionContextClass, string connectionStringKey)
        {
            
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008                                                 // define o dialeto.
                    .ShowSql()                                                                  // define apresentação do SQL.
                    .ConnectionString(c => c.FromConnectionStringWithKey(connectionStringKey))) // define string de conexão.
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(FluentSessionFactoryFactory).Assembly)) // define todos os mapeamentos encontrado dentro do assembly onde o FluentSessionFactoryFactory se encontra.                
                .ExposeConfiguration(x => x.SetProperty(Environment.CurrentSessionContextClass, currentSessionContextClass)) // permite alteração das configurações do NHibernate após ser criado.
                .Cache(x => x.UseQueryCache()) // define a utilização de cache.                
                .BuildSessionFactory(); // constroi a SessionFactory do NHibernate.
        }
    }
}
