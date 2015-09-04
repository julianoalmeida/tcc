using Data;
using Entidades;

namespace Negocio
{
    public interface IUserBusiness : INegocioBase<User>
    {
        User GetLoggedUser(string login, string password);
    }

    public class UserBusiness : BaseBusiness<User>, IUserBusiness
    {
        private readonly IUserData _userData;
        public UserBusiness(IUserData data)
            : base(data)
        {
            _userData = data;
        }

        public User GetLoggedUser(string login, string password)
        {
            return _userData.GetLoggedUser(login, password);
        }
    }
}