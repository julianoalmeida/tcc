using System.Linq;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IUserData : INHibernateRepository<User>
    {
        User GetByCredentials(string login, string senha);
    }

    public class UserData : NHibernateRepository<User>, IUserData
    {
        public UserData(ISession session)
            : base(session)
        { }
        
        public User GetByCredentials(string login, string senha)
        {   
            return GetAll()
                .Where(a => a.Login.Equals(login)).FirstOrDefault(a => a.Password.Equals(senha));
        }
    }
}
