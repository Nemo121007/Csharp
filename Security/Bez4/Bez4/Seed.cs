using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bez4
{
    internal static class Seed
    {
        /// <summary>
        /// Получение и запись в файл сида
        /// </summary>
        public static void WriteSeed()
        {
            var seed = GenerateSeed();
            string path = "";
            while (true)
            {
                Console.WriteLine("Введите полный адрес файла-ключа для вывода: ");
                path = Console.ReadLine();
                if (!File.Exists(path))
                {
                    try
                    {
                        File.WriteAllText(path, "test data");
                        Console.WriteLine("Адрес вывода корректен");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Адрес некоректен\n Повторите ввод");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("\nДанный файл-ключ уже существует. Перезаписать его?\n" +
                                      "1. Перезаписать\n" +
                                      "2. Отменить запись\n" +
                                      "Выберите операцию:");
                    int i = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (i == 2)
                        path = null;
                    break;
                }
            }
            var key = seed[0].ToString() + " " + seed[1].ToString() + " " + seed[2].ToString() + " " + seed[3].ToString();
            File.WriteAllText(path, key);
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
    }
}
