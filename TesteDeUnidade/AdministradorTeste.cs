using Business;
using Comum.Contratos;
using Comum.Excecoes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDeUnidade
{
    [TestClass]
    public class AdministradorTeste : BaseTeste
    {
        #region TESTES

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarAdministradorSemPreencherCamposObrigatorios()
        {
            var pessoaNegocio = configuradorPessoaBusiness();
            var enderecoNegocio = configuradorEnderecoBusiness();
            var administradorNegocio = configuradorAdministradorBusiness();

            enderecoNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa.Endereco);
            administradorNegocio.Salvar(ADMINISTRADOR_SUCESSO);
            ADMINISTRADOR_SUCESSO.Pessoa.Nome = string.Empty;
            pessoaNegocio.Salvar(ADMINISTRADOR_SUCESSO.Pessoa);
        }


        #endregion

        #region CONFIGURADORES

        public IAdministradorBusiness configuradorAdministradorBusiness()
        {
            var mock = new Mock<IAdministradorData>();
            return new AdministradorBusiness(mock.Object);
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
