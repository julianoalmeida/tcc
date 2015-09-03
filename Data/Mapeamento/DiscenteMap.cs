using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class DiscenteMap : ClassMap<Discente>
    {
        public DiscenteMap()
        {
            Table("DISCENTE");
            Id(a => a.Id, "DISC_CD_DISCENTE");
            References<Pessoa>(a => a.Pessoa, "PESS_CD_PESSOA").Not.Nullable().Cascade.All(); ;
            Map(a => a.Escolaridade, "ESCO_CD_ESCOLARIDADE").Not.Nullable();
            Map(a => a.Matricula, "DISC_DS_MATRICULA").Nullable();

            HasManyToMany<Turma>(x => x.Turmas)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("TURM_CD_TURMA")
                .ChildKeyColumn("DISC_CD_DISCENTE")
                .Cascade.All();
        }
    }
}
