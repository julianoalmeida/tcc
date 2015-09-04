using System.Linq;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IUserData : INHibernateRepository<User>
    {
        User GetLoggedUser(string login, string senha);
    }

    public class UserData : NHibernateRepository<User>, IUserData
    {
        public UserData(ISession session)
            : base(session)
        { }
        
        public User GetLoggedUser(string login, string senha)
        {
            return GetAll()
                .Where(a => a.Login.Equals(login)).FirstOrDefault(a => a.Password.Equals(senha));
        }
    }
}
