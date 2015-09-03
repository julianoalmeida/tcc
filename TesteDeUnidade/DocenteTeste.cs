using Business;
using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace TesteDeUnidade
{
    [TestClass]
    public class DocenteTeste : BaseTeste
    {
        #region TESTES

        #region ERRO

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarDocenteSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var enderecoNegocio = configuradorEnderecoBusiness();
            var docenteNegocio = configuradorDocenteBusiness();
            var docenteDisciplina = configuradorDocenteDisciplinaBusiness();

            enderecoNegocio.Salvar(DOCENTE_SUCESSO.Pessoa.Endereco);
            pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
            docenteNegocio.Salvar(DOCENTE_SUCESSO);
            docenteDisciplina.Salvar(1, DISCIPLINAS_ERRO);
        }


        [TestMethod]
        [ExpectedException(typeof(DataAtualFuturaException))]
        public void SalvarDocenteComDataDeNascimentoAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.DataNascimento = DateTime.Now;
            pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(CpfException))]
        public void SalvarDocenteComCpfInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.Cpf = "11111111111";
            pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        }

        [TestMethod]
        [ExpectedException(typeof(EmailException))]
        public void SalvarDocenteComEmailInvalido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.Email = CAMPO_PREENCHIDO;
            pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        }

        //[TestMethod]
        //[ExpectedException(typeof(VincularDisciplinaException))]
        //public void SalvarDocenteSemVincularNenhumaDisciplina()
        //{
        //    var pessoaNegocio = configuradorPessoaBusiness();
        //    DOCENTE_SUCESSO.Pessoa.DataNascimento = DateTime.Now;
        //    pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
        //}

        #endregion

        #region SUCESSO

        [TestMethod]
        public void SalvarDocenteComCamposPreenchidos()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var enderecoNegocio = configuradorEnderecoBusiness();
            var docenteNegocio = configuradorDocenteBusiness();
            
            var retorno = enderecoNegocio.Salvar(DOCENTE_SUCESSO.Pessoa.Endereco);
            retorno += pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
            retorno += docenteNegocio.Salvar(DOCENTE_SUCESSO);

            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDocenteComCpfValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.Cpf = "16485884173";
            var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDocenteComDataDeNascimentoMenorQueAtual()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.DataNascimento = new DateTime(2000, 01, 01);
            var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        [TestMethod]
        public void SalvarDocenteEmailValido()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            DOCENTE_SUCESSO.Pessoa.Email = "marcosfreire@cast.com.br";

            var retorno = pessoaNegocio.Salvar(DOCENTE_SUCESSO.Pessoa);
            Assert.IsTrue(retorno == 0);
        }

        #endregion

        #endregion

        #region CONFIGURADOES

        public IDocenteBusiness configuradorDocenteBusiness()
        {
            var mock = new Mock<IDocenteData>();
            return new DocenteBusiness(mock.Object);
        }

        public IDocenteDisciplinaBusiness configuradorDocenteDisciplinaBusiness()
        {
            var mock = new Mock<IDocenteDisciplinaData>();
            return new DocenteDisciplinaBusiness(mock.Object);
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
