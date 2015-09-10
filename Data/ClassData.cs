using System;
using System.Linq;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IClassData : IBaseRepositoryRepository<Class>
    {
        bool IsDuplicated(Class entity);
    }

    public class ClassData : BaseRepositoryRepository<Class>, IClassData
    {
        public ClassData(ISession session) : base(session)
        { }

        public bool IsDuplicated(Class entity)
        {
            return SelectWithFilter(DuplicatedClassCondition(entity)).Values.Any();
        }

        private static Func<Class, bool> DuplicatedClassCondition(Class entity)
        {
            return a => a.Description.ToLower().Equals(entity.Description.ToLower()) && a.Id != entity.Id;
        }
    }
}