using System;

namespace Procents
{
    internal static class Procents
    {
        private static void Main()
        {
            string userInput = Console.ReadLine();
            userInput = userInput.Replace('.', ',');
            Console.WriteLine(Calculate(userInput));
        }
        public static double Calculate(string userInput)
            //double beginSum, double procents, int time
        {
            string[] data = userInput.Split(' ');
            double beginSum = double.Parse(data[0]);
            double procents = double.Parse(data[1]);
            double time = double.Parse(data[2]);
            double res = beginSum * Math.Pow(1 + procents / (12 * 100), time);
            return res;
        }
    }
}