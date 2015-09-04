using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class CourseMap : ClassMap<Courses>
    {
        public CourseMap()
        {
            Table("DISCIPLINA");
            Id(x => x.Id, "Id").Not.Nullable();
            Map(x => x.Description, "Descricao").Not.Nullable();

            HasManyToMany<Courses>(x => x.Teachers)
                   .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("IdDocente")
                .ChildKeyColumn("IdDisciplina")
                .Cascade.All();
        }
    }
}
