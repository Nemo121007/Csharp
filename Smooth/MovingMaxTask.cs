using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        //Fix me!
        Queue<double> queue = new Queue<double>();
        LinkedList<double> listMax = new LinkedList<double>();
        listMax.AddFirst(Double.MinValue);
        foreach (DataPoint point in data)
        {
            queue.Enqueue(point.OriginalY);
            if (listMax.First.Value <= point.OriginalY)
            {
                listMax.Clear();
                listMax.AddFirst(point.OriginalY);
            }
            else
            {
                while(listMax.Last.Value <= point.OriginalY)
                {
                    listMax.RemoveLast();
                }
                listMax.AddLast(point.OriginalY);
            }
            if (queue.Count > windowWidth)
            {
                if (queue.Dequeue() == listMax.First.Value)
                    listMax.RemoveFirst();
            }
            yield return point.WithMaxY(listMax.First.Value);
        }

            //}
            //var points = new LinkedList<double>();
            //var xPoints = new Queue<double>();

            //foreach (var point in data)
            //{
            //    xPoints.Enqueue(point.X);

            //    if (xPoints.Count > windowWidth && points.First.Value <= xPoints.Dequeue())
            //    {
            //        points.RemoveFirst();
            //        points.RemoveFirst();
            //    }

            //    while (points.Count != 0 && points.Last.Value < point.OriginalY)
            //    {
            //        points.RemoveLast();
            //        points.RemoveLast();
            //    }

            //    points.AddLast(point.X);
            //    points.AddLast(point.OriginalY);
            //    yield return point.WithMaxY(points.First.Next.Value);
            //}
        }
}