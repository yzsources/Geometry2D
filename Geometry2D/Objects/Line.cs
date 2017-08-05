using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry2D.Objects
{
    public class Line
    {
        private double _a;
        private double _b;
        private double _c;
        public Line(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }
        public Line(Point pointOnLine, Vector baseVector, bool VectorIsOrthogonal = true)
        {
        }

        public double A { get => _a; }
        public double B { get => _b; }
        public double C { get => _c; }
    }
}
