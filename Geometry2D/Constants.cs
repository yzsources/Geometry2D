namespace Geometry2D
{
    public static class Constants
    {
        public static double EPS = 0.00001;
        public static int Sign(double number) =>
            (number > EPS) ? 1 : ((number<-EPS)?-1:0);
    }
}
