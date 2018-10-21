using System;

namespace Geometry2D
{
    public static class Constants
    {
        public static double EPS = 0.00001;
        public static int Sign(double number) =>
            (number > EPS) ? 1 : ((number<-EPS)?-1:0);
        public static double AngleByCosSin(double cosOfAngle, double sinOfAngle)
        {
            var reducedCosOfAngle = cosOfAngle > 1 ? 1 : cosOfAngle < -1 ? -1 : cosOfAngle;
            var reducedSinOfAngle = sinOfAngle > 1 ? 1 : sinOfAngle < -1 ? -1 : sinOfAngle;
            return Sign(reducedSinOfAngle) * Math.Acos(reducedCosOfAngle);
        }
    }
}
