using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Servico
{
    public class DocenteBusiness : NegocioBase<Docente>, IDocenteBusiness
    {
        #region INJEÇÃO

        private readonly IDocenteData _docenteData;
        public DocenteBusiness(IDocenteData data)
            : base(data)
        {
            _docenteData = data;
        }
        #endregion

        public List<Docente> ListarTodos(Docente docente, int paginaAtual)
        {
            return _docenteData.ListarTodos(docente, paginaAtual);
        }

        public int TotalRegistros(Docente docente)
        {
            return _docenteData.TotalRegistros(docente);
        }

        private bool VerificarPreenchimentoEscolaridade(Docente docente)
        {
            bool sucesso = true;
            if (docente.Escolaridade == 0)
            {
                sucesso = false;
                throw new CampoObrigatorioException();
            }
            return sucesso;
        }

        private bool VerificarPreenchimentoDisciplinas(Docente docente)
        {
            bool sucesso = true;
            if (!docente.Disciplinas.Any())
            {
                sucesso = false;
                throw new CampoObrigatorioException();
            }
            return sucesso;
        }

        public bool VerificarPreenchimentoCamposObrigatorios(Docente docente)
        {
            var retorno = VerificarPreenchimentoEscolaridade(docente);
            retorno &= VerificarPreenchimentoDisciplinas(docente);
            return retorno;

        }
    }
}
