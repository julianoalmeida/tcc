using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Negocio.Servico;
using System;
using Testes.Base;

namespace Testes
{
    [TestClass]
    public class DiscenteTeste : BaseTeste
    {
        #region ERRO

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarDiscenteSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var discenteNegocio = configuradorDiscenteBusiness();

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                if (discenteNegocio.VerificarPreenchimentoEscolaridade(DISCENTE_SEM_CAMPOS_OBRIGATORIOS))
                {
                    pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarDiscenteComDataDeNascimentoAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.DataNascimento = DateTime.Now;
            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CpfException))]
        public void SalvarDiscenteComCpfInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Cpf = "11111111111";
            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(EmailException))]
        public void SalvarDiscenteComEmailInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Email = CAMPO_PREENCHIDO;
            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            }
        }

        #endregion

        #region SUCESSO

        [TestMethod]
        public void SalvarDiscenteComCamposPreenchidos()
        {
            var sucesso = true;

            var pessoaNegocio = configuradorPessoaBusiness();
            var discenteNegocio = configuradorDiscenteBusiness();

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                if (discenteNegocio.VerificarPreenchimentoEscolaridade(DISCENTE_SUCESSO))
                {
                    pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
                }
            }

            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarDiscenteComCpfValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Cpf = "16485884173";
            var sucesso = true;

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarDiscenteComDataDeNascimentoMenorQueAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.DataNascimento = new DateTime(2014, 09, 15);//01/01/2000
            var sucesso = true;

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
            }
            else
            {
                sucesso = false;
            }
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarDiscenteEmailValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.Email = "marcosfreire@cast.com.br";

            var sucesso = true;
            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
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

        public IDiscenteBusiness configuradorDiscenteBusiness()
        {
            var mock = new Mock<IDiscenteData>();
            return new DiscenteBusiness(mock.Object);
        }

        #endregion

    }
}
