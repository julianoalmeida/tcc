using Entidades;

namespace Comum
{
    public interface IUsuarioBusiness : INegocioBase<Usuario>
    {
        Usuario RecuperarUsuarioLogado(string Login, string Senha);
    }
}
