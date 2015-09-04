using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class StudentMap : ClassMap<Student>
    {
        public StudentMap()
        {
            Table("DISCENTE");
            Id(a => a.Id, "Id");
            References(a => a.Person, "IdPessoa").Not.Nullable().Cascade.All(); ;
            Map(a => a.Education, "IdEscolaridade").Not.Nullable();
            Map(a => a.RegistrationNumber, "NumeroMatricula").Nullable();

            HasManyToMany(x => x.Classes)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("IdTurma")
                .ChildKeyColumn("IdDiscente")
                .Cascade.All();
        }
    }
}