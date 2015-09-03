using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Entidades.Extensions
{
    public static class StringExtension
    {
        public static string RemoveMaskCharacters(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var empty = string.Empty;
            return value.Replace(".", string.Empty)
                        .Replace("/", empty)
                        .Replace("-", empty)
                        .Replace(",", empty)
                        .Replace("(", empty)
                        .Replace(")", empty)
                        .Replace(" ", empty)
                        .Trim();
        }

        public static string RemoveAccents(this string text)
        {
            var formatedText = string.Empty;
            var caracteres = text.Normalize(NormalizationForm.FormD).ToCharArray();

            return caracteres.Where(caracter => CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                             .Aggregate(formatedText, (current, caracter) => current + caracter);
        }

        public static string RemoveEmptySpaces(this string text)
        {
            return string.IsNullOrEmpty(text)
                ? string.Empty
                : Regex.Replace(text.Trim().ToLower(), @"\s", string.Empty);
        }

        public static string RemoveAccentsAndEmptySpaces(this string texto)
        {
            return RemoveAccents(RemoveEmptySpaces(texto));
        }

        public static string GetTwoLastCpfCharacters(this string text)
        {
            var total = text.Count();
            return text.Substring(0, total - 2);
        }
    }
}
