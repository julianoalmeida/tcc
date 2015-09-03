using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Entidades.Extensions
{
    public static class StringExtension
    {

        /// <summary>
        /// Extensão de string que remove todos caracteres de máscara  { ./-,() }
        /// </summary>
        /// <param name="value">String que será modificada.</param>
        /// <returns>String com os caracteres de máscara removidos.</returns>
        public static string RemoveCaracteresMascara(this string value)
        {
            string texto = string.Empty;
            if (value != null)
            {
                texto = value.ToString();
                texto = texto.Replace(".", String.Empty).Replace("/", String.Empty).Replace("-", String.Empty)
                             .Replace(",", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty)
                             .Replace(" ", String.Empty).Trim();
            }
            return texto;
        }

        /// <summary>
        /// Extensão de string que remove todos os acentos possíveis de uma string.
        /// </summary>
        /// <param name="text">String que será removido os acentos.</param>
        /// <returns>String sem os acentos.</returns>
        public static string RemoveAcentos(this string text)
        {
            StringBuilder retorno = new StringBuilder();
            var caracteres = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char caracter in caracteres)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                    retorno.Append(caracter);
            }
            return retorno.ToString();
        }

        /// <summary>
        /// Extensão de string que remove todos os espaços possíveis de uma string.
        /// </summary>
        /// <param name="texto">String que será removido os espaços.</param>
        /// <returns>String sem os espaços.</returns>
        public static string RemoveEspacos(this string texto)
        {
            if (!String.IsNullOrEmpty(texto))
            {
                var textoFormatado = texto.Trim().ToLower();
                textoFormatado = Regex.Replace(textoFormatado, @"\s", String.Empty);
                return textoFormatado;
            }

            return String.Empty;
        }

        /// <summary>
        /// Extensão de string para limpar o texto removendos os acentos e os espaços.
        /// </summary>
        /// <param name="texto">String que será alterada.</param>
        /// <returns>String sem espaços e sem acentos.</returns>
        public static string LimparTexto(this string texto)
        {
            return RemoveAcentos(RemoveEspacos(texto));
        }

        public static string RetornaUltimosCaracteresCPF(this string texto)
        {
            var quantidadeCaracteres = texto.Count();
            var retorno = texto[quantidadeCaracteres - 2].ToString();
            retorno += texto[quantidadeCaracteres - 1].ToString();
            return retorno;
        }

        public static string RetornaNomeSemEspacos(this string texto)
        {
            var msg = texto.Split(' ');
            var retorno = string.Empty;

            if (msg.Count() > 1)
            {
                retorno = msg[0] + msg[1];
            }
            else
            {
                retorno = msg[0];
            }

            return retorno;
        }

    }
}
