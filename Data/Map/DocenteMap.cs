using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class DocenteMap : ClassMap<Teacher>
    {
        public DocenteMap()
        {
            Table("DOCENTE");
            Id(a => a.Id, "DOCE_CD_DOCENTE");
            References<Person>(a => a.Person, "PESS_CD_PESSOA").Cascade.All();
            Map(a => a.Education, "ESCO_CD_ESCOLARIDADE").Not.Nullable();

            HasManyToMany<Courses>(x => x.Courses)
                .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("DOCE_ID_DOCENTE")
                .ChildKeyColumn("DOCE_ID_DISCIPLINA")
                .Cascade.All();

        }

    }
}
