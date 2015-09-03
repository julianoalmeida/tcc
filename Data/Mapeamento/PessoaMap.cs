using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("PESSOA");
            Id(a => a.Id, "PESS_CD_PESSOA");
            References<Endereco>(a => a.Endereco, "ENDE_CD_ENDERECO").Not.Nullable().Cascade.All();
            Map(a => a.Nome, "PESS_NM_PESSOA").Not.Nullable();
            Map(a => a.Cpf, "PESS_DS_CPF").Not.Nullable();

            Map(a => a.Sexo, "PESS_SG_SEXO").Not.Nullable();
            Map(a => a.DataNascimento, "PESS_DT_NASCIMENTO").Not.Nullable();
            Map(a => a.Email, "PESS_EMAIL").Nullable();
            Map(a => a.Telefone, "PESS_NM_TELEFONE").Nullable();
            Map(a => a.Celular, "PESS_NM_CELULAR").Nullable();
            Map(a => a.Observacao, "PESS_NM_OBSERVACAO").Nullable();
            Map(a => a.EstadoCivil, "PESS_SG_ESTADO_CIVIL").Not.Nullable();

        }
    }
}
