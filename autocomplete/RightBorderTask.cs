using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            // IReadOnlyList похож на List, но у него нет методов модификации списка.
            // Этот код решает задачу, но слишком неэффективно. Замените его на бинарный поиск!
            if (prefix == "" || phrases.Count == 0)
                return right;
            long middle;
            while (right != left)
            {
                middle = (left + right) / 2;
                if (string.Compare(phrases[Convert.ToInt32(middle)], prefix, StringComparison.OrdinalIgnoreCase) > 0)
                    right = Convert.ToInt32(middle);
                else
                    left = Convert.ToInt32(middle) + 1;
            }
            return right;
        }
    }
}