using Geometry2D.Objects;
using System;
using System.Collections.Generic;

namespace Geometry2D
{
    public static class Geometry
    {
        public static double DestinationTwoPoints(Point point1, Point point2) =>
            Point.Destination(point1, point2);
        public static bool IfOrthogonal(Vector vector1, Vector vector2) =>
            Math.Abs(vector1 * vector2) <= Constants.EPS;
        public static bool IfColinear(Vector vector1, Vector vector2) =>
            Math.Abs(Vector.ColinearityProduct(vector1, vector2)) <= Constants.EPS;
        public static bool IfPointInLine(Point point, Line line) =>
            Math.Abs(line.Value(point)) < Constants.EPS;
        public static Point IntersectionTwoLines(Line line1, Line line2)
        {
            if (Line.IfParallel(line1, line2))
                throw new ArgumentException("Lines are paralel");
            return new Point(
                (line2.C * line1.B - line1.C * line2.B) / (line1.A * line2.B - line1.B * line2.A),
                (line2.A * line1.C - line1.A * line2.C) / (line1.A * line2.B - line1.B * line2.A)
                );
        } 
        public static Line ParallelLine(Line line, Point point, bool returnCopy=false)
        {
            if (IfPointInLine(point,line)&&!returnCopy)
                throw new ArgumentException("Point lies on line");
            return new Line(line.A, line.B, -line.A * point.X - line.B * point.Y);
        }
        public static Line PerpendicularLineToLine(Line line, Point point) =>
            new Line(line.B, -line.A, -line.B * point.X + line.A * point.Y);
        public static Point ProjectPointToLine(Line line, Point point, bool returnCopy=false)
        {
            if (IfPointInLine(point, line)&&!returnCopy)
                throw new ArgumentException("Point lies on line");
            return IntersectionTwoLines(line, PerpendicularLineToLine(line,point));
        }
        public static Line LineByTwoPoints(Point point1, Point point2)
        {
            if (point1 == point2)
                throw new ArgumentException("Points cannot be equal");
            return new Line(
                point2.Y - point1.Y,
                point1.X - point2.X,
                (point1.Y- point2.Y) * point1.X + (point2.X- point1.X) * point1.Y
                );
        }
        public static Point Midpoint(Point point1, Point point2, bool returnCopy=false)
        {
            if (point1 == point2&&!returnCopy)
                throw new ArgumentException("Points cannot be equal");
            return new Point((point1.X+point2.X)/2,(point1.Y+point2.Y)/2);
        }
        public static Line PerpendicularBisector(Point point1, Point point2)
        {
            if (point1 == point2)
                throw new ArgumentException("Points cannot be equal");
            return new Line(Midpoint(point1, point2), new Vector(point1, point2));
        }
        public static bool IfColinear(Point point1, Point point2, Point point3) =>
             IfColinear(new Vector(point1,point2),new Vector(point2,point3));
        public static Circle CircleByThreePoints(Point point1, Point point2, Point point3)
        {
            if(IfColinear(point1, point2, point3))
                throw new ArgumentException("Points are colinear");
            var center = IntersectionTwoLines(PerpendicularBisector(point1, point2), PerpendicularBisector(point2, point3));
            var radius = DestinationTwoPoints(point1, center);
            return new Circle(center, radius);
        }
        public static List<Point> IntersectionCircleLine(Circle circle, Line line)
        {
            var result = new List<Point>();
            var perpendicularBase = ProjectPointToLine(line, circle.Center,true);
            var destinationCenterLine = DestinationTwoPoints(circle.Center,perpendicularBase);
            if (destinationCenterLine > Constants.EPS + circle.Radius)
                return result;
            if (destinationCenterLine >= -Constants.EPS + circle.Radius)
            {
                result.Add(perpendicularBase);
                return result;
            }
            var chordHalfLengthSqr = circle.Radius*circle.Radius-line.C*line.C/(line.A*line.A+line.B*line.B);
            var coefficient = Math.Sqrt(chordHalfLengthSqr / (line.A * line.A + line.B * line.B));
            result.Add(new Point(
                circle.Center.X + line.B * coefficient,
                circle.Center.Y - line.A * coefficient
                ));
            result.Add(new Point(
                circle.Center.X - line.B*coefficient,
                circle.Center.Y + line.A*coefficient
                ));
            return result;
        }
        public static double PointPower(Point point, Circle circle)
        {
            var destination = DestinationTwoPoints(point, circle.Center);
            return destination * destination - circle.Radius * circle.Radius;
        }
        public static Line RadicalAxis(Circle circle1, Circle circle2)
        {
            if (circle1.Center == circle2.Center)
                throw new ArgumentException("Circles must not be concentric");
            return new Line(
                2*(circle2.Center.X-circle1.Center.X),
                2*(circle2.Center.Y-circle1.Center.Y),
                PointPower(Point.CoordinateStartPoint(),circle1)-PointPower(Point.CoordinateStartPoint(),circle2)
                );
        }
        public static List<Point> IntersectionTwoCircles(Circle circle1, Circle circle2)
        {
            if (circle1.Center == circle2.Center) return new List<Point>();
            return IntersectionCircleLine(circle1, RadicalAxis(circle1, circle2));
        }
    }

}
