using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositorio
{
    public class EstadoData : RepositorioNHibernate<Estado>, IEstadoData
    {
        public EstadoData(ISession session)
            : base(session)
        {

        }
    }
}
