using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class CidadeMap : ClassMap<City>
    {
        public CidadeMap()
        {
            Table("CIDADE");
            Id(x => x.Id, "CIDA_CD_CIDADE");
            Map(x => x.Name, "CIDA_DS_CIDADE");
            References<State>(x => x.State, "UF_SG_UF");
        }
    }
}
