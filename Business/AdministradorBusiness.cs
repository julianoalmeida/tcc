using System.Collections.Generic;
using Data;
using Entidades;

namespace Negocio
{
    public interface IAdministradorBusiness : INegocioBase<Administrator>
    {
        int Total(Administrator administrator);
        List<Administrator> SelectWithPagination(Administrator administrator, int paginaAtual);
    }

    public class AdministradorBusiness : BaseBusiness<Administrator>, IAdministradorBusiness
    {
        private readonly IAdministratorData _administratorData;
        public AdministradorBusiness(IAdministratorData administratorData)
            : base(administratorData)
        {
            _administratorData = administratorData;
        }

        public int Total(Administrator administrator)
        {
            return _administratorData.Total(administrator);
        }

        public List<Administrator> SelectWithPagination(Administrator administrator, int paginaAtual)
        {
            return _administratorData.SelectWithPagination(administrator, paginaAtual);
        }
    }
}