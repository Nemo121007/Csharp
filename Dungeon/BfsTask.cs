using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon
{
    public class BfsTask
    {
        // ����� ��� ������ ����� �� ��������� ����� � ����� (��������)
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            // ������� ��� �������� ����� �� ��������� ����� � ������ ������
            var tracks = new Dictionary<Point, SinglyLinkedList<Point>>();
            // ������������� ���� �� ��������� ����� � ����� ����
            tracks[start] = new SinglyLinkedList<Point>(start);

            var queue = new Queue<SinglyLinkedList<Point>>();
            queue.Enqueue(tracks[start]);

            // ������� �������� ��� ���������� ����� � ������� �� ���
            foreach (var chest in chests)
            {
                // ���� ���� � ������� ��� ����������, ������� ���
                if (tracks.ContainsKey(chest))
                {
                    yield return tracks[chest];
                    continue;
                }

                // � ��������� ������, ��������� ����� ���� � ������� ���, ���� ������
                var path = FindPath(tracks, queue, map, chest);
                if (path != null)
                    yield return path;
            }
        }

        // ��������������� ����� ��� ������ ����
        static SinglyLinkedList<Point> FindPath(Dictionary<Point, SinglyLinkedList<Point>> tracks,
                                                Queue<SinglyLinkedList<Point>> queue, Map map, Point end)
        {
            while (queue.Count != 0)
            {
                // ���������� �������� ���� �� �������
                var node = queue.Dequeue();
                // ��������� ������� �����, ������� ����� ������� �� �������� ����
                var incidentNodes =
                    Walker.PossibleDirections
                            .Select(direction => node.Value + direction)
                            .Where(point => map.InBounds(point) && map.Dungeon[point.X, point.Y] != MapCell.Wall);

                // ������� ������� �����
                foreach (var nextNode in incidentNodes)
                {
                    // ���� ���� �� ���� ��� ����������, ���������� ���
                    if (tracks.ContainsKey(nextNode))
                        continue;
                    // �������� ������ ���� � ���������� ��� � ������� � �������
                    tracks[nextNode] = new SinglyLinkedList<Point>(nextNode, node);
                    queue.Enqueue(tracks[nextNode]);
                }

                // ���� ���� �� ������� ����� (�������) ������, ������� ���
                if (tracks.ContainsKey(end))
                    return tracks[end];
            }

            // ���� ���� �� ������� ����� �� ��� ������, ������� null
            if (!tracks.ContainsKey(end))
                return null;

            // ���� ������� ����� �� ���� ������� ����� ���������� �����, ������� ����������
            throw new Exception("Error FindPath");
        }
    }
}
