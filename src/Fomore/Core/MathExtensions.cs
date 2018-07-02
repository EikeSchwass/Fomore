using static System.Math;

namespace Core
{
    public static class MathExtensions
    {
        public const double DegreesToRadiansFactor = PI / 180;
        public const double RadiansToDegreesFactor = 180 / PI;

        public static Vector2 RotateAround(this Vector2 original, Vector2 pivot, double angleInRadians)
        {
            double x = original.X - pivot.X;
            double y = original.Y - pivot.Y;
            double newX = x * Cos(angleInRadians) - y * Sin(angleInRadians);
            double newY = y * Cos(angleInRadians) + x * Sin(angleInRadians);
            newX += pivot.X;
            newY += pivot.Y;
            return new Vector2(newX, newY);
        }
    }
}