using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using System.Collections.Generic;

namespace Negocio.Servico
{
    public class AdministradorBusiness : NegocioBase<Administrador>, IAdministradorBusiness
    {
        #region INJEÇÃO

        private readonly IAdministradorData _administradorData;

        public AdministradorBusiness(IAdministradorData administradorData)
            : base(administradorData)
        {
            _administradorData = administradorData;
        }
        #endregion

        #region CONSULTAS
        public List<Administrador> ListarTodos(Administrador administrador, int paginaAtual)
        {
            return _administradorData.ListarTodos(administrador, paginaAtual);
        }

        public int TotalRegistros(Administrador administrador)
        {
            return _administradorData.TotalRegistros(administrador);
        }

        #endregion

    }
}
