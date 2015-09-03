using Comum;
using Entidades;
using NHibernate;
using System.Linq;

namespace Data.Repositorio
{
    public class UsuarioData : RepositorioNHibernate<Usuario>, IUsuarioData
    {
        public UsuarioData(ISession session)
            : base(session)
        {
        }


        public Usuario RecuperarUsuarioLogado(string Login, string Senha)
        {
            var usuario = Listar().Where(a => a.Login.Equals(Login))
                .Where(a => a.Senha.Equals(Senha)).FirstOrDefault();

            return usuario;
        }
    }
}
