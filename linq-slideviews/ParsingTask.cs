using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        return lines
            .Skip(1)
            .Select(line =>
            {
                var record = line.Split(';');
                if (record.Length != 3 || !int.TryParse(record[0], out int slideId))
                    return null;
                switch (record[1])
                {
                    case "theory":
                        return new SlideRecord(slideId, SlideType.Theory, record[2]);
                        break;
                    case "quiz":
                        return new SlideRecord(slideId, SlideType.Quiz, record[2]);
                        break;
                    case "exercise":
                        return new SlideRecord(slideId, SlideType.Exercise, record[2]);
                        break;
                    default:
                        return null;
                }
            })
            .Where(line => line != null)
            .ToDictionary(line => line.SlideId);
    }

    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        {
            string format = "yyyy-MM-dd;HH:mm:ss";
            return lines.Skip(1).Select(line =>
            {
                var record = line
                            .Split(';')
                            .Where(line => !(line == ""))
                            .ToArray();
                if (record.Length < 4 ||
                    !int.TryParse(record[0], out int userID) ||
                    !int.TryParse(record[1], out int slideID) ||
                    !slides.TryGetValue(slideID, out SlideRecord slide) ||
                    !DateTime.TryParseExact(record[2] + ";" + record[3], format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
                    throw new FormatException($"Wrong line [{line}]");

                return new VisitRecord(userID, slideID, time, slide.SlideType);
            });
        }
    }
}