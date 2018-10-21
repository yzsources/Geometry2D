using System;

namespace Geometry2D.Objects.Algebraic
{
    public class Matrix
    {
        private double[,] _components = new double[2, 2];

        #region Constructors
        public Matrix(double line1Column1, double line1Column2, double line2Column1, double line2Column2)
        {
            _components[0, 0] = line1Column1;
            _components[0, 1] = line1Column2;
            _components[1, 0] = line2Column1;
            _components[1, 1] = line2Column2;
        }

        public Matrix(Vector vector1, Vector vector2, bool asColumns = true)
        {
            if (asColumns)
            {
                _components[0, 0] = vector1.X;
                _components[0, 1] = vector2.X;
                _components[1, 0] = vector1.Y;
                _components[1, 1] = vector2.Y;
                return;
            }
            _components[0, 0] = vector1.X;
            _components[0, 1] = vector1.Y;
            _components[1, 0] = vector2.X;
            _components[1, 1] = vector2.Y;
        }
        #endregion

        #region Properties and indexators
        public double this[int line, int column]
        {
            get
            {
                if (line < 0 || line > 1 || column < 0 || column > 1)
                    throw new IndexOutOfRangeException();

                return _components[line, column];
            }

            set => _components[line, column] = value;
        }

        public double Determinant { get => this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0]; }

        public bool Invertible { get => Math.Abs(Determinant) < Constants.EPS; }

        public Vector[] Lines => new Vector[2]
        {
            new Vector(this[0, 0], this[0, 1]),
            new Vector(this[1, 0], this[1, 1])
        };

        public Vector[] Columns => new Vector[2]
        {
            new Vector(this[0, 0], this[1, 0]),
            new Vector(this[0, 1], this[1, 1])
        };
        #endregion




    }
}
