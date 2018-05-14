using System;

namespace Core
{
    public struct Vector2
    {
        public float X { get; }
        public float Y { get; }
        public float Length { get; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
            Length = (float)Math.Sqrt(X * X + Y * Y);
        }

        public static Vector2 Add(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 Subtract(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 Scale(float scalar, Vector2 vector) => new Vector2(scalar * vector.X, scalar * vector.Y);

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => Add(v1, v2);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => Subtract(v1, v2);
        public static Vector2 operator *(float scalar, Vector2 vector) => Scale(scalar, vector);
    }
}