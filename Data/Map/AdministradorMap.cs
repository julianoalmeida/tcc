using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class AdministradorMap : ClassMap<Administrator>
    {
        public AdministradorMap()
        {
            Table("ADMINISTRADOR");
            Id(a => a.Id, "ADMI_CD_ADMINISTRADOR");
            References<Person>(a => a.Person, "PESS_CD_PESSOA").Not.Nullable().Cascade.All();
        }
    }
}