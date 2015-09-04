using Entidades;
using NHibernate;

namespace Data
{
    public interface ICityData : INHibernateRepository<City> { }

    public class CityData : NHibernateRepository<City>, ICityData
    {
        public CityData(ISession session)
            : base(session)
        { }
    }
}