
using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;

namespace Data.Repositorio
{
    public class CidadeData : RepositorioNHibernate<Cidade>, ICidadeData
    {
        public CidadeData(ISession session)
            : base(session)
        {
        }

    }
}
