using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class EstadoMap : ClassMap<State>
    {
        public EstadoMap()
        {
            Table("UF");
            Id(x => x.Code, "UF_SG_UF");
            Map(x => x.Name, "UF_NM_UF");
            References(x => x.Country, "cmpCoPais");
            HasMany(x => x.Cities).KeyColumn("cmpCoUF").Inverse();
        }
    }
    
}
