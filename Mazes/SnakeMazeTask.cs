namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int startHeight = 1;
            while(true)
            {
                MoveRight(robot, width);
                MoveDown(robot, 5);
                startHeight += 2;
                MoveLeft(robot, width);
                if (startHeight == height - 2)
                    break;
                MoveDown(robot, 5);
                startHeight += 2;
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

        public static void MoveLeft(Robot robot, int width)
        {
            for (int i = 0; i < width - 3; i++)
                robot.MoveTo(Direction.Left);
        }
    }
}