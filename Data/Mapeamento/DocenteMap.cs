using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class DocenteMap : ClassMap<Docente>
    {
        public DocenteMap()
        {
            Table("DOCENTE");
            Id(a => a.Id, "DOCE_CD_DOCENTE");
            References<Pessoa>(a => a.Pessoa, "PESS_CD_PESSOA").Cascade.All();
            Map(a => a.Escolaridade, "ESCO_CD_ESCOLARIDADE").Not.Nullable();

            HasManyToMany<Disciplina>(x => x.Disciplinas)
                .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("DOCE_ID_DOCENTE")
                .ChildKeyColumn("DOCE_ID_DISCIPLINA")
                .Cascade.All();

        }

    }
}
