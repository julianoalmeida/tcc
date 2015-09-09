using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("USUARIO");
            Id(a => a.Id, "Id").Not.Nullable();
            Map(a => a.Login, "Login").Not.Nullable();
            Map(a => a.Password, "Senha").Not.Nullable();
            Map(a => a.AccessCode, "IdPerfil").Not.Nullable();
        }
    }
}