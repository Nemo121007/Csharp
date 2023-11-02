using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
	public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
	{
		//Fix me!
		Queue<double> queue = new Queue<double>();
		double sum = 0;
		foreach (DataPoint point in data)
		{
			queue.Enqueue(point.OriginalY);
			sum += point.OriginalY;
			if (queue.Count > windowWidth)
			{
				sum -= queue.Dequeue();
			}
			//point.AvgSmoothedY = sum / queue.Count;
			//yield return point;
			yield return point.WithAvgSmoothedY(sum / queue.Count);
		}
	}
}