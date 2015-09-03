using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface ITurmaData : IRepositorio<Class>
    {
        int Total(Class model);
        List<Class> SelectWithPagination(Class model, int startPage);
    }

    public class TurmaData : RepositorioNHibernate<Class>, ITurmaData
    {
        public TurmaData(ISession session)
            : base(session)
        { }

        public List<Class> SelectWithPagination(Class model, int startPage)
        {
            return Filter(model)
                .Skip(startPage)
                .Take(Constants.TOTAL_REGISTRO_POR_PAGINAS)
                .ToList();
        }
        
        public int Total(Class model)
        {
            return Filter(model).Count();
        }

        private IEnumerable<Class> Filter(Class @class)
        {
            return GetAll()
                .Where(
                    a =>
                        string.IsNullOrEmpty(@class.Description) ||
                        a.Description.ToLower().Contains(@class.Description.ToLower()))
                .Where(a => @class.ClassTime == 0 || a.ClassTime == @class.ClassTime)
                .OrderBy(a => a.Description);
        }
    }
}
