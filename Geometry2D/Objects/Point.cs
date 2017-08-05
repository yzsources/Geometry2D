namespace Geometry2D.Objects
{

    public class Point
    {
        private double _x;
        private double _y;
        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double X { get => _x; }
        public double Y { get => _y; }
    }

}
