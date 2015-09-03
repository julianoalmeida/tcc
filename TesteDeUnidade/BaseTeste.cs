using Entidades;
using System;
using System.Collections.Generic;

namespace TesteDeUnidade
{
    public class BaseTeste
    {
        #region CONSTANTES_SUCESSO

        public const string CAMPO_PREENCHIDO = "TEXTO";
        public const int ID = 1;


        protected List<Disciplina> DISCIPLINAS_SUCESSO = new List<Disciplina> { new Disciplina { Descricao = CAMPO_PREENCHIDO, Id = ID } };


        protected Docente DOCENTE_SUCESSO = new Docente()
        {
            Ativo = true,
            Escolaridade = new Escolaridade()
            {
                Descricao = CAMPO_PREENCHIDO,
                Id = ID
            },
            Id = ID,
            IdEscolaridade = ID,
            IdPessoa = ID,
            Pessoa = new Pessoa()
            {
                Celular = CAMPO_PREENCHIDO,
                Cpf = "46865421035",
                DataNascimento = new DateTime(2000, 01, 01),
                Email = "marcosteste@hotmail.com",
                EstadoCivil = ID,
                Sexo = ID,
                Nome = CAMPO_PREENCHIDO,
                Endereco = new Endereco()
                {
                    Bairro = CAMPO_PREENCHIDO,
                    CEP = CAMPO_PREENCHIDO,
                    Complemento = CAMPO_PREENCHIDO,
                    Id = ID,
                    IdCidade = ID,
                    Logradouro = CAMPO_PREENCHIDO,
                    Numero = CAMPO_PREENCHIDO,
                    Cidade = new Cidade()
                    {
                        Descricao = CAMPO_PREENCHIDO,
                        Id = ID,
                        IdEstado = "AA",
                        Estado = new Estado()
                        {
                            Descricao = CAMPO_PREENCHIDO,
                            Sigla = "AA",
                        }
                    }
                }
            }
        };


        protected Administrador ADMINISTRADOR_SUCESSO = new Administrador
        {
            Ativo = true,
            Id = ID,
            IdPessoa = ID,
            Pessoa = new Pessoa()
            {
                Celular = CAMPO_PREENCHIDO,
                Cpf = "46865421035",
                DataNascimento = new DateTime(2000, 01, 01),
                Email = "marcosteste@hotmail.com",
                EstadoCivil = ID,
                Sexo = ID,
                Nome = CAMPO_PREENCHIDO,
                Endereco = new Endereco()
                {
                    Bairro = CAMPO_PREENCHIDO,
                    CEP = CAMPO_PREENCHIDO,
                    Complemento = CAMPO_PREENCHIDO,
                    Id = ID,
                    IdCidade = ID,
                    Logradouro = CAMPO_PREENCHIDO,
                    Numero = CAMPO_PREENCHIDO,
                    Cidade = new Cidade()
                    {
                        Descricao = CAMPO_PREENCHIDO,
                        Id = ID,
                        IdEstado = "AA",
                        Estado = new Estado()
                        {
                            Descricao = CAMPO_PREENCHIDO,
                            Sigla = "AA",
                        }
                    }
                }
            }
        };






        protected Discente DISCENTE_SUCESSO = new Discente()
        {
            Escolaridade = new Escolaridade()
            {
                Descricao = CAMPO_PREENCHIDO,
                Id = ID
            },
            Id = ID,
            IdEscolaridade = ID,
            IdPessoa = ID,
            Pessoa = new Pessoa()
            {
                Celular = CAMPO_PREENCHIDO,
                Cpf = "46865421035",
                DataNascimento = new DateTime(2000, 01, 01),
                Email = "marcosteste@hotmail.com",
                EstadoCivil = ID,
                Sexo = ID,
                Nome = CAMPO_PREENCHIDO,
                Endereco = new Endereco()
                {
                    Bairro = CAMPO_PREENCHIDO,
                    CEP = CAMPO_PREENCHIDO,
                    Complemento = CAMPO_PREENCHIDO,
                    Id = ID,
                    IdCidade = ID,
                    Logradouro = CAMPO_PREENCHIDO,
                    Numero = CAMPO_PREENCHIDO,
                    Cidade = new Cidade()
                    {
                        Descricao = CAMPO_PREENCHIDO,
                        Id = ID,
                        IdEstado = "AA",
                        Estado = new Estado()
                        {
                            Descricao = CAMPO_PREENCHIDO,
                            Sigla = "AA",
                        }
                    }
                }
            }
        };

        #endregion

        #region CONSTANTES_FALHA

        protected List<Disciplina> DISCIPLINAS_ERRO = new List<Disciplina>();

        protected Administrador ADMINISTRADOR_SEM_CAMPOS_OBRIGATORIOS = new Administrador
        {
            Pessoa = new Pessoa
            {
                Endereco = new Endereco
                {
                    Cidade = new Cidade
                    {
                        Estado = new Estado()
                    }
                }
            }
        };

        protected Docente DOCENTE_SEM_CAMPOS_OBRIGATORIOS = new Docente()
        {
            Pessoa = new Pessoa()
            {
                Endereco = new Endereco()
                {
                    Cidade = new Cidade()
                    {
                        Estado = new Estado()
                    }
                },
                DataNascimento = DateTime.MinValue,
            },
            Escolaridade = new Escolaridade(),
        };

        protected Discente DISCENTE_SEM_CAMPOS_OBRIGATORIOS = new Discente()
        {
            Pessoa = new Pessoa()
            {
                Endereco = new Endereco()
                {
                    Cidade = new Cidade()
                    {
                        Estado = new Estado()
                    }
                },
                DataNascimento = DateTime.MinValue,
            },
            Escolaridade = new Escolaridade(),
        };

        #endregion

    }
}
