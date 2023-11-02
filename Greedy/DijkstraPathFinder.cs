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
        // ����� ��� ������ ����� � �������������� ��������� ��������
        public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start, IEnumerable<Point> targets)
        {
            // ������������� �������� ������
            var chests = new HashSet<Point>(targets);            // ��������� ������� ������
            Dictionary<Point, (Point?, int)> track;              // ������� ��� ������������ ������ � ��������
            var priorityQueue = new PriorityQueue<Point, int>(); // ������������ ������� ��� ������ ������� � ���������� ����������

            // ������������� ��������� �������
            track = new Dictionary<Point, (Point?, int)>();
            track[start] = (null, 0);
            priorityQueue.Enqueue(start, 0);

            while (!(priorityQueue.Count == 0))
            {
                var current = priorityQueue.Dequeue();

                // ���� ������� ������� �������� �����, ������� ����
                if (chests.Contains(current))
                {
                    var path = new List<Point>();
                    Point? currentPoint = current;

                    // ������������ ���� �� ������� ������� � ���������
                    while (currentPoint != null)
                    {
                        path.Add(currentPoint.Value);
                        currentPoint = track[currentPoint.Value].Item1;
                    }

                    path.Reverse(); // ���������� ����
                    PathWithCost pathResult = new PathWithCost(track[current].Item2, path.ToArray());

                    yield return pathResult;

                    chests.Remove(current); // ������� ���� ����� � ����������
                }

                // ������� ������
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

                    // ���� ������� ��� �� �������� ��� ������ ����� �������� ����
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
