using System;

namespace Geometry2D.Objects
{

    public class Point
    {
        private double _x;
        private double _y;

        #region Constructors
        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }
        #endregion

        #region Comparasion
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var temp = obj as Point;
            if ((object)temp == null) return false;
            return Destination(this, temp) < Constants.EPS;
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !point1.Equals(point2);
        }
        #endregion

        #region Properties
        public double X { get => _x; }

        public double Y { get => _y; }
        #endregion

        #region Non-static functions
        public Vector ToVector() => new Vector(this);
        #endregion

        #region Static functions
        public static double Destination(Point point1, Point point2) =>
            Math.Sqrt((point2._x - point1._x) * (point2._x - point1._x) + (point2._y - point1._y) * (point2._y - point1._y));

        public static Point Origin() => new Point(0, 0);
        #endregion





    }

}
