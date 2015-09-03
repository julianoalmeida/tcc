using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IDiscenteData : IRepositorio<Student>
    {
        int Total(Student docente);
        List<Student> SelectWithPagination(Student docente, int paginaAtual);
    }

    public class DiscenteData : RepositorioNHibernate<Student>, IDiscenteData
    {
        public DiscenteData(ISession session)
            : base(session)
        { }

        public List<Student> SelectWithPagination(Student student, int startPage)
        {
            return Filter(student).Skip(startPage).Take(Constants.TOTAL_REGISTRO_POR_PAGINAS).ToList();
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

