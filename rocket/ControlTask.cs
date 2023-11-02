using System;

namespace func_rocket;

public class ControlTask
{
	public static Turn ControlRocket(Rocket rocket, Vector target)
	{
        Vector targetRocket = rocket.Location - target;
        Vector direction = new Vector(1, 0).Rotate(rocket.Direction);
		Vector move = direction + rocket.Velocity.Normalize();
		double angle = Math.Acos((move.X * targetRocket.X + move.Y * targetRocket.Y) / (move.Length * targetRocket.Length));

		if (angle <= 1e-2)
			return Turn.None;

		Vector moveRight = move.Rotate(angle / 2);
		double angleRight = Math.Acos((moveRight.X * targetRocket.X + moveRight.Y * targetRocket.Y)
										/ (moveRight.Length * targetRocket.Length));

        Vector moveLeft = move.Rotate(angle / -2);
        double angleLeft = Math.Acos((moveLeft.X * targetRocket.X + moveLeft.Y * targetRocket.Y)
                                        / (moveLeft.Length * targetRocket.Length));

		if (angleRight < angleLeft)
			return Turn.Left;
		else
			return Turn.Right;
	}
}