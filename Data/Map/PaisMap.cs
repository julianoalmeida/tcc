using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class PaisMap : ClassMap<Country>
    {
        public PaisMap()
        {
            Table("PAIS");
            Id(x => x.Codigo, "cmpCoPais");
            Map(x => x.Name, "cmpNoPais");
            HasMany<State>(x => x.States).KeyColumn("cmpCoPais").Inverse();
        }
    }
}
