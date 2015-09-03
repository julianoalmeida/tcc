using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class UsuarioMap : ClassMap<User>
    {
        public UsuarioMap()
        {
            Table("PERFIL_LOGIN");
            Id(a => a.Id, "Id_USU").Not.Nullable();
            References<Person>(a => a.Person, "ID_PESS_COD").Not.Nullable();
            Map(a => a.AccessCode, "PERF_CD_PERFIL").Not.Nullable();
            Map(a => a.Login, "LOGIN_PERFIL").Not.Nullable();
            Map(a => a.Password, "LOGIN_SENHA").Not.Nullable();
        }

    }
}
