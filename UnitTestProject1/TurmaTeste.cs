using Comum.Contratos.Turmas;
using Comum.Excecoes;
using Entidades;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Negocio.Servico;
using Testes.Base;

namespace Testes
{
    [TestClass]
    public class TurmaTeste : BaseTeste
    {

        #region SUCESSO

        [TestMethod]
        [ExpectedException(typeof(CampoObrigatorioException))]
        public void SalvarTurmaSemPreencherCamposObrigatorios()
        {
            var turmanegocio = configuradorTurma();

            TURMA_SUCESSO.Docente = DOCENTE_SUCESSO;
            TURMA_SUCESSO.Turno = 0;

            if (turmanegocio.ValidarRegrasNegocio(TURMA_SUCESSO) == 1)
            {
                turmanegocio.Salvar(TURMA_SUCESSO);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CapacidadeVagasUltrapassadaException))]
        public void SalvarTurmaTotalDiscentesMaiorQuantidadeVagasDisponiveis()
        {
            var turmanegocio = configuradorTurma();

            TURMA_SUCESSO.Docente = DOCENTE_SEM_CAMPOS_OBRIGATORIOS;

            for (int i = 0; i < 25; i++)
            {
                TURMA_SUCESSO.Discentes.Add(new Discente());
            }

            if (turmanegocio.ValidarRegrasNegocio(TURMA_SUCESSO) == 1)
            {
                turmanegocio.Salvar(TURMA_SUCESSO);
            }
        }



        #endregion

        #region ERRO


        #endregion

        #region CONFIGURADORES
        public ITurmaBusiness configuradorTurma()
        {
            var mock = new Mock<ITurmaData>();
            return new TurmaBusiness(mock.Object);
        }
        #endregion

    }
}
