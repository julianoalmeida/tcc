using System.Globalization;
using System.Linq;
using System.Text;

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
            if (string.IsNullOrEmpty(text)) return string.Empty;

            var formatedText = string.Empty;
            var caracteres = text.Normalize(NormalizationForm.FormD).ToCharArray();

            return caracteres.Where(caracter => CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                             .Aggregate(formatedText, (current, caracter) => current + caracter);
        }

        public static string RemoveEmptySpaces(this string text)
        {
            return string.IsNullOrEmpty(text)
                ? string.Empty
                : text.Trim();
        }

        public static string RemoveAccentsAndEmptySpaces(this string text)
        {
            return RemoveAccents(RemoveEmptySpaces(text));
        }

        public static string GetTwoLastCharacters(this string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty : text.Substring(text.Count() - 2);
        }
    }
}