using Entidades;
using NHibernate;

namespace Data
{
    public interface IPersonData : INHibernateRepository<Person>
    {
        bool IsDuplicated(Person person);
    }

    public class PersonData : NHibernateRepository<Person>, IPersonData
    {
        public PersonData(ISession session)
            : base(session)
        { }

        public bool IsDuplicated(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}
