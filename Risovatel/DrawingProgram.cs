using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Paint
    {
        static float x, y;
        static Graphics graph;

        public static void Initialize(Graphics newGraph)
        {
            graph = newGraph;
            graph.SmoothingMode = SmoothingMode.None;
            graph.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        { x = x0; y = y0; }

        public static void MakeIt(Pen pen, double dlina, double angle)
        {
            //Делает шаг длиной dlina в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + dlina * Math.Cos(angle));
            var y1 = (float)(y + dlina * Math.Sin(angle));
            graph.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double dlina, double angle)
        {
            x = (float)(x + dlina * Math.Cos(angle));
            y = (float)(y + dlina * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int shirina, int visota, double angleMove, Graphics graph)
        {
            // angleMove пока не используется, но будет использоваться в будущем
            Paint.Initialize(graph);

            var sz = Math.Min(shirina, visota);

            var diagonalLength = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + shirina / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + visota / 2f;

            Paint.SetPosition(x0, y0);

            //Рисуем 1-ую сторону
            FirstPart(sz);

            //Рисуем 2-ую сторону
            SecondPart(sz);

            //Рисуем 3-ю сторону
            ThirdtPart(sz);

            //Рисуем 4-ую сторону
            FourthPart(sz);
        }

        public static void FirstPart(int sz)
        {
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, 0);
            Paint.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 4);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2);

            Paint.Change(sz * 0.04f, -Math.PI);
            Paint.Change(sz * 0.04f * Math.Sqrt(2), 3 * Math.PI / 4);
        }

        public static void SecondPart(int sz)
        {
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2);
            Paint.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, -Math.PI / 2 + Math.PI);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, -Math.PI / 2 + Math.PI / 2);

            Paint.Change(sz * 0.04f, -Math.PI / 2 - Math.PI);
            Paint.Change(sz * 0.04f * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        public static void ThirdtPart(int sz)
        {
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI);
            Paint.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI + Math.PI);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI + Math.PI / 2);

            Paint.Change(sz * 0.04f, Math.PI - Math.PI);
            Paint.Change(sz * 0.04f * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        public static void FourthPart(int sz)
        {
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2);
            Paint.MakeIt(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f, Math.PI / 2 + Math.PI);
            Paint.MakeIt(Pens.Yellow, sz * 0.375f - sz * 0.04f, Math.PI / 2 + Math.PI / 2);

            Paint.Change(sz * 0.04f, Math.PI / 2 - Math.PI);
            Paint.Change(sz * 0.04f * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

    }
}