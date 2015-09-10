using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IPersonData : IBaseRepositoryRepository<Person>
    { }

    public class PersonData : BaseRepositoryRepository<Person>, IPersonData
    {
        public PersonData(ISession session) : base(session)
        { }
    }
}
