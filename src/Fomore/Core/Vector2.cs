using System;

namespace Core
{
    // TODO mark this as obsolete
    //[Obsolete("This class is only a placeholder for the actual Vector2 class of FarseerPhysics")]
    public struct Vector2
    {
        public double X { get; }
        public double Y { get; }
        public double Length { get; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
            Length = Math.Sqrt(X * X + Y * Y);
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 operator *(Vector2 v1, double scalar) => new Vector2(v1.X * scalar, v1.Y * scalar);

        public override string ToString() => $"({X}|{Y})";

        public bool Equals(Vector2 other) => X.Equals(other.X) && Y.Equals(other.Y);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 vector2 && Equals(vector2);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}