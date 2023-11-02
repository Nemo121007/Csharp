using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            float x;
            float y;

            x = (float)Math.Cos(shoulder) * Manipulator.UpperArm;
            y = (float)Math.Sin(shoulder) * Manipulator.UpperArm;
            
            var elbowPos = new PointF(x, y);

            x += (float)Math.Cos(elbow + shoulder - Math.PI) * Manipulator.Forearm;
            y += (float)Math.Sin(elbow + shoulder - Math.PI) * Manipulator.Forearm;

            var wristPos = new PointF(x, y);
            
            x += (float)Math.Cos(wrist + shoulder + elbow) * Manipulator.Palm;
            y += (float)Math.Sin(wrist + shoulder + elbow) * Manipulator.Palm;

            var palmEndPos = new PointF(x, y);

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI, Math.PI, 0, Manipulator.UpperArm + Manipulator.Forearm + Manipulator.Palm)]
        [TestCase(0, Math.PI, Math.PI, Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm, 0)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]

        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
        }
    }
}