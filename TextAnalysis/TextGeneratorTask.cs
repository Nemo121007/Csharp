using System.Collections.Generic;
using System;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            StringBuilder ending = new StringBuilder(phraseBeginning);

            int i = 0;
            string beggining;
            while (i < wordsCount)
            {
                phraseBeginning = ending.ToString();
                var phrase = phraseBeginning.Split(' ');
                if (phrase.Length > 1)
                {
                    beggining = phrase[phrase.Length - 2] + " " + phrase[phrase.Length - 1];
                    if (nextWords.ContainsKey(beggining))
                    {
                        ending.Append(" " + nextWords[beggining]);
                        i += 2;
                        continue;
                    }
                }
             
                beggining = phrase[phrase.Length - 1];
                if (nextWords.ContainsKey(beggining))
                {
                    ending.Append(" " + nextWords[beggining]);
                    i++;
                }
                else return ending.ToString();
            }
            return ending.ToString();
        }
    }
}