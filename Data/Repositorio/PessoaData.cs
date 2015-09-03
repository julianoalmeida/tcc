using Comum.Contratos;
using Entidades;
using Entidades.Extensions;
using NHibernate;
using System.Linq;

namespace Data.Repositorio
{
    public class PessoaData : RepositorioNHibernate<Pessoa>, IPessoaData
    {
        public PessoaData(ISession session)
            : base(session)
        {

        }

        public bool verificarDuplicidade(Pessoa pessoa)
        {
            throw new System.NotImplementedException();
        }
    }
}
