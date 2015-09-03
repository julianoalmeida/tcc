using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Extensions
{
    public static class ListExtension
    {
        /// <summary>
        /// Extensão de IEnumarable<string> para formatar essa lista corretamente com ',' e entre os dois últimos registros com 'e'.
        /// </summary>
        /// <param name="camposInvalidos">Lista de campos inválidos.</param>
        /// <returns>String com os campos formatados.</returns>
        public static string RecuperarCamposFormatado(this IEnumerable<string> campos)
        {
            campos = campos.Where(a => a != null);
            if (campos.Count() == 0)
                return String.Empty;

            var mensagemFormatada = campos.First();

            if (campos.Count() > 1)
            {
                mensagemFormatada = String.Join(", ", campos.ToArray(), 0, campos.Count() - 1);
                mensagemFormatada += " e ";
                mensagemFormatada += campos.Last();
            }

            return mensagemFormatada;
        }
    }
}
