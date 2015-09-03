using Entidades;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Negocio.Servico
{
    public class BaseBusiness
    {
        /// <summary>
        /// Verifica se o email informado é valido.
        /// </summary>
        /// <param name="email">Email a ser validado</param>
        /// <returns>Retorna se o email é valido.</returns>
        protected bool EmailValido(string email)
        {
            if (String.IsNullOrEmpty(email))
                return true;
            return new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$").IsMatch(email);
        }

        /// <summary>
        /// Verifica se um CPF é válido
        /// </summary>
        /// <param name="cpf">CPF</param>
        /// <returns>Retorna true se for válido e false se for inválido</returns>
        protected bool CpfValido(string cpf)
        {
            //Retorna falso quando for campo vazio , ou algum dos CPF INVALIDOS já Conhecidos
            if (string.IsNullOrEmpty(cpf) ||
                  cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" ||
                  cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" ||
                  cpf == "88888888888" || cpf == "99999999999")
            {
                return false;
            }


            Regex regEx = new Regex(@"^\d{11}$", RegexOptions.Singleline);
            if (!regEx.IsMatch(cpf))
            {
                return false;
            }
            else
            {
                string fmtCpf = cpf;
                if (fmtCpf.Equals("00000000000"))
                {
                    return false;
                }
                int total = 0;
                int digitoVerificador = 0;
                total += (int.Parse(fmtCpf.Substring(0, 1)) * 10);
                total += (int.Parse(fmtCpf.Substring(1, 1)) * 9);
                total += (int.Parse(fmtCpf.Substring(2, 1)) * 8);
                total += (int.Parse(fmtCpf.Substring(3, 1)) * 7);
                total += (int.Parse(fmtCpf.Substring(4, 1)) * 6);
                total += (int.Parse(fmtCpf.Substring(5, 1)) * 5);
                total += (int.Parse(fmtCpf.Substring(6, 1)) * 4);
                total += (int.Parse(fmtCpf.Substring(7, 1)) * 3);
                total += (int.Parse(fmtCpf.Substring(8, 1)) * 2);
                digitoVerificador = total % 11;
                if (digitoVerificador < 2)
                {
                    digitoVerificador = 0;
                }
                else
                {
                    digitoVerificador = 11 - digitoVerificador;
                }

                if (int.Parse(fmtCpf.Substring(9, 1)) != digitoVerificador)
                {
                    return false;
                }
                else
                {
                    total = 0;
                    digitoVerificador = 0;
                    total += (int.Parse(fmtCpf.Substring(0, 1)) * 11);
                    total += (int.Parse(fmtCpf.Substring(1, 1)) * 10);
                    total += (int.Parse(fmtCpf.Substring(2, 1)) * 9);
                    total += (int.Parse(fmtCpf.Substring(3, 1)) * 8);
                    total += (int.Parse(fmtCpf.Substring(4, 1)) * 7);
                    total += (int.Parse(fmtCpf.Substring(5, 1)) * 6);
                    total += (int.Parse(fmtCpf.Substring(6, 1)) * 5);
                    total += (int.Parse(fmtCpf.Substring(7, 1)) * 4);
                    total += (int.Parse(fmtCpf.Substring(8, 1)) * 3);
                    total += (int.Parse(fmtCpf.Substring(9, 1)) * 2);
                    digitoVerificador = total % 11;
                    if (digitoVerificador < 2)
                    {
                        digitoVerificador = 0;
                    }
                    else
                    {
                        digitoVerificador = 11 - digitoVerificador;
                    }
                    if (int.Parse(fmtCpf.Substring(10, 1)) != digitoVerificador)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        protected List<string> ValidaPreenchimentoEndereco(Endereco endereco)
        {
            List<string> camposNaoPreenchidos = new List<string>();

            //if (String.IsNullOrEmpty(endereco.CEP))
            //    camposNaoPreenchidos.Add("Cep");
            //if (String.IsNullOrEmpty(endereco.Logradouro))
            //    camposNaoPreenchidos.Add("Logradouro");
            //if (String.IsNullOrEmpty(endereco.Numero))
            //    camposNaoPreenchidos.Add("Número");
            //if (endereco.IdCidade == 0)
            //    camposNaoPreenchidos.Add("Cidade");
            //if (String.IsNullOrEmpty(endereco.Bairro))
            //    camposNaoPreenchidos.Add("Bairro");
            //if (String.IsNullOrEmpty(endereco.Cidade.IdEstado) || endereco.Cidade.IdEstado.Equals("0"))
            //    camposNaoPreenchidos.Add("Estado");

            return camposNaoPreenchidos;
                
        }

        protected List<string> ValidaPreenchimentoPessoa(Pessoa pessoa)
        {
            List<string> camposNaoPreenchidos = new List<string>();
            if (String.IsNullOrEmpty(pessoa.Celular))
                camposNaoPreenchidos.Add("Celular");
            if (String.IsNullOrEmpty(pessoa.Cpf))
                camposNaoPreenchidos.Add("CPF");
            if (pessoa.DataNascimento == null)
                camposNaoPreenchidos.Add("Data de Nascimento");
            if (string.IsNullOrEmpty(pessoa.Email))
                camposNaoPreenchidos.Add("Email");
            if (pessoa.EstadoCivil == 0)
                camposNaoPreenchidos.Add("Estado Civil");
            if (String.IsNullOrEmpty(pessoa.Nome))
                camposNaoPreenchidos.Add("Nome");
            if (pessoa.Sexo == 0)
                camposNaoPreenchidos.Add("Sexo");
            
            return camposNaoPreenchidos;
        }

    }
}
