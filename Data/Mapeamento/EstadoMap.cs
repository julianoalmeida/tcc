using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Table("dbcorporativo.dbo.tblUF");
            Id(x => x.Codigo, "cmpCoUf");
            Map(x => x.Nome, "cmpDcUf");
            References<Pais>(x => x.Pais, "cmpCoPais");
            HasMany(x => x.Cidades).KeyColumn("cmpCoUF").Inverse();
        }
    }
}
