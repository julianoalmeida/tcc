using Comum;
using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositorio
{
    public class DocenteData : RepositorioNHibernate<Docente>, IDocenteData
    {
        public DocenteData(ISession session)
            : base(session)
        {
        }

        public List<Docente> ListarTodos(Docente docente, int paginaAtual)
        {
            var retorno = Listar()
                .Where(a => string.IsNullOrEmpty(docente.Pessoa.Nome) || a.Pessoa.Nome.ToLower().Contains(docente.Pessoa.Nome.ToLower()))
                .Skip(paginaAtual)
                .Take(Constantes.TOTAL_REGISTRO_POR_PAGINAS)
                .ToList();
            return retorno;
        }

        public int TotalRegistros(Docente docente)
        {
            return Listar()
                .Where(a => string.IsNullOrEmpty(docente.Pessoa.Nome) || a.Pessoa.Nome.ToLower().Contains(docente.Pessoa.Nome.ToLower()))
                .ToList().Count;
        }

    }
}
