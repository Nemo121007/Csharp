using System;
using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var thresholdList = new List<double>();
            var xlength = original.GetLength(0);
            var ylength = original.GetLength(1);
            var thresholdFilter = new double[xlength, ylength];
            for (int i = 0; i < ylength; i++)
                for (int j = 0; j < xlength; j++)
                    thresholdList.Add(original[j, i]);
            thresholdList.Sort();

            double threshold = (int)(whitePixelsFraction * thresholdList.Count);

            if (threshold > 0 && threshold <= thresholdList.Count)
                threshold = thresholdList[(int)(thresholdList.Count - threshold)];
            else
                threshold = double.MaxValue;

            for (int y = 0; y < ylength; y++)
                for (int x = 0; x < xlength; x++)
                    if (original[x, y] >= threshold)
                        thresholdFilter[x, y] = 1.0;
                    else
                        thresholdFilter[x, y] = 0.0;
            return thresholdFilter;
        }
    }
}