using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class PaisMap : ClassMap<Pais>
    {
        public PaisMap()
        {
            Table("dbcorporativo.dbo.tblPais");
            Id(x => x.Codigo, "cmpCoPais");
            Map(x => x.Nome, "cmpNoPais");
            HasMany<Estado>(x => x.Estados).KeyColumn("cmpCoPais").Inverse();
        }
    }
}
