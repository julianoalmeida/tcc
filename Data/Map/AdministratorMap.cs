using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class AdministratorMap : ClassMap<Administrator>
    {
        public AdministratorMap()
        {
            Table("ADMINISTRADOR");
            Id(a => a.Id, "Id");
            References(a => a.Person, "IdPessoa").Not.Nullable().Cascade.All();
        }
    }
}