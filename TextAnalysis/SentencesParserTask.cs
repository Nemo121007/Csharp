using System.Collections.Generic;
using System;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            text = text.ToLower();
            List<List<string>> list = new List<List<string>>();
            string[] strText = text.Split(new char[] { '.', '!', '?', ';', ':', '(', ')' });
            foreach (string word in strText)
            {
                List<string> listWord = new List<string>();
                var textSentence = SeparateString(word);

                string[] listWordFromStr = textSentence.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var varWord in listWordFromStr)
                {
                    if ((varWord !=""))
                        listWord.Add(varWord);
                }
                if (listWord.Count == 0) continue;
                else list.Add(listWord);
            }
            return list;
        }

        private static string SeparateString(string word)
        {
            var textString = "";

            foreach (var simbol in word)
            {
                if (char.IsLetter(simbol) || (simbol == '\''))
                    textString = textString + simbol;
                else textString = textString + ' ';
            }
            return textString;
        }
    }
}