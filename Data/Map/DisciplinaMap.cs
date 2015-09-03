using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class DisciplinaMap : ClassMap<Courses>
    {
        public DisciplinaMap()
        {
            Table("DISCIPLINA");
            Id(x => x.Id, "DISC_ID_DISCIPLINA").Not.Nullable();
            Map(x => x.Description, "DISC_DESC_DISCIPLINA").Not.Nullable();

            HasManyToMany<Courses>(x => x.Teachers)
                   .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("DOCE_ID_DOCENTE")
                .ChildKeyColumn("DOCE_ID_DISCIPLINA")
                .Cascade.All();
        }
    }
}
