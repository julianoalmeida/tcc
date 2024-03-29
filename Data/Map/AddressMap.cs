﻿using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table("ENDERECO");
            Id(x => x.Id, "Id");
            Map(x => x.City, "IdCidade").Not.Nullable();
            Map(x => x.Neighborhood, "Bairro").Nullable();
            Map(x => x.ZipCode, "Cep").Update().Nullable();
            Map(a => a.Number, "NumeroCasa").Not.Nullable();
            Map(x => x.StreetName, "Logradouro").Nullable();
        }
    }
}