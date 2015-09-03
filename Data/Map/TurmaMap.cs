using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class TurmaMap : ClassMap<Class>
    {
        public TurmaMap()
        {
            Table("TURMA");
            Id(a => a.Id, "TURM_CD_TURMA");
            Map(a => a.Description, "TURM_DS_TURMA").Not.Nullable();
            Map(a => a.ClassTime, "TURM_ID_TURNO").Nullable();
            References<Teacher>(x => x.Teacher, "TURM_ID_DOCENTE").Cascade.SaveUpdate();

            HasManyToMany<Student>(x => x.Students)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("TURM_CD_TURMA")
                .ChildKeyColumn("DISC_CD_DISCENTE")
                .Cascade.All();
        }
    }
}
