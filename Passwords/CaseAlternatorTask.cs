using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        //Тесты будут вызывать этот метод
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            //AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            if (startIndex == word.Length)
            {
                var str = new string(word);
                if (!result.Contains(str))
                    result.Add(str);
                return;
            }
            
            if (Char.IsLetter(word[startIndex]))
            {
                word[startIndex] = Char.ToLower(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);

                word[startIndex] = Char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
            }
            else
                AlternateCharCases(word, startIndex + 1, result);
        }
    }
}