using Comum.Contratos;
using Entidades;
using NHibernate;
using System.Collections.Generic;

namespace Data.Repositorio
{
    public class DisciplinaData : RepositorioNHibernate<Disciplina>, IDisciplinaData
    {
        public DisciplinaData(ISession session)
            : base(session)
        {
        }

    }
}
