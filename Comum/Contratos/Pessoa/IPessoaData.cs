using Entidades;

namespace Comum.Contratos
{
    public interface IPessoaData : IRepositorio<Pessoa>
    {
        bool verificarDuplicidade(Pessoa pessoa);
    }
}
