using Comum;
using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositorio
{
    public class DiscenteData : RepositorioNHibernate<Discente>, IDiscenteData
    {
        public DiscenteData(ISession session)
            : base(session)
        {
        }

        public List<Discente> ListarTodos(Discente discente, int paginaAtual)
        {
            return Listar().Where(a => string.IsNullOrEmpty(discente.Pessoa.Nome) || a.Pessoa.Nome.ToLower().Contains(discente.Pessoa.Nome.ToLower()))
                .Skip(paginaAtual).Take(Constantes.TOTAL_REGISTRO_POR_PAGINAS)
                .ToList();
        }

        public int TotalRegistros(Discente discente)
        {
            return Listar().Where(a => string.IsNullOrEmpty(discente.Pessoa.Nome) || a.Pessoa.Nome.ToLower().Contains(discente.Pessoa.Nome.ToLower())).Count();
        }
    }
}

