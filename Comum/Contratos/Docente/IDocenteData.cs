using Entidades;
using System.Collections.Generic;

namespace Comum.Contratos
{
    public interface IDocenteData : IRepositorio<Docente>
    {
        List<Docente> ListarTodos(Docente docente, int paginaAtual);

        int TotalRegistros(Docente docente);

    }
}
