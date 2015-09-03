using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class EnderecoMap : ClassMap<Address>
    {
        public EnderecoMap()
        {
            Table("ENDERECO");
            Id(x => x.Id, "cmpIdEndereco");
            Map(x => x.State, "cmpCoUf").Update().Nullable();
            Map(x => x.CityId, "cmpIdCidadeBrasil").Update().Nullable();
            Map(x => x.StreetName, "cmpEdEndereco").Nullable();
            Map(x => x.Neighborhood, "cmpDcBairro").Nullable();
            Map(x => x.ZipCode, "cmpNuCep").Nullable();
        }
    }
}
