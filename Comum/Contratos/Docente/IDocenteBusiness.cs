using Entidades;
using System.Collections.Generic;

namespace Comum.Contratos
{
    public interface IDocenteBusiness : INegocioBase<Docente>
    {
        List<Docente> ListarTodos(Docente docente, int paginaAtual);

        int TotalRegistros(Docente docente);

        bool VerificarPreenchimentoCamposObrigatorios(Docente docente);
    }
}
