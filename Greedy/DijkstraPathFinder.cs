using System;
using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;
using Point = Greedy.Architecture.Point;

namespace Greedy
{
    public class DijkstraPathFinder
    {
        // Метод для поиска путей с использованием алгоритма Дейкстры
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            // Инициализация структур данных
            var chests = new HashSet<Point>(targets);            // Множество целевых вершин
            Dictionary<Point, (Point?, int)> track;              // Словарь для отслеживания данных о вершинах
            var priorityQueue = new PriorityQueue<Point, int>(); // Приоритетная очередь для выбора вершины с наименьшей стоимостью

            // Инициализация стартовой вершины
            track = new Dictionary<Point, (Point?, int)>();
            track[start] = (null, 0);
            priorityQueue.Enqueue(start, 0);

            while (!(priorityQueue.Count == 0))
            {
                var current = priorityQueue.Dequeue();

                // Если текущая вершина является целью, вернуть путь
                if (chests.Contains(current))
                {
                    var path = new List<Point>();
                    Point? currentPoint = current;

                    // Восстанавить путь от целевой вершины к начальной
                    while (currentPoint != null)
                    {
                        path.Add(currentPoint.Value);
                        currentPoint = track[currentPoint.Value].Item1;
                    }

                    path.Reverse(); // Разворнуть путь
                    PathWithCost pathResult = new PathWithCost(track[current].Item2, path.ToArray());

                    yield return pathResult;

                    chests.Remove(current); // Удалить цель после её нахождения
                }

                // Смежные ячейки
                var incidentNodes = new Point[]
                    {
                        new Point(current.X, current.Y + 1),
                        new Point(current.X, current.Y - 1),
                        new Point(current.X + 1, current.Y),
                        new Point(current.X - 1, current.Y)
                    }
                    .Where(point => state.InsideMap(point) && !state.IsWallAt(point));


                foreach (var incidentNode in incidentNodes)
                {
                    var currentLong = track[current].Item2 + state.CellCost[incidentNode.X, incidentNode.Y];

                    // Если вершина ещё не посещена или найден более короткий путь
                    if (!track.ContainsKey(incidentNode) || track[incidentNode].Item2 > currentLong)
                    {
                        track[incidentNode] = (current, currentLong);
                        priorityQueue.Enqueue(incidentNode, currentLong);
                    }
                }
            }
        }
    }
}
