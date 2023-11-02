using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var result = new double[g.GetLength(0), g.GetLength(1)];
            var sy = new double[sx.GetLength(0), sx.GetLength(1)];
            for (var i = 0; i < sy.GetLength(0); i++)
                for (var j = 0; j < sy.GetLength(1); j++)
                    sy[i, j] = sx[j, i];
            
            int step = sx.GetLength(0) / 2;

            double gradientX = 0.0;
            double gradientY = 0.0;

            for (var i = 0; i < g.GetLength(0); i++)
                for (var j = 0; j < g.GetLength(1); j++)
                    try
                    {
                        for (var k = -step; k <= step; k++)
                            for (var l = -step; l <= step; l++)
                            {
                                gradientX += sx[i + k, j + l] * g[i + k, j + l];
                                gradientY += sy[i + k, j + l] * g[i + k, j + l];
                            }
                        result[i, j] = Math.Sqrt(gradientX * gradientX + gradientY * gradientY);
                    }
                    catch
                    {
                        continue;
                    }
            return result;
            
            
            
            
            //var width = g.GetLength(0);
            //var height = g.GetLength(1);
            //var result = new double[width, height];
            //for (int x = 1; x < width - 1; x++)
            //    for (int y = 1; y < height - 1; y++)
            //    {
            //        // Вместо этого кода должно быть поэлементное умножение матриц sx и полученной транспонированием из неё sy на окрестность точки (x, y)
            //        // Такая операция ещё называется свёрткой (Сonvolution)
            //        var gx = 
            //            -g[x - 1, y - 1] - 2 * g[x, y - 1] - g[x + 1, y - 1] 
            //            + g[x - 1, y + 1] + 2 * g[x, y + 1] + g[x + 1, y + 1];
            //        var gy = 
            //            -g[x - 1, y - 1] - 2 * g[x - 1, y] - g[x - 1, y + 1] 
            //            + g[x + 1, y - 1] + 2 * g[x + 1, y] + g[x + 1, y + 1];
            //        result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            //    }
            //return result;
        }
    }
}