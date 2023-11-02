using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
    public class DungeonTask
    {
        // Метод для объединения путей и поиска самого короткого пути
        public static List<Point> GetPath(IEnumerable<(SinglyLinkedList<Point> path1, SinglyLinkedList<Point> path2)> paths)
        {
            if (!paths.Any() || paths.First().path2 == null)
            {
                return null;
            }

            // Находим самый короткий путь среди предоставленных
            var shortestPath = paths.Aggregate((min, current) =>
                (current.path1.Length + current.path2.Length) < (min.path1.Length + min.path2.Length) ? current : min);

            // Объединяем и возвращаем самый короткий путь
            return shortestPath.path1.Reverse().Concat(shortestPath.path2.Reverse()).ToList();
        }

        public static MoveDirection[] GetMoveDirectionArray(List<Point> points)
        {
            var directions = new List<MoveDirection>();

            for (var i = 1; i < points.Count; i++)
            {
                if (points[i].X - points[i - 1].X == 1)
                    directions.Add(MoveDirection.Right);
                else if (points[i].X - points[i - 1].X == -1)
                    directions.Add(MoveDirection.Left);
                else if (points[i].Y - points[i - 1].Y == 1)
                    directions.Add(MoveDirection.Down);
                else if (points[i].Y - points[i - 1].Y == -1)
                    directions.Add(MoveDirection.Up);
            }

            return directions.ToArray();
        }

        public static MoveDirection[] FindShortestPath(Map map)
        {
            // Находим путь от начальной точки до выхода из лабиринта
            var startToExitPath = BfsTask.FindPaths(map, map.InitialPosition, new Point[] { map.Exit }).FirstOrDefault();

            if (startToExitPath != null)
            {
                // Если на пути есть сундуки, проверяем, проходит ли путь через них
                if (map.Chests.Any(chestPoint => startToExitPath.Contains(chestPoint)))
                    return GetMoveDirectionArray(startToExitPath.Reverse().ToList());

                // Находим пути от начальной точки до всех сундуков
                var startToChestPaths = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);

                // Составляем пары путей от сундуков до выхода
                var mergedPaths = startToChestPaths.Select(single =>
                    (single, BfsTask.FindPaths(map, single.Value, new[] { map.Exit }).FirstOrDefault()));

                // Находим путь, объединяющий самые короткие пути к сундукам и от сундуков до выхода
                var resultPath = GetPath(mergedPaths);

                if (resultPath == null)
                    return GetMoveDirectionArray(startToExitPath.Reverse().ToList());

                return GetMoveDirectionArray(resultPath);
            }

            return new MoveDirection[0]; // Возвращаем пустой массив, если путь не найден
        }
    }
}
