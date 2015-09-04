using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Table("DOCENTE");
            Id(a => a.Id, "Id");
            References(a => a.Person, "IdPessoa").Cascade.All();
            Map(a => a.Education, "IdEscolaridade").Not.Nullable();

            HasManyToMany(x => x.Courses)
                .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("IdDocente")
                .ChildKeyColumn("IdDisciplina")
                .Cascade.All();
        }
    }
}