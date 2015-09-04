using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class StateMap : ClassMap<State>
    {
        public StateMap()
        {
            Table("UF");
            Id(x => x.Code, "Id");
            Map(x => x.Name, "Descricao");
            References(x => x.Country, "IdPais");
            HasMany(x => x.Cities).KeyColumn("SiglaEstado").Inverse();
        }
    }
}