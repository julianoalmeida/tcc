using Entidades;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapeamento
{
    public class DisciplinaMap : ClassMap<Disciplina>
    {
        public DisciplinaMap()
        {
            Table("DISCIPLINA");
            Id(x => x.Id, "DISC_ID_DISCIPLINA").Not.Nullable();
            Map(x => x.Descricao, "DISC_DESC_DISCIPLINA").Not.Nullable();

            HasManyToMany<Disciplina>(x => x.Docentes)
                   .Table("DOCENTE_DISCIPLINAS")
                .ParentKeyColumn("DOCE_ID_DOCENTE")
                .ChildKeyColumn("DOCE_ID_DISCIPLINA")
                .Cascade.All();
        }
    }
}
