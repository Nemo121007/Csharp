using Bez4;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Names
{
    public static class Program
    {
        private static void Main()
        {
            int workMain = 1;
            while (workMain != 0)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Сгенерировать  и записать ключ\n" +
                                  "2. Зашифровать файл\n" +
                                  "3. Расшифровать файл\n" +
                                  "4. Выход\n" +
                                  "Введите команду:");
                workMain = (int)Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (workMain)
                {
                    case 1:
                        Seed.WriteSeed();
                        continue;
                    case 2:
                        Encode.Encoding();
                        Console.WriteLine("Шифровка завершена\n");
                        continue;
                    case 3:
                        Encode.Encoding();
                        Console.WriteLine("Дешифровка завершена\n");
                        continue;
                    case 4:
                        workMain = 0;
                        break;
                    default:
                        continue;
                }
            }
        } 
    }
}