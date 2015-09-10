using System;
using System.Linq;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IPersonData : IBaseRepositoryRepository<Person>
    {
        bool IsDuplicated(Person person);
    }

    public class PersonData : BaseRepositoryRepository<Person>, IPersonData
    {
        public PersonData(ISession session)
            : base(session)
        { }

        public bool IsDuplicated(Person person)
        {
            return
                GetAll().Values
                    .Any(DuplicatedPersonCondition(person));
        }

        private static Func<Person, bool> DuplicatedPersonCondition(Person person)
        {
            return a => a.Name == person.Name && a.BirthDate?.Date == person.BirthDate?.Date && a.Id != person.Id;
        }
    }
}
