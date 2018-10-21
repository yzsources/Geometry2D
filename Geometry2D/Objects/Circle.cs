namespace Geometry2D.Objects
{
    public class Circle
    {
        private Point _center;
        private double _radius;
        public Circle(Point center, double radius)
        {
            _center = new Point(center.X, center.Y);
            _radius = radius;
        }
        public Point Center { get => _center; }
        public double Radius { get => _radius; }
    }
}
