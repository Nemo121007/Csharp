using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
	static readonly Physics standardPhysics = new();

	public static IEnumerable<Level> CreateLevels()
	{
		yield return GetLevel("Zero");
		yield return GetLevel("Heavy", (size, v) => new Vector(0, 0.9));
		yield return GetLevel("Up", UpTarget, (size, v) => new Vector(0, -300.0 / (size.Y - v.Y + 300.0)));
        yield return GetLevel("WhiteHole", (size, v) => GetGravityInHoles(StandartTarget, v, 140));
        yield return GetLevel("BlackHole", (size, v) => GetGravityInHoles(v, BlackHoleLocation(), 300));
        yield return GetLevel("BlackAndWhite", (size, v) =>
        {
            var blackHole = GetGravityInHoles(v, BlackHoleLocation(), 300);
            var whiteHole = GetGravityInHoles(StandartTarget, v, 140);
            return new Vector((blackHole.X + whiteHole.X) / 2, (blackHole.Y + whiteHole.Y) / 2);
        });
	}

    private static Vector BlackHoleLocation()
    {
        return new Vector((StandartTarget.X - standarRocket.Location.X) / 2 + standarRocket.Location.X,
                        (StandartTarget.Y - standarRocket.Location.Y) / 2 + standarRocket.Location.Y);
    }

	private static Level GetLevel(string name)
	{
        return new Level(name, standarRocket, StandartTarget, (size, v) => Vector.Zero, standardPhysics);
    }

	private static Level GetLevel(string name, Gravity gravity)
	{
        return new Level(name, standarRocket, StandartTarget, gravity, standardPhysics);
    }

    private static Level GetLevel(string name, Vector target,  Gravity gravity)
    {
        return new Level(name, standarRocket, target, gravity, standardPhysics);
    }

	private static Rocket standarRocket = new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);

	private static Vector StandartTarget = new Vector(600, 200);
	private static Vector UpTarget = new Vector(700, 500);

    private static Vector GetGravityInHoles(Vector firstObject, Vector secondObject, double k)
    {
        double x, y;
        double d = Math.Sqrt((secondObject.X - firstObject.X) * (secondObject.X - firstObject.X) + (secondObject.Y - firstObject.Y) * (secondObject.Y - firstObject.Y));
        double direction = k * d / (d * d + 1);
        if (secondObject.X == firstObject.X && secondObject.Y == firstObject.Y)
        {
            x = secondObject.X * direction;
            y = secondObject.Y * direction;
        }
        else
        {
            x = (secondObject.X - firstObject.X) / d * direction;
            y = (secondObject.Y - firstObject.Y) / d * direction;
        }
        return new Vector(x, y);
    }
}