using Entidades;
using FluentNHibernate.Mapping;

namespace Data.Mapeamento
{
    public class UsuarioMap : ClassMap<Usuario>
    {
        public UsuarioMap()
        {
            Table("PERFIL_LOGIN");
            Id(a => a.Id, "Id_USU").Not.Nullable();
            References<Pessoa>(a => a.Pessoa, "ID_PESS_COD").Not.Nullable();
            Map(a => a.PerfilAcesso, "PERF_CD_PERFIL").Not.Nullable();
            Map(a => a.Login, "LOGIN_PERFIL").Not.Nullable();
            Map(a => a.Senha, "LOGIN_SENHA").Not.Nullable();
        }

    }
}
