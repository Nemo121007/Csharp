using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            if (width - 1 > height)
                BypassWidth(robot, width, height);
            else if (height - 1 > width)
                BypassHeight(robot, width, height);
            else BypassDiagonal(robot, width, height);
		}

        public static void BypassWidth(Robot robot, int width, int height)
        {
            int step = (int)Math.Round((width - 2.0) / (height - 2.0) + 3.0);
            int startHeight = 1;
            while (true)
            {
                MoveRight(robot, step);
                startHeight ++;
                if (startHeight == height - 1)
                    break;
                MoveDown(robot, 4);
            }
        }

        public static void BypassHeight(Robot robot, int width, int height)
        {
            int step = (int)Math.Round((height - 2.0) / (width - 2.0) + 3.0);
            int startHeight = 1;
            while (true)
            {
                MoveDown(robot, step);
                startHeight += 2;
                if (startHeight == height - 2)
                    break;
                MoveRight(robot, 4);
            }

        }

        public static void BypassDiagonal(Robot robot, int width, int height)
        {
            int startHeight = 1;
            while (true)
            {
                MoveDown(robot, 4);
                startHeight ++;
                if (startHeight == height - 2)
                    break;
                MoveRight(robot, 4);
            }
        }

        public static void MoveRight(Robot robot, int width)
        {
            for (int i = 0; i < width - 3; i++)
                robot.MoveTo(Direction.Right);
        }
        public static void MoveDown(Robot robot, int height)
        {
            for (int i = 0; i < height - 3; i++)
                robot.MoveTo(Direction.Down);
        }
    }
}