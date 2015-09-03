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
        private readonly IUsuarioData _usuarioData;
        public UsuarioBusiness(IUsuarioData data)
            : base(data)
        {
            _usuarioData = data;
        }

        public User GetLoggedUser(string login, string senha)
        {
            return _usuarioData.GetLoggedUser(login, senha);
        }
    }
}