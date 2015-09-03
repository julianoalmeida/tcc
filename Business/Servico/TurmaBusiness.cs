using Comum;
using Comum.Contratos.Turmas;
using Comum.Excecoes;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Servico
{
    public class TurmaBusiness : NegocioBase<Turma>, ITurmaBusiness
    {
        #region CONSTRUTOR
        public const int ERRO = 0;
        public const int SUCESSO = 1;
        public const int REGISTRO_DUPLICADO = 2;
        public const int CAMPO_OBRIGATORIO_NAO_INFORMADO = 3;
        public const int TOTAL_DISCENTES_MAIOR_QUANTIDADE_VAGAS = 4;
        private readonly ITurmaData _turmaData;

        public TurmaBusiness(ITurmaData repositorio)
            : base(repositorio)
        {
            _turmaData = repositorio;
        }

        #endregion

        public List<Turma> ListarTodos(Turma model, int? turno, int paginaAtual)
        {
            return _turmaData.ListarTodos(model, turno, paginaAtual);
        }

        public int TotalRegistros(Turma model)
        {
            return _turmaData.TotalRegistros(model);
        }

        private int verificarDuplicidade(Turma model)
        {
            var retorno = SUCESSO;

            var turma = _turmaData.Procurar(a => a.Descricao.ToLower().Equals(model.Descricao.ToLower())).FirstOrDefault();

            if (turma != null && turma.Id != model.Id)
            {
                retorno = REGISTRO_DUPLICADO;
            }

            return retorno;
        }

        public int ValidarRegrasNegocio(Turma model)
        {
            var retorno = SUCESSO;

            if (model.Discentes.Count > 20)
            {                
                throw new CapacidadeVagasUltrapassadaException("Vagas", new Exception());
            }
                        
            retorno = verificarDuplicidade(model);
            if (retorno != SUCESSO)
            {
                throw new RegistroDuplicadoException();
            }

            if (retorno == SUCESSO)
            {
                if (string.IsNullOrEmpty(model.Descricao))
                    retorno = CAMPO_OBRIGATORIO_NAO_INFORMADO;
                if (model.Turno == 0)
                    retorno = CAMPO_OBRIGATORIO_NAO_INFORMADO;
                if (model.Docente.Id == 0)
                    retorno = CAMPO_OBRIGATORIO_NAO_INFORMADO;
                if (!model.Discentes.Any())
                    retorno = CAMPO_OBRIGATORIO_NAO_INFORMADO;

                if (retorno != SUCESSO)
                {
                    throw new CampoObrigatorioException();
                }
            }

            return retorno;
        }
    }
}
