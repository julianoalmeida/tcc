using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class AdmMap : ClassMap<Adm>
    {
        public AdmMap()
        {
            Table("ADMINISTRADOR");
            Id(a => a.Id, "Id");
            References(a => a.Person, "IdPessoa").Not.Nullable().Cascade.All();
        }
    }
}