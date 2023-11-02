using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        // ћетод дл€ поиска путей от стартовой точки к цел€м (сундукам)
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            // —ловарь дл€ хранени€ путей от стартовой точки к разным точкам
            var tracks = new Dictionary<Point, SinglyLinkedList<Point>>();
            // »нициализаци€ пути от стартовой точки к самой себе
            tracks[start] = new SinglyLinkedList<Point>(start);

            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(tracks[start]);

            // ѕеребор сундуков дл€ нахождени€ путей к каждому из них
            foreach (var chest in chests)
            {
                // ≈сли путь к сундуку уже существует, вернуть его
                if (tracks.ContainsKey(chest))
                {
                    yield return tracks[chest];
                    continue;
                }

                // ¬ противном случае, выполнить поиск пути и вернуть его, если найден
                var path = FindPath(tracks, queue, map, chest);
                if (path != null)
                    yield return path;
            }
        }

        // ¬спомогательный метод дл€ поиска пути
        static SinglyLinkedList<Point> FindPath(Dictionary<Point, SinglyLinkedList<Point>> tracks,
                                                Queue<SinglyLinkedList<Point>> queue, Map map, Point end)
        {
            while (queue.Count != 0)
            {
                // »звлечение текущего узла из очереди
                var node = queue.Dequeue();
                // ѕолучение смежных узлов, которые можно достичь из текущего узла
                var incidentNodes =
                    Walker.PossibleDirections
                            .Select(direction => node.Value + direction)
                            .Where(point => map.InBounds(point) && map.Dungeon[point.X, point.Y] != MapCell.Wall);

                // ѕеребор смежных узлов
                foreach (var nextNode in incidentNodes)
                {
                    // ≈сли путь до узла уже существует, пропустить его
                    if (tracks.ContainsKey(nextNode))
                        continue;
                    // —оздание нового пути и добавление его в словарь и очередь
                    tracks[nextNode] = new SinglyLinkedList<Point>(nextNode, node);
                    queue.Enqueue(tracks[nextNode]);
                }

                // ≈сли путь до целевой точки (сундука) найден, вернуть его
                if (tracks.ContainsKey(end))
                    return tracks[end];
            }

            // ≈сли путь до целевой точки не был найден, вернуть null
            if (!tracks.ContainsKey(end))
                return null;

            // ≈сли целева€ точка не была найдена после завершени€ цикла, вызвать исключение
            throw new Exception("Error FindPath");
        }
    }
}
