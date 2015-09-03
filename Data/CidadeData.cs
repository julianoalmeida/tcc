using Entidades;
using NHibernate;

namespace Data
{
    public interface ICidadeData : IRepositorio<City> { }

    public class CidadeData : RepositorioNHibernate<City>, ICidadeData
    {
        public CidadeData(ISession session)
            : base(session)
        { }
    }
}
