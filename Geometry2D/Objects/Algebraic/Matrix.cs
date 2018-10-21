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
    }
}
