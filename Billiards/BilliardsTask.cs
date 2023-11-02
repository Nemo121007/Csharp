using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">Угол направления движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns></returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {

            double wallCorner = (Math.PI / 2) + wallInclinationRadians;
            double ballCorner = Math.PI + directionRadians;

            double corner = ballCorner + 2 * (wallCorner - ballCorner);

            corner %= 2 * Math.PI;
            if (corner > Math.PI)
            {
                corner -= 2 * Math.PI;
            }

            return corner;
        }
    }
}