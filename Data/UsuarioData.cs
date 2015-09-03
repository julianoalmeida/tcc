using System.Linq;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IUsuarioData : IRepositorio<User>
    {
        User GetLoggedUser(string login, string senha);
    }

    public class UsuarioData : RepositorioNHibernate<User>, IUsuarioData
    {
        public UsuarioData(ISession session)
            : base(session)
        { }
        
        public User GetLoggedUser(string login, string senha)
        {
            return GetAll()
                .Where(a => a.Login.Equals(login)).FirstOrDefault(a => a.Password.Equals(senha));
        }
    }
}
