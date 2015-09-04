using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IAdmData : INHibernateRepository<Adm>
    {
        int Total(Adm adm);
        List<Adm> SelectWithPagination(Adm adm, int startPage);
    }

    public class AdmData : NHibernateRepository<Adm>, IAdmData
    {
        public AdmData(ISession session)
            : base(session)
        { }

        public List<Adm> SelectWithPagination(Adm adm, int startPage)
        {
            return Filter(adm).Skip(startPage).Take(Constants.TOTAL_REGISTRO_POR_PAGINAS).ToList();
        }
        
        public int Total(Adm adm)
        {
            return Filter(adm).Count();
        }

        private IEnumerable<Adm> Filter(Adm adm)
        {
            return GetAll().Where(a => string.IsNullOrEmpty(adm.Person.Name) ||
                                       a.Person.Name.ToLower().Contains(adm.Person.Name.ToLower()));
        }
    }
}
