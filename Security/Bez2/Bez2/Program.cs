using System;

namespace MethodDissectionSeparation
{
    internal static class Encoding
    {
        private static void Main()
        {
            int [] controlByte = {1, 2, 4, 8, 16, 32};
            string key = "00000";
            Console.WriteLine("Введите исходную последовательность:");
            string str = EnterData(15);
            //string str = "100100101110001";

            int c = 1;
            int j = 0;
            int strI = 1;
            while(strI != str.Length)
            {
                if (c == controlByte[j])
                {
                    j++;
                    c++;
                    continue;
                }
                else
                {
                    strI++;
                    c++;
                }
            }
            
            
            string fullStr = EncodingStr(str, controlByte, key, c);
            var matrix = MatrixSum(controlByte, fullStr.Length);

            key = VectorControlSum(matrix, fullStr.Length, fullStr);
            fullStr = EncodingStr(str, controlByte, key, c);
            Console.WriteLine();
            Console.WriteLine("Зашифрованная строка");
            Console.WriteLine(fullStr);
            for (int i = 0; i < fullStr.Length; i++)
                Console.Write('-');
            Console.WriteLine();
            for (int i = 1; i < matrix.Length; i++)
                Console.WriteLine(matrix[i]);
            Console.WriteLine("Введите переданную строку:");
            fullStr = EnterData(20);
            int n = Check(matrix, fullStr);
            if (n == 0)
                Console.WriteLine("Передача корректна");
            else
            {
                if (n <= fullStr.Length)
                {
                    Console.WriteLine("Ошибка в " + n.ToString() + " позиции");
                    Console.WriteLine("Исправленный вариант:");
                    n--;
                    if (fullStr[n] == '0')
                        fullStr = fullStr.Remove(n, 1).Insert(n, "1");
                    else
                        fullStr = fullStr.Remove(n, 1).Insert(n, "0");
                    Console.WriteLine(fullStr);
                }
                else
                {
                    Console.WriteLine("В передаче более двух ошибок");
                }
            }
        }

        private static string EnterData(int maxLenght)
        {
            while (true)
            {
                string str = Console.ReadLine();
                if ((str.Length < maxLenght + 1) && (str != null))
                    for (int i = 0; i < str.Length; i++)
                    {
                        if ((str[i] != '1') && (str[i] != '0'))
                            break;
                        if (i == str.Length - 1)
                            return str;
                    }
                Console.WriteLine("Ошибка. Повторите ввод");
            }
        }

        private static string [] MatrixSum(int[] controlByte, int strLenght)
        {
            string[] strLine = new string[7];
            int j;
            // Проход по строкам матрицы
            for (int i = 1; i < 6; i++)
            {
                j = 1;
                // Проход по символам матрицы
                while (j < strLenght + 1)
                {
                    if (j % controlByte[i - 1] == 0)
                    {
                        // Растановка 1 и 0 согласно правилу
                        for (int k = 0; k < controlByte[i - 1]; k++)
                        {
                            if (j > strLenght)
                                break;
                            strLine[i] += '1';
                            j++;
                        }
                        for (int k = 0; k < controlByte[i - 1]; k++)
                        {
                            if (j > strLenght)
                                break;
                            strLine[i] += '0';
                            j++;
                        }
                    }
                    else
                    {
                        strLine[i] += '0';
                        j++;
                    }
                } 
            }
            return strLine;
        }

        private static string VectorControlSum(string[] matrix, int strLenght, string str)
        {
            int num;
            string key = "";
            // Проход по строкам матрицы
            for (int i = 1; i < 6; i++)
            {
                num = 0;
                // Проход по символам
                for (int j = 0; j < strLenght; j++)
                {
                    // Если оба символа равны 1, увеличиваем счётчик
                    if ((matrix[i][j] == str[j]) && (str[j] == '1'))
                        num++;
                }
                // Получаем значения байти из счётчика и запись в ключ
                if (num % 2 == 1)
                    key += '1';
                else
                    key += '0';
            }
            return key;
        }

        private static string EncodingStr(string str, int[] controlByte, string key, int strLenght)
        {
            int kStr = 0;
            int kMaxControlLength = 0;
            string fullString = "";
            // Проход по символам 
            for (int i = 1; i < strLenght + 1; i++)
            {
                // Если байт - контрольный, то подставляем значение из ключа. Иначе следующий символ изначальной строки
                if (i == controlByte[kMaxControlLength])
                {
                    fullString += key[kMaxControlLength];
                    kMaxControlLength++;
                }
                else
                {
                    fullString += str[kStr];
                    kStr++;
                }
            }
            return fullString;
        }

        private static int Check(string [] matrix, string fulLine)
        {
            // подсчёт матрицы синдромов
            string key = VectorControlSum(matrix,  fulLine.Length, fulLine);
            // проверка матрицы синдромов
            for (int i = 0; i < key.Length; i++)
                if (key[i] == '0')
                {
                    if (i == key.Length - 1)
                        return 0;
                    continue;
                }
                else
                    break;
            // подсчёт номера ошибочного символа
            char[] ch = key.ToCharArray();
            Array.Reverse(ch);
            key = new string(ch);
            int n = Convert.ToInt32(Convert.ToUInt32(key, 2));
            return n;
        }
    }
}