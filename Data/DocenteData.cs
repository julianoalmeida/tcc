using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IDocenteData : IRepositorio<Teacher>
    {
        int Total(Teacher teacher);
        List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual);
    }

    public class DocenteData : RepositorioNHibernate<Teacher>, IDocenteData
    {
        public DocenteData(ISession session)
            : base(session)
        { }

        public List<Teacher> SelectWithPagination(Teacher teacher, int paginaAtual)
        {
            return Filter(teacher).Skip(paginaAtual).Take(Constants.TOTAL_REGISTRO_POR_PAGINAS).ToList();
        }
        
        public int Total(Teacher teacher)
        {
            return Filter(teacher).Count();
        }

        private IEnumerable<Teacher> Filter(Teacher teacher)
        {
            return
                GetAll()
                    .Where(
                        a =>
                            string.IsNullOrEmpty(teacher.Person.Name) ||
                            a.Person.Name.ToLower().Contains(teacher.Person.Name.ToLower()));
        }
    }
}
