using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using Entidades.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Servico
{
    public class PessoaBusiness : NegocioBase<Pessoa>, IPessoaBusiness
    {
        #region INJEÇÃO
        private readonly IPessoaData _pessoaData;
        public PessoaBusiness(IPessoaData data)
            : base(data)
        {
            _pessoaData = data;
        }
        #endregion
                

        public bool verificarDuplicidade(Pessoa pessoa)
        {
            return _pessoaData.verificarDuplicidade(pessoa);
        }

        #region METODOS_AUXILIARES

        private bool ValidaDataAtualFutura(DateTime data)
        {
            if (data >= DateTime.Today)
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        public bool CamposObrogatorioNaoPreenchidos(Pessoa pessoa)
        {
            var camposPreenchidos = false;

            if (string.IsNullOrEmpty(pessoa.Nome))
                camposPreenchidos = true;
            else if (!pessoa.DataNascimento.HasValue)
                camposPreenchidos = true;
            else if (pessoa.Sexo == 0)
                camposPreenchidos = true;
            else if (pessoa.EstadoCivil == 0)
                camposPreenchidos = true;
            else if (string.IsNullOrEmpty(pessoa.Celular.RemoveCaracteresMascara()))
                camposPreenchidos = true;
            else if (string.IsNullOrEmpty(pessoa.Endereco.CodigoUf))
                camposPreenchidos = true;
            else if (pessoa.Endereco.IdCidadeBrasil == 0)
                camposPreenchidos = true;
            else if (string.IsNullOrEmpty(pessoa.Endereco.NomeEndereco))
                camposPreenchidos = true;
            else if (string.IsNullOrEmpty(pessoa.Endereco.DescricaoBairro))
                camposPreenchidos = true;
            else if (string.IsNullOrEmpty(pessoa.Endereco.Cep))
                camposPreenchidos = true;

            return camposPreenchidos;
        }

        public bool validarPessoa(Pessoa pessoa)
        {
            bool sucesso = true;

            var pessoaBD = _pessoaData.Procurar(a => a.Cpf.Equals(pessoa.Cpf)).FirstOrDefault();

            //duplicidade por Cpf
            if (pessoaBD != null && pessoaBD.Id != pessoa.Id)
            {
                sucesso = false;
                throw new RegistroDuplicadoException();
            }
            else if (CamposObrogatorioNaoPreenchidos(pessoa))
            {
                sucesso = false;
                throw new CampoObrigatorioException();
            }
            else if (ValidaDataAtualFutura(pessoa.DataNascimento.Value))
            {
                sucesso = false;
                throw new DataAtualFuturaException();
            }
            else if (!CpfValido(pessoa.Cpf))
            {
                sucesso = false;
                throw new CpfException();
            }
            else if (!EmailValido(pessoa.Email))
            {
                sucesso = false;
                throw new EmailException();
            }
            return sucesso;
        }
    }
}
