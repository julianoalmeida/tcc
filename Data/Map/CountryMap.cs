using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Table("PAIS");
            Id(x => x.Codigo, "Id");
            Map(x => x.Name, "Descricao");
            HasMany(x => x.States).KeyColumn("IdPais").Inverse();
        }
    }
}