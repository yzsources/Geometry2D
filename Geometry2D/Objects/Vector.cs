using System;

namespace Geometry2D.Objects
{
    public class Vector
    {
        private double _x;
        private double _y;

        #region Constructors
        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public Vector(Point start, Point end)
        {
            _x = end.X - start.X;
            _y = end.Y - start.Y;
        }

        public Vector(Point end)
        {
            _x = end.X;
            _y = end.Y;
        }
        #endregion

        #region Comparasion
        public bool IfZero() => Norm <= Constants.EPS;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var temp = obj as Vector;
            if ((object)temp == null) return false;
            return (temp - this).IfZero();
        }

        public override int GetHashCode()
        {
            return _x.GetHashCode() ^ _y.GetHashCode();
        }

        public static bool operator ==(Vector vector1, Vector vector2)
        {
            return vector1.Equals(vector2);
        }

        public static bool operator !=(Vector vector1, Vector vector2)
        {
            return !vector1.Equals(vector2);
        }
        #endregion

        #region Properties
        public double X { get => _x; }

        public double Y { get => _y; }

        public double Norm { get => Math.Sqrt(_x * _x + _y * _y); }
        #endregion

        #region Linear operations
        public static Vector operator +(Vector vector1, Vector vector2) =>
            new Vector(vector1._x + vector2._x, vector1._y + vector2._y);

        public static Vector operator -(Vector vector1, Vector vector2) =>
            new Vector(vector1._x - vector2._x, vector1._y + vector2._y);

        public static Vector operator -(Vector vector) =>
            new Vector(-vector._x, -vector._y);

        public static Vector operator *(double coefficient, Vector vector) =>
            new Vector(coefficient * vector._x, coefficient * vector._y);

        public static double operator *(Vector vector1, Vector vector2) =>
            vector1._x * vector2._x + vector1._y * vector2._y;
        #endregion

        #region Non-static functions
        public void Norming()
        {
            var CurrentNorm = Norm;
            if (CurrentNorm > Constants.EPS)
            {
                _x /= CurrentNorm;
                _y /= CurrentNorm;
            }
        }

        public Vector Normed()
        {
            var result = new Vector(_x, _y);
            result.Norming();
            return result;
        }
        #endregion

        #region Static functions
        public static double ColinearityProduct(Vector vector1, Vector vector2) =>
            vector1._x * vector2._y - vector1._y * vector2._x;
        #endregion

    }

}
