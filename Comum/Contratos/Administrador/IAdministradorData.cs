using Entidades;
using System.Collections.Generic;

namespace Comum.Contratos
{
    public interface IAdministradorData : IRepositorio<Administrador>
    {
        List<Administrador> ListarTodos(Administrador administrador, int paginaAtual);

        int TotalRegistros(Administrador administrador);
    }
}
