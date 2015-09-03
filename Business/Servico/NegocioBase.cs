using Comum;
using Comum.Contratos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocio.Servico
{
    public abstract class NegocioBase<TEntidade> : INegocioBase<TEntidade>
         where TEntidade : Entidade
    {
        #region VALIDACOES

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


        #endregion

        public NegocioBase(IRepositorio<TEntidade> repositorio)
        {
            Repositorio = repositorio;
        }

        protected readonly IRepositorio<TEntidade> Repositorio;
        protected List<string> mensagensValidacao = new List<string>();
        //JavaScriptSerializer jss = new JavaScriptSerializer();

        public TEntidade RemoverProxy(TEntidade model)
        {
            return Repositorio.RemoverProxy(model);
        }

        public virtual TEntidade ObterPorId(int id)
        {
            return Repositorio.ObterPorId(id);
        }

        public virtual IEnumerable<TEntidade> Listar()
        {
            return Repositorio.Listar();
        }

        public virtual IEnumerable<TEntidade> Listar(params Expression<Func<TEntidade, object>>[] incluirPropriedades)
        {
            return Repositorio.Listar(incluirPropriedades);
        }

        public virtual void Excluir(int id)
        {
            var entidade = Repositorio.ObterPorId(id);
            Repositorio.Apagar(entidade);
        }

        public virtual void Salvar(TEntidade entidade)
        {
            Repositorio.Salvar(entidade);
        }

        public virtual int SalvarComRetorno(TEntidade entidade)
        {
            return Repositorio.SalvarComRetorno(entidade);
        }

        public IEnumerable<TEntidade> Pesquisar(System.Linq.Expressions.Expression<System.Func<TEntidade, bool>> where, int limit)
        {
            return Repositorio.Procurar(where, limit);
        }

        public IEnumerable<TEntidade> Pesquisar(System.Linq.Expressions.Expression<System.Func<TEntidade, bool>> where, int limit, int offset)
        {
            return Repositorio.Procurar(where, limit, offset);
        }

        public IEnumerable<TEntidade> Pesquisar(System.Linq.Expressions.Expression<System.Func<TEntidade, bool>> where)
        {
            return Repositorio.Procurar(where);
        }

        public string GerarMD5(string valor = null)
        {
            if (valor == null) return null;
            MD5 md5Hasher = MD5.Create();

            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(valor));

            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
