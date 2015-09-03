using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class PessoaMap : ClassMap<Person>
    {
        public PessoaMap()
        {
            Table("PESSOA");
            Id(a => a.Id, "PESS_CD_PESSOA");
            References<Address>(a => a.Address, "ENDE_CD_ENDERECO").Not.Nullable().Cascade.All();
            Map(a => a.Name, "PESS_NM_PESSOA").Not.Nullable();
            Map(a => a.ZipCode, "PESS_DS_CPF").Not.Nullable();

            Map(a => a.Sex, "PESS_SG_SEXO").Not.Nullable();
            Map(a => a.BirthDate, "PESS_DT_NASCIMENTO").Not.Nullable();
            Map(a => a.Email, "PESS_EMAIL").Nullable();
            Map(a => a.PhoneNumber, "PESS_NM_TELEFONE").Nullable();
            Map(a => a.MobileNumber, "PESS_NM_CELULAR").Nullable();
            Map(a => a.Observacao, "PESS_NM_OBSERVACAO").Nullable();
            Map(a => a.MaritalState, "PESS_SG_ESTADO_CIVIL").Not.Nullable();

        }
    }
}
