using System;

namespace MethodDissectionSeparation 
{
    internal static class Encoding
    {
        private static void Main()
        {
            string[] encryptedData = new string[10];
            string s = "";
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("МЕНЮ:");
                Console.WriteLine("1. Зашифровать сообщение");
                Console.WriteLine("2. Расшифровать сообщение");
                Console.WriteLine("3. Завершить работу");
                Console.WriteLine("Выберите действие:");
                try
                {
                    int step = int.Parse(Console.ReadLine());
                    if (step == 1)
                    {
                        encryptedData = Encrypt();
                    }
                    else if (step == 2)
                    {
                        s = Decrypt(encryptedData);
                        Console.WriteLine();
                        Console.WriteLine("Расшифрованное сообщение:");
                        Console.WriteLine();
                        Console.WriteLine(s);
                        Console.WriteLine();
                    }
                    else break;
                }
                catch
                {
                    break;
                }
            }
        }   

        private static char [,] WriteData(string str, int lengthKeyCollumn, int countLine)
        {
            
            char[,] writeData = new char[countLine, lengthKeyCollumn];
            int num = 0;
            int ik = 0;
            while (true)
            {
                for (int j = 0; j < lengthKeyCollumn; j++)
                {
                    if (num >= str.Length)
                        return writeData;
                    writeData[ik, j] = str[num];
                    num++;
                }
                ik++;
            }
        }

        private static string[] EncryptedData(char [,] writeData, int [] keyLine, int [] keyColumn , int countLine)
        {
            int k;
            // Создание массива под блоки 
            string[] encryptedData = new string[keyLine.Length * keyColumn.Length + 1];
            // Проход под всем цифрам
            for (int i = 0; i < countLine; i++)
            {
                for (int j = 0; j < keyColumn.Length; j++)
                {
                    // подсчёт блока для записи и запись символа в конец блока
                    k = keyColumn.Length * (keyLine[i] - 1) + keyColumn[j];
                    encryptedData[k] += writeData[i, j];
                }
            }
            return encryptedData;
        }

        private static string DecryptionData(string[] encryptedData, int[] keyLine, int[] keyColumn, int countLine)
        {
            int k;
            string s = "";
            // Перебор по всем блокам
            for (int i = 0; i < countLine; i++)
                for (int j = 0; j < keyColumn.Length; j++)
                {
                    // Вычисление блока, откуда скопировать очередной символ
                    k = keyColumn.Length * (keyLine[i] - 1) + keyColumn[j];
                    // Запись символа в строку для вывода
                    s += encryptedData[k][0];
                    // Удаление скопированного символа из блока
                    encryptedData[k] = encryptedData[k].Remove(0, 1);
                }
            return s;
        }   

        private static string [] Encrypt()
        {
            var key = ReadKey();

            Console.WriteLine("Введите текст для шифровки:");
            string str = Console.ReadLine();

            //string str = "МЕТОД РАССЕЧЕНИЯ-РАЗНЕСЕНИЯ.";
            //string str = "«Мой дядя самых честных правил,\r\nКогда не в шутку занемог,\r\nОн уважать себя заставил\r\nИ лучше выдумать не мог.\r\nЕго пример другим наука;\r\nНо, боже мой, какая скука\r\nС больным сидеть и день и ночь,\r\nНе отходя ни шагу прочь!\r\nКакое низкое коварство\r\nПолуживого забавлять,\r\nЕму подушки поправлять,\r\nПечально подносить лекарство,\r\nВздыхать и думать про себя:\r\nКогда же черт возьмет тебя!»";
            var keyColumn = key[0].Split(' ').Select(int.Parse).ToArray();
            int countLine = (int)Math.Ceiling(Convert.ToDouble(str.Length) / keyColumn.Length);

            var protoKeyLine = key[1].Split(' ').Select(int.Parse).ToArray();

            var writeData = WriteData(str, keyColumn.Length, countLine);

            int[] keyLine = new int[Math.Max(countLine, protoKeyLine.Length)];
            for (int i = 0; i < countLine; i++)
                keyLine[i] = protoKeyLine[i % protoKeyLine.Length];
            
            var encryptedData = EncryptedData(writeData, keyLine, keyColumn, countLine);

            //for (int i = 1; i < encryptedData.Length; i++)
            //    Console.WriteLine(encryptedData[i]);
            
            Console.WriteLine("Шифровка завершена");
            Console.WriteLine();
            return encryptedData;
        }

        private static string Decrypt(string[] encryptedData)
        {
            var key = ReadKey();

            int countSymbol = 0;
            for (int i = 1; i < encryptedData.Length; i++)
                if (encryptedData[i] != null)
                    countSymbol += encryptedData[i].Length;

            var keyColumn = key[0].Split(' ').Select(int.Parse).ToArray();
            int countLine = (int)Math.Ceiling(Convert.ToDouble(countSymbol) / keyColumn.Length);

            var protoKeyLine = key[1].Split(' ').Select(int.Parse).ToArray();

            int[] keyLine = new int[countLine];
            for (int i = 0; i < countLine; i++)
                keyLine[i] = protoKeyLine[i % protoKeyLine.Length];

            var s = DecryptionData(encryptedData, keyLine, keyColumn, countLine);
            return s;
        }

        private static string[] ReadKey ()
        {
            string[] key = new string[2];
            Console.WriteLine("Введите первый ключ:");
            key[0] = Key(0);
            Console.WriteLine("Введите второй ключ:");
            key[1] = Key(1);
            return key;
        }

        private static string Key(int k)
        {
            while (true)
            {
                int longKeyColumn = 5;
                int longKeyLine = 2;
                string key = Console.ReadLine();
                try
                {
                    var keyArray = key.Split(' ').Select(int.Parse).ToArray();
                    if (((k == 0) && (keyArray.Length > longKeyColumn)) || ((k == 1) && (keyArray.Length > longKeyLine)))
                    {
                        Console.WriteLine("Ошибка ввода");
                        continue;
                    }
                    for (int i = 0; i < keyArray.Length; i++)
                    {
                        if (keyArray[i] <= 0)
                            break;
                        if (i == keyArray.Length - 1)
                            return key;
                    }
                    Console.WriteLine("Ошибка ввода"); 
                }
                catch
                {
                    Console.WriteLine("Ошибка ввода");
                }
            }
        }
    }   
}