using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes
{
    public class BaseTeste
    {
        #region CONSTANTES_SUCESSO

        public const string CAMPO_PREENCHIDO = "TEXTO";
        public const int ID = 1;

        protected Administrador ADMINISTRADOR_SUCESSO = new Administrador()
        {
            Id = ID,
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
                    DescricaoBairro = CAMPO_PREENCHIDO,
                    Cep = CAMPO_PREENCHIDO,
                    Id = ID,
                    IdCidadeBrasil = ID,
                    NomeEndereco = CAMPO_PREENCHIDO,
                    CodigoUf = CAMPO_PREENCHIDO,
                }
            }
        };

        #endregion

        #region CONSTANTES_FALHA

        protected Administrador ADMINISTRADOR_SEM_CAMPOS_OBRIGATORIOS = new Administrador()
        {
            Id = ID,
            Pessoa = new Pessoa(),
        };


        #endregion
    }
}
