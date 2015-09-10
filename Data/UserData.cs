using System.Linq;
using Data.BaseRepositories;
using Entidades;
using NHibernate;

namespace Data
{
    public interface IUserData : IBaseRepositoryRepository<User>
    {
        User GetByCredentials(string login, string senha);
    }

    public class UserData : BaseRepositoryRepository<User>, IUserData
    {
        public UserData(ISession session)
            : base(session)
        { }
        
        public User GetByCredentials(string login, string senha)
        {   
            return GetAll().Values.Where(a => a.Login.Equals(login)).FirstOrDefault(a => a.Password.Equals(senha));
        }
    }
}
