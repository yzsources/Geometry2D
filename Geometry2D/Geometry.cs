﻿using Geometry2D.Objects;
using System;
using System.Collections.Generic;

namespace Geometry2D
{
    public static class Geometry
    {
        #region Destinations
        public static double DestinationTwoPoints(Point point1, Point point2) =>
            Point.Destination(point1, point2);
        public static double DestinationTwoPointsOnCircle(Point point1, Point point2, Circle circle)
        {
            if (circle.Radius <= Constants.EPS)
                return 0;
            return Math.Abs(AngleTwoPointsOnCircle(point1, point2, circle)) * circle.Radius;
        }
        public static double DestinationPointLine(Point point, Line line) => Math.Abs(line.NormalValue(point));
        public static double DestinationTwoParallelLines(Line line1, Line line2)
        {
            if (!Colinear(line1.NormalVector(), line2.NormalVector()))
                throw new ArgumentException("Lines are not parallel");
            return Math.Abs(line1.NormalValue(0,0)-line2.NormalValue(0,0));
        }
        
        #endregion

        #region CheckingMethods
        public static bool Orthogonal(Vector vector1, Vector vector2) =>
            Math.Abs(vector1 * vector2) <= Constants.EPS;
        public static bool Colinear(Vector vector1, Vector vector2) =>
            Math.Abs(Vector.Determinant(vector1, vector2)) <= Constants.EPS;
        public static bool PointOnLine(Point point, Line line) =>
            Math.Abs(line.Value(point)) < Constants.EPS;
        public static bool PointOnCircle(Point point, Circle circle) =>
            DestinationTwoPoints(point, circle.Center) <= Constants.EPS;
        public static bool Colinear(Point point1, Point point2, Point point3) =>
             Colinear(new Vector(point1, point2), new Vector(point2, point3));

        public static bool Parallel(Line line1, Line line2) => 
            Math.Abs(Vector.Determinant(line1.DirectingVector(), line2.DirectingVector())) < Constants.EPS;
        #endregion

        #region Intersections
        public static Point IntersectionTwoLines(Line line1, Line line2)
        {
            if (Parallel(line1, line2))
                throw new ArgumentException("Lines are paralel");
            return new Point(
                (line2.C * line1.B - line1.C * line2.B) / (line1.A * line2.B - line1.B * line2.A),
                (line2.A * line1.C - line1.A * line2.C) / (line1.A * line2.B - line1.B * line2.A)
                );
        }
        public static List<Point> IntersectionCircleLine(Circle circle, Line line)
        {
            var result = new List<Point>();
            var perpendicularBase = ProjectPointToLine(line, circle.Center, true);
            var destinationCenterLine = DestinationTwoPoints(circle.Center, perpendicularBase);
            if (destinationCenterLine > Constants.EPS + circle.Radius)
                return result;
            if (destinationCenterLine >= -Constants.EPS + circle.Radius)
            {
                result.Add(perpendicularBase);
                return result;
            }
            var chordHalfLengthSqr = circle.Radius * circle.Radius - line.C * line.C / (line.A * line.A + line.B * line.B);
            var coefficient = Math.Sqrt(chordHalfLengthSqr / (line.A * line.A + line.B * line.B));
            result.Add(new Point(
                circle.Center.X + line.B * coefficient,
                circle.Center.Y - line.A * coefficient
                ));
            result.Add(new Point(
                circle.Center.X - line.B * coefficient,
                circle.Center.Y + line.A * coefficient
                ));
            return result;
        }
        public static List<Point> IntersectionTwoCircles(Circle circle1, Circle circle2)
        {
            if (circle1.Center == circle2.Center) return new List<Point>();
            return IntersectionCircleLine(circle1, RadicalAxis(circle1, circle2));
        }
        #endregion

        #region WorkingWithLines
        public static Line LineByTwoPoints(Point point1, Point point2)
        {
            if (point1 == point2)
                throw new ArgumentException("Points cannot be equal");
            return new Line(
                point2.Y - point1.Y,
                point1.X - point2.X,
                (point1.Y - point2.Y) * point1.X + (point2.X - point1.X) * point1.Y
                );
        }
        public static Line ParallelLine(Line line, Point point, bool returnCopy = false)
        {
            if (PointOnLine(point, line) && !returnCopy)
                throw new ArgumentException("Point lies on line");
            return new Line(line.A, line.B, -line.A * point.X - line.B * point.Y);
        }
        public static Line PerpendicularLineToLine(Line line, Point point) =>
            new Line(line.B, -line.A, -line.B * point.X + line.A * point.Y);
        
        #endregion

        #region WorkingWithLineSegments
        public static Point Midpoint(Point point1, Point point2, bool returnCopy = false)
        {
            if (point1 == point2 && !returnCopy)
                throw new ArgumentException("Points cannot be equal");
            return new Point((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }
        public static Line PerpendicularBisector(Point point1, Point point2)
        {
            if (point1 == point2)
                throw new ArgumentException("Points cannot be equal");
            return new Line(Midpoint(point1, point2), new Vector(point1, point2));
        }
        #endregion

        #region WorkingWithCircles
        public static Circle CircleByThreePoints(Point point1, Point point2, Point point3)
        {
            if (Colinear(point1, point2, point3))
                throw new ArgumentException("Points are colinear");
            var center = IntersectionTwoLines(PerpendicularBisector(point1, point2), PerpendicularBisector(point2, point3));
            var radius = DestinationTwoPoints(point1, center);
            return new Circle(center, radius);
        }
        public static double PointPower(Point point, Circle circle)
        {
            var destination = DestinationTwoPoints(point, circle.Center);
            return destination * destination - circle.Radius * circle.Radius;
        }
        public static double PointAngleOnCircle(Point point, Circle circle)
        {
            if (!PointOnCircle(point, circle))
                throw new ArgumentException("Point does not lie on circle");
            if (circle.Radius <= Constants.EPS)
                throw new ArgumentException("Circle is too small");
            return Constants.AngleByCosSin(
                (point.X - circle.Center.X) / circle.Radius,
                (point.Y - circle.Center.Y) / circle.Radius
                );
        }
        public static double AngleTwoPointsOnCircle(Point point1, Point point2, Circle circle) =>
            PointAngleOnCircle(point2, circle) - PointAngleOnCircle(point1, circle);
        public static Line RadicalAxis(Circle circle1, Circle circle2)
        {
            if (circle1.Center == circle2.Center)
                throw new ArgumentException("Circles must not be concentric");
            return new Line(
                2 * (circle2.Center.X - circle1.Center.X),
                2 * (circle2.Center.Y - circle1.Center.Y),
                PointPower(Point.Origin(), circle1) - PointPower(Point.Origin(), circle2)
                );
        }
        #endregion

        #region ProjectionMethods
        public static Point ProjectPointToLine(Line line, Point point, bool returnCopy = false)
        {
            if (PointOnLine(point, line) && !returnCopy)
                throw new ArgumentException("Point lies on line");
            return IntersectionTwoLines(line, PerpendicularLineToLine(line, point));
        }
        #endregion

        

        
        
        
        
        

        
    }

}
