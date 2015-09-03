using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositorio
{
    public class AdministradorData : RepositorioNHibernate<Administrador>, IAdministradorData
    {
        public AdministradorData(ISession session)
            : base(session)
        {
        }

        public List<Administrador> ListarTodos(Administrador administrador, int paginaAtual)
        {
            var pesquisa = Listar().Where(a => string.IsNullOrEmpty(administrador.Pessoa.Nome) || a.Pessoa.Nome.ToLower().Contains(administrador.Pessoa.Nome.ToLower()))
                     .ToList();

            return pesquisa;
        }

        public int TotalRegistros(Administrador administrador)
        {
            return 5;
        }

    }
}
