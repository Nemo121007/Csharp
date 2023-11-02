using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates.PairsAnalysis
{
    public static class Analysis
    {
        public static int FindMaxPeriodIndex(params DateTime[] data)
            => data
            .Pairs()
            .Select((dataTime) => (dataTime.Item2 - dataTime.Item1).TotalSeconds)
            .MaxIndex();

        public static int MaxIndex<T>(this IEnumerable<T> data)
            where T : IComparable
        {
            int index = 0;
            int i = 0;
            var max = default(T);

            foreach (var item in data)
            {
                if (i == 0)
                    max = item;
                if (item.CompareTo(max) == 1)
                {
                    index = i;
                    max = item;
                }
                i++;
            }

            if (i == 0)
                throw new InvalidOperationException();
            else
                return index;
        }

        public static IEnumerable<Tuple<T, T>> Pairs <T>(this IEnumerable<T> data)
        {
            bool oneElement = true;
            T now = default;

            foreach (var item in data)
            {
                if (oneElement)
                {
                    oneElement = false;
                    now = item;
                    continue;
                }
                yield return Tuple.Create(now, item);
                now = item;
            }

            if (oneElement)
                throw new InvalidOperationException();
        }

        public static double FindAverageRelativeDifference(params double[] data)
        {
            if (data.Length > 1)
                return new AverageDifferenceFinder().Analyze(data);
            else
                throw new InvalidOperationException();
        }
    }
}
