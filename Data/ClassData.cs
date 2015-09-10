using System;
using System.Collections.Generic;
using System.Linq;
using Comum;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IClassData : IBaseRepositoryRepository<Class>
    {
        int Total(Class model);
        List<Class> SelectWithPagination(Class model, int startPage);
    }

    public class ClassData : BaseRepositoryRepository<Class>, IClassData
    {
        public ClassData(ISession session)
            : base(session)
        { }

        public List<Class> SelectWithPagination(Class model, int startPage)
        {
            return Filter(model)
                .Skip(startPage)
                .Take(Constants.TOTAL_PAGE_REGISTERS)
                .ToList();
        }

        public int Total(Class model)
        {
            return Filter(model).Count();
        }

        private IEnumerable<Class> Filter(Class @class)
        {
            return GetAll().Values
                .Where(ClassFilterCondition(@class))
                .OrderBy(a => a.Description);
        }

        private static Func<Class, bool> ClassFilterCondition(Class @class)
        {
            return a => string.IsNullOrEmpty(@class.Description) || a.Description.ToLower().Contains(@class.Description.ToLower())
                        && @class.ClassTime == 0 || a.ClassTime == @class.ClassTime;
        }
    }
}