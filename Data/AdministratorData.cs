using System.Collections.Generic;
using System.Linq;
using Comum;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IAdministratorData : INHibernateRepository<Administrator>
    {
        int Total(Administrator administrator);
        List<Administrator> SelectWithPagination(Administrator administrator, int startPage);
    }

    public class AdministratorData : NHibernateRepository<Administrator>, IAdministratorData
    {
        public AdministratorData(ISession session)
            : base(session)
        { }

        public List<Administrator> SelectWithPagination(Administrator administrator, int startPage)
        {
            return Filter(administrator).Skip(startPage).Take(Constants.TOTAL_REGISTRO_POR_PAGINAS).ToList();
        }
        
        public int Total(Administrator administrator)
        {
            return Filter(administrator).Count();
        }

        private IEnumerable<Administrator> Filter(Administrator administrator)
        {
            return GetAll().Where(a => string.IsNullOrEmpty(administrator.Person.Name) ||
                                       a.Person.Name.ToLower().Contains(administrator.Person.Name.ToLower()));
        }
    }
}
