using Entidades;
using System.Collections.Generic;

namespace Comum.Contratos
{
    public interface IDiscenteData : IRepositorio<Discente>
    {
        List<Discente> ListarTodos(Discente docente, int paginaAtual);

        int TotalRegistros(Discente docente);
    }
}
