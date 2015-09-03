using Business;
using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace TesteDeUnidade
{
    [TestClass]
    public class DiscenteTeste : BaseTeste
    {
        #region TESTES

        #region EXCEPTION

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarDiscenteSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var enderecoNegocio = configuradorEnderecoBusiness();
            var discenteNegocio = configuradorDiscenteBusiness();

            enderecoNegocio.Salvar(DISCENTE_SUCESSO.Pessoa.Endereco);
            pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            DISCENTE_SUCESSO.IdEscolaridade = 0;
            discenteNegocio.Salvar(DISCENTE_SUCESSO);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarDiscenteComDataDeNascimentoAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.DataNascimento = DateTime.Now;
            pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(CpfException))]
        public void SalvarDiscenteComCpfInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Cpf = "11111111111";
            pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailException))]
        public void SalvarDiscenteComEmailInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Email = CAMPO_PREENCHIDO;
            pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
        }


        #endregion

        #region SUCESSO

        [TestMethod]
        public void SalvarDiscenteComCamposPreenchidos()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var enderecoNegocio = configuradorEnderecoBusiness();
            var docenteNegocio = configuradorDiscenteBusiness();

            var retorno = enderecoNegocio.Salvar(DISCENTE_SUCESSO.Pessoa.Endereco);
            retorno += pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            retorno += docenteNegocio.Salvar(DISCENTE_SUCESSO);

            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDiscenteComCpfValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Cpf = "16485884173";
            var retorno = pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDiscenteComDataDeNascimentoMenorQueAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.DataNascimento = new DateTime(2000, 01, 01);
            var retorno = pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDiscenteEmailValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Email = "marcosfreire@cast.com.br";

            var retorno = pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        #endregion

        #endregion

        #region CONFIGURADOES


        public IDiscenteBusiness configuradorDiscenteBusiness()
        {
            var mock = new Mock<IDiscenteData>();
            return new DiscenteBusiness(mock.Object);
        }

        public IPessoaBusiness configuradorPessoaBusiness()
        {
            var mock = new Mock<IPessoaData>();
            return new PessoaBusiness(mock.Object);
        }

        public IEnderecoBusiness configuradorEnderecoBusiness()
        {
            var mock = new Mock<IEnderecoData>();
            return new EnderecoBusiness(mock.Object);
        }

        #endregion
    }
}
