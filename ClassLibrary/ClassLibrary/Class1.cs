using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;
    }

    public class Geometry
    {
        public static double GetLength(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static Vector Add(Vector a, Vector b)
        {
            return new Vector
            {
                X = a.X + b.X,
                Y = a.Y + b.Y
            };
        }

        public static double GetLength(Vector a, Vector b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            double yfunc = (vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y) / (segment.End.X - segment.Begin.X) + segment.Begin.Y;
            if ( yfunc == vector.Y && ((yfunc >= segment.Begin.Y && yfunc <= segment.End.Y) || (yfunc <= segment.Begin.Y && yfunc >= segment.End.Y)))
                return true;
            return false;
        }
    }
}