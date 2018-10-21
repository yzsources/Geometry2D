using System;

namespace Geometry2D.Objects
{
    public class Line
    {
        private double _a;
        private double _b;
        private double _c;

        #region Constructors
        public Line(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public Line(Point pointOnLine, Vector baseVector, bool VectorIsOrthogonal = true)
        {
            if (baseVector.IfZero())
                throw new ArgumentException("Cannot be equal to zero", nameof(baseVector));
            if (VectorIsOrthogonal)
            {
                _a = baseVector.X;
                _b = baseVector.Y;
            }
            else
            {
                _a = baseVector.Y;
                _b = -baseVector.X;
            }
            _c = -_a * pointOnLine.X - _b * pointOnLine.Y;
        }

        #endregion

        #region Properties
        public double A { get => _a; }
        public double B { get => _b; }
        public double C { get => _c; }
        public double NormalA { get => -Math.Sign(_c) * _a / NormalVector().Norm; }
        public double NormalB { get => -Math.Sign(_c) * _b / NormalVector().Norm; }
        public double NormalC { get => -Math.Sign(_c) * _c / NormalVector().Norm; }
        #endregion

        #region Non-static functions
        public Vector NormalVector() =>
          new Vector(_a, _b);

        public Vector DirectingVector() =>
            new Vector(_b, -_a);

        public double Value(double x, double y) =>
            _a * x + _b * y + _c;

        public double Value(Point point) => Value(point.X, point.Y);

        public double NormalValue(double x, double y) =>
            NormalA * x + NormalB * y + NormalC;

        public double NormalValue(Point point) => Value(point.X, point.Y);
        #endregion

        #region Static functions
        public static bool IfParallel(Line line1, Line line2) =>
            Math.Abs(line1._a * line2._b - line1._b * line2._a) < Constants.EPS;
        #endregion


    }
}
