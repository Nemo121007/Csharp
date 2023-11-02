using System.Linq;
using System;
using System.Collections.Generic;

namespace Recognizer
{
	internal static class MedianFilterTask
	{
        /* 
		 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
		 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
		 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
		 * https://en.wikipedia.org/wiki/Median_filter
		 * 
		 * Используйте окно размером 3х3 для не граничных пикселей,
		 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
		 */
        public static double[,] MedianFilter(double[,] original)
        {
            var medianList = new List<double>();
            var xlength = original.GetLength(0);
            var ylength = original.GetLength(1);

            double[] medianArray = new double[9];
            var medianFilter = new double[xlength, ylength];

            for (int y = 0; y < ylength; y++)
                for (int x = 0; x < xlength; x++)
                {
                    for(int i = -1; i < 2; i++)
                        for (int j = -1; j < 2; j++)
                            if ((y + i != -1) && (y + i != ylength))
                                if((x + j != -1) && (x + j != xlength))
                                    medianList.Add(original[x + j, y + i]);
                    
                    
                    medianList.Sort();
                    if (medianList.Count % 2 == 0)
                        medianFilter[x, y] = (medianList[medianList.Count / 2] + medianList[medianList.Count / 2 - 1]) / 2;
                    else
                        medianFilter[x, y] = medianList[medianList.Count / 2];
                    medianList.Clear();
                }
            return medianFilter;
        }
    }
}