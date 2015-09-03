using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comum.Contratos.Turmas
{
    public interface ITurmaData : IRepositorio<Turma>
    {
        List<Turma> ListarTodos(Turma model, int ? turno , int paginaAtual);

        int TotalRegistros(Turma model);
    }
}
