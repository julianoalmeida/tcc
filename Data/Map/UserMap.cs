using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("PERFIL_LOGIN");
            Id(a => a.Id, "Id_USU").Not.Nullable();
            References(a => a.Person, "IdPessoa").Not.Nullable();
            Map(a => a.AccessCode, "IdPerfil").Not.Nullable();
            Map(a => a.Login, "Login").Not.Nullable();
            Map(a => a.Password, "Senha").Not.Nullable();
        }
    }
}