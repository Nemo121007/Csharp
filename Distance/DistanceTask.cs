using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			double distanceToA = Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
			double distanceToB = Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
			double distanceAB = Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));

			// Вырожденный отрезок
			if (ax == bx && ay == by)
				return distanceToA;

			// Проверка на условие существования треугольника
			if ((distanceToB * distanceToB) + distanceAB * distanceAB >= distanceToA * distanceToA)
				if (distanceToA * distanceToA + distanceAB * distanceAB >= distanceToB * distanceToB)
					return Math.Abs(x * (by - ay) - y * (bx - ax) + bx * ay - by * ax) 
						/ Math.Sqrt((by - ay) * (by - ay) + (bx - ax) * (bx - ax));
			return Math.Min(distanceToB, distanceToA);
		}
	}
}