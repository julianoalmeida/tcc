using Comum;
using Entidades;

namespace Negocio.Servico
{
    public class UsuarioBusiness : NegocioBase<Usuario>, IUsuarioBusiness
    {
        #region INJEÇÃO
        private readonly IUsuarioData _usuarioData;
        public UsuarioBusiness(IUsuarioData data)
            : base(data)
        {
            _usuarioData = data;
        }
        #endregion


        public Usuario RecuperarUsuarioLogado(string Login, string Senha)
        {
            return _usuarioData.RecuperarUsuarioLogado(Login, Senha);
        }
    }
}
