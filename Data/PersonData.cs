using System.Linq;
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
            return
                GetAll()
                    .Any(a => a.Name == person.Name && a.BirthDate?.Date == person.BirthDate?.Date && a.Id != person.Id);
        }
    }
}
