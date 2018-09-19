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

        public static Vector2 Normalize(this Vector2 original)
        {
            double length = original.Length;
            return new Vector2(original.X / length, original.Y / length);
        }

        public static int GetQuadrant(this Vector2 vector)
        {
            if (vector.X > 0 && vector.Y < 0)
                return 1;
            if (vector.X < 0 && vector.Y < 0)
                return 2;
            if (vector.X < 0 && vector.Y > 0)
                return 3;
            if (vector.X > 0 && vector.Y > 0)
                return 4;
            return 1;
        }

        public static double GetAngleTowards(this Vector2 from, Vector2 to)
        {
            var translated = to - from;
            if (Abs(translated.X) < 0.0001)
                return translated.Y < 0 ? 3 * PI / 2 : PI / 2;
            if (Abs(translated.Y) < 0.0001)
                return translated.X < 0 ? PI : 0;

            double angle = Atan(-translated.Y / translated.X);
            int quadrant = translated.GetQuadrant();
            switch (quadrant)
            {
                case 1: break;
                case 2:
                    angle += PI;
                    break;
                case 3:
                    angle += PI;
                    break;
                case 4:
                    angle += 2 * PI;
                    break;
            }

            angle = 2 * PI - angle;
            while (angle > 2 * PI)
                angle -= 2 * PI;
            return angle;
        }

        public static Microsoft.Xna.Framework.Vector2 ToXna(this Vector2 vector)
        {
            return new Microsoft.Xna.Framework.Vector2((float)vector.X, (float)vector.Y);
        }

        public static Vector2 ToWindows(this Microsoft.Xna.Framework.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
    }
}