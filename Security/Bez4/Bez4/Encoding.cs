using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bez4
{
    public static class Encode
    {
        /// <summary>
        /// Гаммирование. Ключи получаются внутри метода
        /// </summary>
        public static void Encoding()
        {
            var seed = ReadKey();
            var dataReadFile = ReadFile();
            var pathWriteFile = PathWriteFile();

            // Установка таймера для времени
            var startTime = System.Diagnostics.Stopwatch.StartNew();

            int c = seed[3];
            // Пропуск первых n чисел
            for (int i = 0; i < seed[3]; i++)
                c = GenerateNumber(seed[0], seed[1], seed[2], c);

            int byteCode;
            int byteKey = c;
            byte[] bytesWrite = new byte[dataReadFile.Length];
            // Проход по всем байтам
            for (int i = 0; i < dataReadFile.Length; i++)
            {
                // Отсечение очередного байта числа для кодирования
                if (byteKey > 256)
                {
                    byteCode = byteKey % 256;
                    byteKey = byteKey / 256;
                }
                // Если число меньше байта, остаток использовать для шифровки и сгенерировать новое случайное число
                else
                {
                    byteCode = byteKey;
                    c = GenerateNumber(seed[0], seed[1], seed[2], c);
                    byteKey = c;
                }
                bytesWrite[i] = (byte)(dataReadFile[i] ^ byteCode);
            }
            File.WriteAllBytes(pathWriteFile, bytesWrite);

            // Подсчёт затраченного времени
            startTime.Stop();
            var resultTime = startTime.Elapsed;

            // elapsedTime - строка, которая будет содержать значение затраченного времени
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                resultTime.Hours,
                resultTime.Minutes,
                resultTime.Seconds,
                resultTime.Milliseconds);

            Console.WriteLine("Файл сохранён\n Адрес: {0} \n Время гаммирования{1}", pathWriteFile, elapsedTime);
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
        /// Получение ключа из файла 
        /// </summary>
        /// <returns>Сид</returns>
        private static int[] ReadKey()
        {
            int[] key = new int[4];
            while(true)
            {
                // При отсутствии ошибок: чтение и запись в сид
                try
                {
                    Console.WriteLine("Введите полный адрес файла-ключа");
                    var data = File.ReadAllText(Console.ReadLine()).Split(' ');
                    // Key и data должны оказаться одного размера (4 int-а)
                    for (int i = 0; i < data.Length; i++)
                        key[i] = Convert.ToInt32(data[i]);
                    Console.WriteLine("Чтение файла-ключа прошло успешно\n");
                    break;
                }
                // При ошибках - повторный ввод
                catch
                {
                    Console.WriteLine("При чтении произошла ошибка\n Повторите ввод\n");
                    continue;
                }
            }
            return key;
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        /// <returns>Массив байтов, состовляющих файл</returns>
        private static byte[] ReadFile()
        {
            string readPath = "";
            while (true)
            {
                Console.WriteLine("Введите полный адрес файла для чтения");
                readPath = Console.ReadLine();
                // При некоректном вводе файла - повторный ввод
                if (File.Exists(readPath))
                { 
                    Console.WriteLine("Файл найден");
                    break;
                }
                else
                {                 
                    Console.WriteLine("При поиске произошла ошибка\n Повторите ввод");
                    continue;
                }
            }
            var dataChar = File.ReadAllBytes(readPath);
            Console.WriteLine("Чтение прошло успешно\n");
            return dataChar;
        }
    
        /// <summary>
        /// Получение пути для записи файла
        /// </summary>
        /// <returns>Путь для записи файла</returns>
        private static string PathWriteFile()
        {
            string path = "";
            while (true)
            {
                Console.WriteLine("Введите полный адрес файла для вывода: ");    
                path = Console.ReadLine();
                // Если файла не существует
                if (!File.Exists(path))
                {
                    try
                    {
                        File.WriteAllText(path, "test data");
                        Console.WriteLine("Адрес вывода корректен\n");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Адрес некоректен\n Повторите ввод");
                        continue;
                    }
                }
                // Если файл существует
                else
                {
                    Console.WriteLine("Файл уже существует. Перезаписать его?\n" +
                                      "1. Перезаписать\n" + 
                                      "2. Отменить запись\n" +
                                      "Выберите операцию:");
                    int i = Convert.ToInt32(Console.ReadLine());
                    if (i == 2)
                        path = null;
                    break;
                }
            }
            Console.WriteLine("Файл успешно записан. Адрес: {0}\n\n", path);
            return path;
        }
    }
}
