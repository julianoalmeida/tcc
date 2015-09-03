using Business.Servico;
using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Testes
{
    [TestClass]
    public class AdministradorTeste : BaseTeste
    {
        #region ERRO

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarAdministradorSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();

            ADMINISTRADOR_SUCESSO.Pessoa.Nome = string.Empty;
            pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);

        }

        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarAdministradorComDataDeNascimentoAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.DataNascimento = DateTime.Now;
            pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(CpfException))]
        public void SalvarAdministradorComCpfInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Cpf = "11111111111";
            pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailException))]
        public void SalvarAdministradorComEmailInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Email = CAMPO_PREENCHIDO;
            pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
        }

        #endregion

        #region SUCESSO

        //[TestMethod]
        //public void SalvarDocenteComCamposPreenchidos()
        //{
        //    var pessoaNegocio = configuradorPessoaBusiness();
        //    var enderecoNegocio = configuradorEnderecoBusiness();
        //    var docenteNegocio = configuradorDocenteBusiness();

        //    var retorno = enderecoNegocio.Salvar(DOCENTE_SUCESSO.Pessoa.Endereco);
        //    retorno += pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        //    retorno += docenteNegocio.Salvar(DOCENTE_SUCESSO);

        //    Assert.IsTrue(retorno == 0);
        //}

        //[TestMethod]
        //public void SalvarDocenteComCpfValido()
        //{
        //    var pessoaNegocio = configuradorPessoaBusiness();
        //    DOCENTE_SUCESSO.Pessoa.Cpf = "16485884173";
        //    var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        //    Assert.IsTrue(retorno == 0);
        //}

        //[TestMethod]
        //public void SalvarDocenteComDataDeNascimentoMenorQueAtual()
        //{
        //    var pessoaNegocio = configuradorPessoaBusiness();
        //    DOCENTE_SUCESSO.Pessoa.DataNascimento = new DateTime(2000, 01, 01);
        //    var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        //    Assert.IsTrue(retorno == 0);
        //}

        //[TestMethod]
        //public void SalvarDocenteEmailValido()
        //{
        //    var pessoaNegocio = configuradorPessoaBusiness();
        //    DOCENTE_SUCESSO.Pessoa.Email = "marcosfreire@cast.com.br";

        //    var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        //    Assert.IsTrue(retorno == 0);
        //}

        #endregion

        #region CONFIGURADOES

        public IPessoaBusiness configuradorPessoaBusiness()
        {
            var mock = new Mock<IPessoaData>();
            return new PessoaBusiness(mock.Object);
        }

        #endregion

    }
}

