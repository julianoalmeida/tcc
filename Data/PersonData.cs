using System;
using System.Linq;
using Comum;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IPersonData : IBaseRepositoryRepository<Person>
    {
        bool IsDuplicated(Person entity);
    }

    public class PersonData : BaseRepositoryRepository<Person>, IPersonData
    {
        public PersonData(ISession session) : base(session)
        { }

        public bool IsDuplicated(Person entity)
        {
            return SelectWithFilter(DuplicatedPersonCondition(entity)).Values.Any();
        }

        public static Func<Person, bool> DuplicatedPersonCondition(Person person)
        {
            return a => a.Name == person.Name && a.Email == person.Email && a.Id != person.Id;
        }
    }
}
