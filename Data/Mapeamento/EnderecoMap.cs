using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class EnderecoMap : ClassMap<Endereco>
    {
        public EnderecoMap()
        {
            Table("ENDERECO");
            Id(x => x.Id, "cmpIdEndereco");
            Map(x => x.CodigoUf, "cmpCoUf").Update().Nullable();
            Map(x => x.IdCidadeBrasil, "cmpIdCidadeBrasil").Update().Nullable();
            Map(x => x.NomeEndereco, "cmpEdEndereco").Nullable();
            Map(x => x.DescricaoBairro, "cmpDcBairro").Nullable();
            Map(x => x.Cep, "cmpNuCep").Nullable();
        }
    }
}
