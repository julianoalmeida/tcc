using System;
using System.Collections.Generic;
using System.Linq;
using Comum;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface ITeacherData : IBaseRepositoryRepository<Teacher>
    {
        int Total(Teacher teacher);
        List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual);
    }

    public class TeacherData : BaseRepositoryRepository<Teacher>, ITeacherData
    {
        public TeacherData(ISession session)
            : base(session)
        { }

        public List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual)
        {
            return Filter(teacher).Skip(paginaAtual).Take(Constants.TOTAL_PAGE_REGISTERS).ToList();
        }
        
        public int Total(Teacher teacher)
        {
            return Filter(teacher).Count();
        }

        private IEnumerable<Teacher> Filter(Teacher teacher)
        {
            return
                GetAll().Values
                    .Where(
                        TeacherFilterCondition(teacher));
        }

        private static Func<Teacher, bool> TeacherFilterCondition(Teacher teacher)
        {
            return a =>
                string.IsNullOrEmpty(teacher.Person.Name) ||
                a.Person.Name.ToLower().Contains(teacher.Person.Name.ToLower());
        }
    }
}
