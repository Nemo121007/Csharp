using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            string [] number = new string[30];
            for (int i = 0; i < 30; i++)
                number[i] = (i + 2).ToString();

            string [] month = new string[12];
            for (int i = 0; i < 12; i++)
                month[i] = (i + 1).ToString();

            double[,] birtCount = new double[30, 12];
            foreach (var people in names)
            {
                if (people.BirthDate.Day != 1)
                    birtCount[people.BirthDate.Day - 2, people.BirthDate.Month - 1] += 1;
            }
            
            return new HeatmapData(
               "Пример карты интенсивностей",
               birtCount,number, month);
               
               //new[] { "a", "b", "c", "d" },
               //new[] { "X", "Y", "Z" });



            /*
            return new HeatmapData(
                "Пример карты интенсивностей",
                new double[,] { { 1, 2, 3 }, 
                                { 2, 3, 4 }, 
                                { 3, 4, 4 }, 
                                { 4, 4, 4 } }, 
                new[] { "a", "b", "c", "d" }, 
                new[] { "X", "Y", "Z" });
            */
        }
    }
}