using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class CidadeMap : ClassMap<Cidade>
    {
        public CidadeMap()
        {
            Table("dbcorporativo.dbo.tblCidadeBrasil");
            Id(x => x.Id, "cmpIdCidadeBrasil");
            Map(x => x.Nome, "cmpDcCidadeBrasil");
            References<Estado>(x => x.Estado, "cmpCoUF");
        }
    }
}
