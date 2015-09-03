using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class TurmaMap : ClassMap<Turma>
    {
        public TurmaMap()
        {
            Table("TURMA");
            Id(a => a.Id, "TURM_CD_TURMA");
            Map(a => a.Descricao, "TURM_DS_TURMA").Not.Nullable();
            Map(a => a.Turno, "TURM_ID_TURNO").Nullable();
            References<Docente>(x => x.Docente, "TURM_ID_DOCENTE").Cascade.SaveUpdate();

            HasManyToMany<Discente>(x => x.Discentes)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("TURM_CD_TURMA")
                .ChildKeyColumn("DISC_CD_DISCENTE")
                .Cascade.All();
        }
    }
}
