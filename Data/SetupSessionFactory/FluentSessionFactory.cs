using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;

namespace Data.SetupSessionFactory
{
    public class FluentSessionFactory
    {   
        public static ISessionFactory GetSessionFactory(string currentSessionContextClass, string connectionStringKey)
        {
            
            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012                                                 
                    .ShowSql()                                                                  
                    .ConnectionString(c => c.FromConnectionStringWithKey(connectionStringKey))) 
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(FluentSessionFactory).Assembly))
                .ExposeConfiguration(x => x.SetProperty(Environment.CurrentSessionContextClass, currentSessionContextClass))
                .Cache(x => x.UseQueryCache()) 
                .BuildSessionFactory(); 
        }
    }
}
