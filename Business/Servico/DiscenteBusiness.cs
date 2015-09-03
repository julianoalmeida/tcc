using Comum.Contratos;
using Comum.Excecoes;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Servico
{
    public class DiscenteBusiness : NegocioBase<Discente>, IDiscenteBusiness
    {
        #region INJECAO

        private readonly IDiscenteData _discenteData;
        public DiscenteBusiness(IDiscenteData discenteData)
            : base(discenteData)
        {
            _discenteData = discenteData;
        }

        #endregion

        #region ACOES

        #endregion

        #region CONSULTA

        public List<Discente> ListarTodos(Discente docente, int paginaAtual)
        {
            return _discenteData.ListarTodos(docente, paginaAtual);
        }

        public int TotalRegistros(Discente docente)
        {
            return _discenteData.TotalRegistros(docente);
        }

        public string GerarNumeroMatricula(Discente discente)
        {
            var maxId = 1;
            var matricula = string.Empty;

            var ultimo = _discenteData.Listar().ToList().LastOrDefault();

            if (ultimo != null)
            {
                maxId = ultimo.Id + 1;
                if (ultimo.Id == discente.Id)
                {
                    matricula = ultimo.Matricula;
                }
                else
                {
                    matricula = string.Format("{0}{1}{2}", discente.Pessoa.Nome, "UNIP", maxId);
                }
            }
            else
            {
                matricula = string.Format("{0}{1}{2}", discente.Pessoa.Nome, "UNIP", maxId);
            }

            return matricula;
        }


        #endregion


        public bool VerificarPreenchimentoEscolaridade(Discente discente)
        {
            bool sucesso = true;
            if (discente.Escolaridade == 0)
            {
                sucesso = false;
                throw new CampoObrigatorioException();
            }
            return sucesso;
        }
    }
}
