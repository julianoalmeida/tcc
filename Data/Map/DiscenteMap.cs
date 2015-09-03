using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class DiscenteMap : ClassMap<Student>
    {
        public DiscenteMap()
        {
            Table("DISCENTE");
            Id(a => a.Id, "DISC_CD_DISCENTE");
            References<Person>(a => a.Person, "PESS_CD_PESSOA").Not.Nullable().Cascade.All(); ;
            Map(a => a.Education, "ESCO_CD_ESCOLARIDADE").Not.Nullable();
            Map(a => a.RegistrationNumber, "DISC_DS_MATRICULA").Nullable();

            HasManyToMany<Class>(x => x.Classes)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("TURM_CD_TURMA")
                .ChildKeyColumn("DISC_CD_DISCENTE")
                .Cascade.All();
        }
    }
}
