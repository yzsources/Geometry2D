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
        public double NormalA { get => -Math.Sign(_c) * _a / OrthogonalVector().Norm; }
        public double NormalB { get => -Math.Sign(_c) * _b / OrthogonalVector().Norm; }
        public double NormalC { get => -Math.Sign(_c) * _c / OrthogonalVector().Norm; }
        #endregion

        #region Non-static functions
        public Vector OrthogonalVector() =>
          new Vector(_a, _b);

        public Vector DirectingVector() =>
            new Vector(_b, -_a);

        public Vector NormalVector() =>
            new Vector(NormalA, NormalB);

        public double Value(double x, double y) =>
            _a * x + _b * y + _c;

        public double Value(Point point) => Value(point.X, point.Y);

        public double NormalValue(double x, double y) =>
            NormalA * x + NormalB * y + NormalC;

        public double NormalValue(Point point) => Value(point.X, point.Y);
        #endregion

        #region Static functions
        public static bool IfParallel(Line line1, Line line2) =>
            Math.Abs(Vector.Determinant(line1.DirectingVector(), line2.DirectingVector())) < Constants.EPS;
        #endregion


    }
}
