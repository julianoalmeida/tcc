using Entidades;
using System.Collections.Generic;

namespace Comum.Contratos
{
    public interface IDiscenteBusiness : INegocioBase<Discente>
    {
        List<Discente> ListarTodos(Discente docente, int paginaAtual);

        int TotalRegistros(Discente docente);

        string GerarNumeroMatricula(Discente discente);

        bool VerificarPreenchimentoEscolaridade(Discente discente);
    }
}
