using System.Collections.Generic;
using System.Linq;
using Comum;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IAdmData : IBaseRepositoryRepository<Adm>
    {
        int Total(Adm adm);
        List<Adm> SelectWithPagination(Adm adm, int startPage);
    }

    public class AdmData : BaseRepositoryRepository<Adm>, IAdmData
    {
        public AdmData(ISession session)
            : base(session)
        { }

        public List<Adm> SelectWithPagination(Adm adm, int startPage)
        {
            return Filter(adm).Skip(startPage).Take(Constants.TOTAL_PAGE_REGISTERS).ToList();
        }
        
        public int Total(Adm adm)
        {
            return Filter(adm).Count();
        }

        private IEnumerable<Adm> Filter(Adm adm)
        {
            return GetAll().Values.Where(a => string.IsNullOrEmpty(adm.Person.Name) ||
                                       a.Person.Name.ToLower().Contains(adm.Person.Name.ToLower()));
        }
    }
}
