using Comum.Contratos;
using Entidades;

namespace Comum
{
    public interface IUsuarioData : IRepositorio<Usuario>
    {
        Usuario RecuperarUsuarioLogado(string Login, string Senha);
    }
}
