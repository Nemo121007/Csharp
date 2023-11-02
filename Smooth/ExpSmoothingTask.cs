using System.Collections.Generic;

namespace yield;

public static class ExpSmoothingTask
{
	public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
	{
		//Fix me!
		DataPoint previousPoint = null;
		foreach(var point in data)
		{
			if (previousPoint != null)
			{
				var s = alpha * point.OriginalY + (1 - alpha) * previousPoint.ExpSmoothedY;
				previousPoint = point.WithExpSmoothedY(s);
				yield return previousPoint;
			}
			else
			{
				var p = point.OriginalY;
				previousPoint = point.WithExpSmoothedY(p);
				yield return previousPoint;
			}
		}
	}
}
