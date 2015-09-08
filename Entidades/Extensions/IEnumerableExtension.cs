using System.Collections.Generic;
using System.Linq;

namespace Entidades.Extensions
{
    public static class EnumerableExtension
    {
        private static List<string> _list;

        public static string BuildStringFormatedTextFromListWithCollonSplitingElements(this IEnumerable<string> list)
        {
            _list = list.ToList();

            RemoveNullAndEmptySpaces();

            if (!_list.Any())
                return string.Empty;

            if (_list.Count == 1)
                return _list.First();

            var formatedMessage = FormatItensWithcommaExceptTheLasOne();
            return IncreaseEStatementAndAddLastListItem(formatedMessage);
        }

        private static void RemoveNullAndEmptySpaces()
        {
            _list.RemoveAll(string.IsNullOrEmpty);
        }

        private static string FormatItensWithcommaExceptTheLasOne()
        {
            return string.Join(", ", _list.ToArray(), 0, _list.Count() - 1);
        }

        private static string IncreaseEStatementAndAddLastListItem(string formatedMessage)
        {
            var message = formatedMessage + " e ";
            message += _list.Last();
            return message;
        }
    }
}