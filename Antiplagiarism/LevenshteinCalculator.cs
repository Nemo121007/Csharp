using System;
using System.Configuration;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;


namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        // Метод сравнивает документы попарно и возвращает результаты сравнения
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var result = new List<ComparisonResult>();

            // Перебор комбинаций документов
            for (int i = 0; i < documents.Count; i++)
                for (int j = i + 1; j < documents.Count; j++)
                    result.Add(CompareDocs(documents[i], documents[j]));

            return result;
        }

        // Вернуть результат сравнения документов
        ComparisonResult CompareDocs(DocumentTokens firstDoc, DocumentTokens secondDoc)
        {
            double differenceSize;
            double[,] levenshteinDistance = new double[firstDoc.Count + 1, secondDoc.Count + 1];

            // Инициализация матрицы для вычисления расстояния Левенштейна
            for (int i = 0; i <= firstDoc.Count; i++) 
                levenshteinDistance[i, 0] = i; 
            for (int j = 0; j <= secondDoc.Count; j++) 
                levenshteinDistance[0, j] = j; 

            for (int i = 1; i <= firstDoc.Count; i++)
                for (int j = 1; j <= secondDoc.Count; j++)
                {
                    // Вычисление разницы между текущими токенами
                    if ((firstDoc[i - 1] == secondDoc[j - 1]))
                        differenceSize = 0;
                    else
                        differenceSize = TokenDistanceCalculator.GetTokenDistance(firstDoc[i - 1], secondDoc[j - 1]);

                    // Вычисление минимального расстояния
                    levenshteinDistance[i, j] = Math.Min
                        (
                            Math.Min(
                                levenshteinDistance[i - 1, j] + 1,
                                levenshteinDistance[i, j - 1] + 1
                            ),
                            levenshteinDistance[i - 1, j - 1] + differenceSize
                        );
                }

            // Возвращаем результат сравнения в виде объекта ComparisonResult
            return new ComparisonResult(firstDoc, secondDoc, levenshteinDistance[firstDoc.Count, secondDoc.Count]);
        }
    }
}
