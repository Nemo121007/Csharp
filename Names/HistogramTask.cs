using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            double[] countPeople = new double[31];
            string[] numberDay = new string[31];

            for (int i = 0; i < 31; i++)
            {
                numberDay[i] = (i + 1).ToString();
                foreach (var people in names)
                {
                    if ((people.BirthDate.Day != 1) && (people.BirthDate.Day == i + 1) && (people.Name == name))
                        countPeople[i]++;
                }
            }

            return new HistogramData(
                    string.Format("Рождаемость людей с именем '{0}'", name),
                    numberDay,
                    countPeople);
 
        }
    }
}

