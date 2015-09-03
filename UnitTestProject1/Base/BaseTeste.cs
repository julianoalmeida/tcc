using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Base
{
    public class BaseTeste
    {
        #region CONSTANTES_SUCESSO

        public const string CAMPO_PREENCHIDO = "TEXTO";
        public const int ID = 1;
        public const int ERRO = 0;
        public const int SUCESSO = 1;


        protected static Pessoa PESSOA_SUCESSO()
        {
            return new Pessoa()
            {
                Celular = CAMPO_PREENCHIDO,
                Cpf = "16485884173",
                DataNascimento = new DateTime(2000, 01, 01),
                Email = "marcosteste@hotmail.com",
                EstadoCivil = ID,
                Sexo = ID,
                Nome = CAMPO_PREENCHIDO,
                Endereco = new Endereco()
                {
                    DescricaoBairro = CAMPO_PREENCHIDO,
                    Cep = CAMPO_PREENCHIDO,
                    Id = ID,
                    IdCidadeBrasil = ID,
                    NomeEndereco = CAMPO_PREENCHIDO,
                    CodigoUf = CAMPO_PREENCHIDO,
                }
            };
        }

        protected Administrador ADMINISTRADOR_SUCESSO = new Administrador()
        {
            Id = ID,
            Pessoa = PESSOA_SUCESSO()
        };

        protected Discente DISCENTE_SUCESSO = new Discente()
        {
            Id = ID,
            Escolaridade = ID,
            Pessoa = PESSOA_SUCESSO(),
        };

        protected Docente DOCENTE_SUCESSO = new Docente()
        {
            Id = ID,
            Escolaridade = ID,
            Disciplinas = DISCIPLINAS_SUCESSO(),
            Pessoa = PESSOA_SUCESSO(),
        };

        protected Turma TURMA_SUCESSO = new Turma()
        {
            Descricao = CAMPO_PREENCHIDO,
            Discentes = LISTA_DISCENTES_PREENCHIDA(),
            Turno = ID,
        };

        protected static List<Disciplina> DISCIPLINAS_SUCESSO()
        {
            List<Disciplina> retorno = new List<Disciplina>();

            for (int i = 0; i < 5; i++)
            {
                var disciplina = new Disciplina();
                retorno.Add(disciplina);
            }
            return retorno;
        }

        protected static List<Discente> LISTA_DISCENTES_PREENCHIDA()
        {
            List<Discente> retorno = new List<Discente>();

            for (int i = 0; i < 5; i++)
            {
                var discente = new Discente();
                retorno.Add(discente);
            }
            return retorno;
        }

        #endregion

        #region CONSTANTES_FALHA

        protected Turma TURMA_ERRO = new Turma()
        {
            Descricao = string.Empty,
            Discentes = new List<Discente>(),
            Turno = 0,
        };

        protected static List<Disciplina> DISCIPLINAS_ERRO()
        {
            List<Disciplina> retorno = new List<Disciplina>();
            return retorno;
        }

        protected Administrador ADMINISTRADOR_SEM_CAMPOS_OBRIGATORIOS = new Administrador()
        {
            Id = 0,
            Pessoa = new Pessoa(),
        };

        protected Discente DISCENTE_SEM_CAMPOS_OBRIGATORIOS = new Discente()
        {
            Id = 0,
            Escolaridade = 0,
            Pessoa = new Pessoa(),
        };

        protected Docente DOCENTE_SEM_CAMPOS_OBRIGATORIOS = new Docente()
        {
            Id = 0,
            Escolaridade = 0,
            Pessoa = new Pessoa(),
            Disciplinas = DISCIPLINAS_ERRO(),
        };

        #endregion

    }
}
