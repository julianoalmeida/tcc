using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class AdministradorMap : ClassMap<Administrador>
    {

        public AdministradorMap()
        {
            Table("ADMINISTRADOR");
            Id(a => a.Id, "ADMI_CD_ADMINISTRADOR");
            References<Pessoa>(a => a.Pessoa, "PESS_CD_PESSOA").Not.Nullable().Cascade.All();           
        }

    }
}
