using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IStudentData : INHibernateRepository<Student>
    {
        int Total(Student docente);
        List<Student> SelectWithPagination(Student docente, int paginaAtual);
    }

    public class StudentData : NHibernateRepository<Student>, IStudentData
    {
        public StudentData(ISession session)
            : base(session)
        { }

        public List<Student> SelectWithPagination(Student student, int startPage)
        {
            return Filter(student).Skip(startPage).Take(Constants.TOTAL_PAGE_REGISTERS).ToList();
        }

        public int Total(Student student)
        {
            return Filter(student).Count();
        }

        private IEnumerable<Student> Filter(Student student)
        {
            return
                GetAll()
                    .Where(
                        a =>
                            string.IsNullOrEmpty(student.Person.Name) ||
                            a.Person.Name.ToLower().Contains(student.Person.Name.ToLower()));
        }
    }
}

