using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Negocio.Servico;
using System;
using Testes.Base;

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

            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
                pessoaNegocio.SaveAndReturn(ADMINISTRADOR_SUCESSO.Pessoa);
            
        }

        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarAdministradorComDataDeNascimentoAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.DataNascimento = DateTime.Now;

            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
                pessoaNegocio.SaveAndReturn(ADMINISTRADOR_SUCESSO.Pessoa);
            
        }

        [TestMethod]
        [ExpectedException(typeof(CpfException))]
        public void SalvarAdministradorComCpfInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Cpf = "11111111111";

            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
                pessoaNegocio.SaveAndReturn(ADMINISTRADOR_SUCESSO.Pessoa);
            
        }

        [TestMethod]
        [ExpectedException(typeof(EmailException))]
        public void SalvarAdministradorComEmailInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Email = CAMPO_PREENCHIDO;
            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
            }
        }

        #endregion

        #region SUCESSO

        [TestMethod]
        public void SalvarAdministradorComCamposPreenchidos()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var sucesso = true;

            // ADMINISTRADOR_SUCESSO.Pessoa.Nome = string.Empty;
            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }

            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarAdministradorComCpfValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Cpf = "16485884173";
            var sucesso = true;

            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarAdministradorComDataDeNascimentoMenorQueAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.DataNascimento = new DateTime(2014, 09, 15);//01/01/2000
            var sucesso = true;

            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarAdministradorEmailValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            ADMINISTRADOR_SUCESSO.Pessoa.Email = "marcosfreire@cast.com.br";

            var sucesso = true;
            if (pessoaNegocio.validarPessoa(ADMINISTRADOR_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }
            Assert.IsTrue(sucesso);
        }

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
