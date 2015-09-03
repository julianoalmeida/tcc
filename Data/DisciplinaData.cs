using Entidades;
using NHibernate;

namespace Data
{
    public interface IDisciplinaData : IRepositorio<Courses> { }

    public class DisciplinaData : RepositorioNHibernate<Courses>, IDisciplinaData
    {
        public DisciplinaData(ISession session)
            : base(session)
        { }
    }
}
