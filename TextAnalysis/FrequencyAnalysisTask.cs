using System.Collections.Generic;
using System;


namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var combinedVersion = new Dictionary<string, string>();
            var dictBigrams = GetMostFrequentBigrams(text);
            var dictTrigrams = GetMostFrequentTrigrams(text);
            foreach (var data in dictBigrams)
                combinedVersion.Add(data.Key, data.Value);
            foreach (var data in dictTrigrams)
                combinedVersion.Add(data.Key, data.Value);
            return combinedVersion;
        }

        public static Dictionary<string, string> GetMostFrequentBigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> bigrams = FillBigrams(text);
            Dictionary<string, string> mostFrequentBigrams = new Dictionary<string, string>();
            foreach (var firstWord in bigrams)
            {
                var maxcount = 0;
                string mostFrequentSecondWord = null;

                foreach (var secondWord in firstWord.Value)
                {
                    if (secondWord.Value == maxcount)
                        if (string.CompareOrdinal(mostFrequentSecondWord, secondWord.Key) > 0)
                            mostFrequentSecondWord = secondWord.Key;
                    if (secondWord.Value > maxcount)
                    {
                        mostFrequentSecondWord = secondWord.Key;
                        maxcount = secondWord.Value;
                    }
                }
                mostFrequentBigrams.Add(firstWord.Key, mostFrequentSecondWord);
            }

            return mostFrequentBigrams;
        }

        public static Dictionary<string, Dictionary<string, int>> FillBigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> listBigrams = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    if (!listBigrams.ContainsKey(sentence[i]))
                        listBigrams.Add(sentence[i], new Dictionary<string, int>());
                    if (!listBigrams[sentence[i]].ContainsKey(sentence[i + 1]))
                        listBigrams[sentence[i]].Add(sentence[i + 1], 1);
                    else listBigrams[sentence[i]][sentence[i + 1]] += 1;
                }
            }

            return listBigrams;
        }

        public static Dictionary<string, string> GetMostFrequentTrigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> trigrams = FillTrigrams(text);
            Dictionary<string, string> mostFrequentTrigrams = new Dictionary<string, string>();

            foreach (var wordPair in trigrams)
            {
                var maxcount = 0;
                string mostFrequentThirdWord = null;

                foreach (var ThirdWord in wordPair.Value)
                {
                    if (ThirdWord.Value == maxcount)
                        if (string.CompareOrdinal(mostFrequentThirdWord, ThirdWord.Key) > 0)
                            mostFrequentThirdWord = ThirdWord.Key;
                    if (ThirdWord.Value > maxcount)
                    {
                        mostFrequentThirdWord = ThirdWord.Key;
                        maxcount = ThirdWord.Value;
                    }
                }
                mostFrequentTrigrams.Add(wordPair.Key, mostFrequentThirdWord);
            }
            return mostFrequentTrigrams;
        }

        public static Dictionary<string, Dictionary<string, int>> FillTrigrams(List<List<string>> text)
        {
            Dictionary<string, Dictionary<string, int>> listTrigrams = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 2; i++)
                {
                    if (!listTrigrams.ContainsKey(sentence[i] + " " + sentence[i + 1]))
                        listTrigrams.Add(sentence[i] + " " + sentence[i + 1], new Dictionary<string, int>());
                    if (!listTrigrams[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i + 2]))
                        listTrigrams[sentence[i] + " " + sentence[i + 1]].Add(sentence[i + 2], 1);
                    else listTrigrams[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]] += 1;
                }
            }

            return listTrigrams;
        }
    }
}