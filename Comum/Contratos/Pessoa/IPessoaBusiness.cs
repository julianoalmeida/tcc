using Entidades;

namespace Comum.Contratos
{
    public interface IPessoaBusiness : INegocioBase<Pessoa>
    {
        bool verificarDuplicidade(Pessoa pessoa);

        bool CamposObrogatorioNaoPreenchidos(Pessoa pessoa);

        bool validarPessoa(Pessoa pessoa);
    }
}
