using System.Collections.Generic;
using System.Linq;

namespace Entidades.Extensions
{
    public static class ListExtension
    {
        public static string BuildStringFormatedTextFromListWithCollonSplitingElements(this IEnumerable<string> list)
        {
            list = list.ToList();

            if (list.ToList().All(a => a == null))
                return string.Empty;

            var formatedMessage = FormatItensWithcommaExceptTheLasOne(list);
            return IncreaseEStatementAndAddLastListItem(list, formatedMessage);
        }

        private static string IncreaseEStatementAndAddLastListItem(IEnumerable<string> list, string formatedMessage)
        {
            var message = formatedMessage + " e ";
            message += list.Last();
            return message;
        }

        private static string FormatItensWithcommaExceptTheLasOne(IEnumerable<string> list)
        {
            return string.Join(", ", list.ToArray(), 0, list.Count() - 1);
        }
    }
}
