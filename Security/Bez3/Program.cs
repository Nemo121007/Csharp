using System;
using System.Linq;
using System.Text;

namespace Names
{
    public static class Program
    {
        private static void Main()
        {
            int work = 1;
            var seed = new int[4];
            int c = 0;
            string comand;
            while (work != 0)
            {
                Console.WriteLine("Меню: \n" +
                                    "1. Сгенерировать новые сиды \n" +
                                    "2. Сгенерировать случайное число\n" +
                                    "3. Проверить правильность генерации чисел\n" +
                                    "4. Выход\n"+
                                    "Введите команду:");
                comand = Console.ReadLine();
                Console.WriteLine();
                if (comand == null || comand == "")
                    work = 0;
                else
                    work = Convert.ToInt32(comand);
                switch (work)
                {
                    case 1:
                        seed = GenerateSeed();
                        Console.WriteLine("Seeds: \n a = {0} \n b = {1} \n m = {2} \n c = {3} \n", seed[0], seed[1], seed[2], seed[3]);
                        continue;
                    case 2:
                        c = seed[3];
                        int generateWork = 1;
                        while (generateWork != 0)
                        {
                            Console.WriteLine("Генерация чисел:\n"
                                             +"1. Сгенерировать следующее число\n"
                                             +"2. Выход в основное меню\n"
                                             + "Введите команду:");
                            generateWork = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine();
                            switch(generateWork)
                            {
                                case 1:
                                    c = GenerateNumber(seed[0], seed[1], seed[2], c);
                                    Console.WriteLine("Следующее число: \t {0}\n", c);
                                    break;
                                case 2:
                                    generateWork = 0;
                                    break;
                                default:
                                    continue;
                            }
                        }
                        continue;
                    case 3:
                        Console.WriteLine("Введите количество генерируемых чсиел для проверки статистики");
                        int count = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Статистика по интервалам:");
                        CheckPrint(GenerateStatistic(seed, count), seed, count);
                        continue;
                    case 4:
                        work = 0;
                        break;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// Генерация сидов
        /// </summary>
        /// <returns>
        /// a = seed[0]
        /// b = seed[1]
        /// m = seed[2]
        /// c = seed[3]
        /// </returns>
        private static int[] GenerateSeed()
        {
            var seed = new int[4];
            // Получение элемента m
            seed[2] = (int)Math.Pow(2, 24);
            // Получение элемента а через суммирование цифр времени
            seed[0] = NumberFormation((DateTime.Now).ToString("HH:mm:ss"));
            // Нормирование а
            switch (seed[0] % 4)
            {
                case 0:
                    seed[0] += 1;
                    break;
                case 3:
                    seed[0] += 2;
                    break;
                case 2:
                    seed[0] += 3;
                    break;
                default:
                    break;
            }
            // Получение b через обработку названия машины
            seed[1] = NumberFormation(Environment.MachineName);
            // Нормирование b
            if (seed[1] % 2 == 0)
                seed[1]++;
            // Получение с из даты
            seed[3] = NumberFormation(DateTime.UtcNow.ToString("MM-dd-yyyy"));
            return seed;
        }
        
        /// <summary>
        /// Получение чисел из разных строк
        /// </summary>
        /// <param name="data">Строка для вычисления значения</param>
        /// <returns>Полученное число</returns>
        private static int NumberFormation(string data)
        {
            int number = 0;
            data = data.ToUpper();
            foreach (char ch in data)
            {
                // Если символ - цифра
                if (Char.IsNumber(ch))
                    number += ch - 48;
                // Если символ буква - сложение с кодом буквы относительно 0
                if (Char.IsLetter(ch))
                {
                    number += (ch - '9');
                }
            }
            return number;
        }

        /// <summary>
        /// Генерация числа
        /// </summary>
        /// <param name="a">Параметр a(seed[0])</param>
        /// <param name="b">Параметр b(seed[1]</param>
        /// <param name="m">Параметр m(seed[2]</param>
        /// <param name="c0">Предыдущее псевдослучайное число(seed[3]</param>
        /// <returns>Следующее псевдослучайное число</returns>
        private static int GenerateNumber(int a, int b, int m, int c0)
        {
            int c = (a * c0 + b) % m;
            return c;
        }

        /// <summary>
        /// Генерация массива чисел для проверки
        /// </summary>
        /// <param name="seed">
        /// a = seed[0]
        /// b = seed[1]
        /// m = seed[2]
        /// c = seed[3]
        /// </param>
        /// <param name="count">Количество чисел</param>
        /// <returns>
        /// Массив распределения чисел
        /// </returns>
        private static int[] GenerateStatistic(int[] seed, int count)
        {
            var c = new int[count];
            c[0] = seed[3];
            StringBuilder text = new StringBuilder();
            // Очистка файла для записи сгенерированных чисел
            System.IO.File.Delete("C:\\Users\\Saveliy\\Documents\\C#\\Bez3\\GenerateNumber.txt");
            for (int i = 1; i < count; i++)
            {
                // Генерация и запись в файл очередного сгенерированного числа
                c[i] = GenerateNumber(seed[0], seed[1], seed[2], c[i - 1]);
                text.Append((c[i].ToString()) + '\n');
            }
            System.IO.File.AppendAllText("C:\\Users\\Saveliy\\Documents\\C#\\Bez3\\GenerateNumber.txt", text.ToString());

            int step = seed[2] / 100;
            var distribution = new int[100];
            // Расчёт статистики попадения в интервалы
            foreach (int i in c)
            {
                if (i / step > 99)
                {
                    distribution[i / step - 1]++;
                    continue;
                }
                distribution[i / step]++;
            }
            return distribution;
        }

        /// <summary>
        /// Вывод результатов проверки
        /// </summary>
        /// <param name="distribution">Массив распределения чисел</param>
        /// <param name="seed">
        /// a = seed[0]
        /// b = seed[1]
        /// m = seed[2]
        /// c = seed[3]
        /// </param>
        /// <param name="count">Количество чисел</param>
        private static void CheckPrint(int[] distribution, int[] seed, int count)
        {
            double[] relativeDistribution = new double[100];
            string[] numberSegment = new string[100];
            for (int i = 0; i < 100; i++)
            {
                // Вывод статистики попадания в интервалы
                relativeDistribution[i] = Convert.ToDouble(distribution[i]) / count * 100;
                numberSegment[i] = (i + 1).ToString();
                Console.WriteLine("{0} \t {1}%", numberSegment[i], relativeDistribution[i]);
            }
            Console.WriteLine();

            double difference = relativeDistribution.Max() - relativeDistribution.Min();
            // Вывод гистограммы через стороннюю библиотеку
            Charts.ShowHistogram(new HistogramData(
                    string.Format("Относительная частота попадения чисел генератора в интервалы\n"
                                 + "a = {0}   b = {1}   c[0] = {2} количество чисел:{4}\n"
                                 + "Максимальная разница вероятностей {3}%", seed[0], seed[1], seed[3], difference, count),
                    numberSegment,
                    relativeDistribution));
        }
    }
}