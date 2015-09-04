using Data;
using Entidades;

namespace Negocio
{
    public interface IUsuarioBusiness : INegocioBase<User>
    {
        User GetLoggedUser(string login, string senha);
    }

    public class UsuarioBusiness : BaseBusiness<User>, IUsuarioBusiness
    {
        private readonly IUserData _userData;
        public UsuarioBusiness(IUserData data)
            : base(data)
        {
            _userData = data;
        }

        public User GetLoggedUser(string login, string senha)
        {
            return _userData.GetLoggedUser(login, senha);
        }
    }
}