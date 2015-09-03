using Entidades;
using NHibernate;

namespace Data
{
    public interface IEstadoData : IRepositorio<State> { }

    public class EstadoData : RepositorioNHibernate<State>, IEstadoData
    {
        public EstadoData(ISession session)
            : base(session)
        { }
    }
}