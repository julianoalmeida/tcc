using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Negocio.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Base;

namespace Testes
{
    [TestClass]
    public class DocenteTeste : BaseTeste
    {


        #region ERRO

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarDocenteSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var docenteNegocio = configurarDocenteBusiness();

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                DOCENTE_SUCESSO.Disciplinas.Clear();

                if (docenteNegocio.VerificarPreenchimentoCamposObrigatorios(DOCENTE_SUCESSO))
                {
                    pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarDocenteComDataDeNascimentoAtual()
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
        public void SalvarDocenteComCpfInvalido()
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
        public void SalvarDocenteComEmailInvalido()
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
        public void SalvarDocenteComCamposPreenchidos()
        {
            var sucesso = true;

            var pessoaNegocio = configuradorPessoaBusiness();
            var docente = configurarDocenteBusiness();

            if (pessoaNegocio.validarPessoa(DISCENTE_SUCESSO.Pessoa))
            {
                if (docente.VerificarPreenchimentoCamposObrigatorios(DOCENTE_SUCESSO))
                {
                    pessoaNegocio.Salvar(DISCENTE_SUCESSO.Pessoa);
                }
            }

            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SalvarDocenteComCpfValido()
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
        public void SalvarDocenteComDataDeNascimentoMenorQueAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DISCENTE_SUCESSO.Pessoa.DataNascimento = new DateTime(2014, 09, 15);
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
        public void SalvarDocenteEmailValido()
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

        public IDocenteBusiness configurarDocenteBusiness()
        {
            var mock = new Mock<IDocenteData>();
            return new DocenteBusiness(mock.Object);
        }

        #endregion

    }
}
