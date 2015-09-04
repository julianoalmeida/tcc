using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class CityMap : ClassMap<City>
    {
        public CityMap()
        {
            Table("CIDADE");
            Id(x => x.Id, "Id");
            Map(x => x.Name, "Descricao");
            References(x => x.State, "SiglaEstado").Not.Nullable().Cascade.All();
        }
    }
}
