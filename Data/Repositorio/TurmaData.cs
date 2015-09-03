using Comum;
using Comum.Contratos.Turmas;
using Entidades;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositorio
{
    public class TurmaData : RepositorioNHibernate<Turma>, ITurmaData
    {
        public TurmaData(ISession session)
            : base(session)
        {
        }

        public List<Turma> ListarTodos(Turma model, int? turno, int paginaAtual)
        {
            turno = turno ?? 0;
            var turmas = Listar().Where(a => string.IsNullOrEmpty(model.Descricao) || a.Descricao.ToLower().Contains(model.Descricao.ToLower()))
                .Where(a => turno.Value == 0 || a.Turno == turno.Value)
                .OrderBy(a => a.Descricao)
                .Skip(paginaAtual)
                .Take(Constantes.TOTAL_REGISTRO_POR_PAGINAS)
                .ToList();

            return turmas;
        }

        public int TotalRegistros(Turma model)
        {
            return Listar().ToList().Count;
        }


    }
}
