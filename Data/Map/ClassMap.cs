using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class ClassMap : ClassMap<Class>
    {
        public ClassMap()
        {
            Table("TURMA");
            Id(a => a.Id, "Id");
            Map(a => a.Description, "Descricao").Not.Nullable();
            Map(a => a.Capacity, "Capacidade").Not.Nullable();
            Map(a => a.ClassTime, "Turno").Nullable();            
            References(x => x.Teacher, "IdDocente").Cascade.SaveUpdate();

            HasManyToMany(x => x.Students)
                .Table("TURMA_DISCENTE")
                .ParentKeyColumn("IdTurma")
                .ChildKeyColumn("IdDiscente")
                .Cascade.All();
        }
    }
}
