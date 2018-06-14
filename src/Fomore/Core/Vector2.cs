namespace Core
{
    // TODO mark this as obsolete
    //[Obsolete("This class is only a placeholder for the actual Vector2 class of FarseerPhysics")]
    public struct Vector2
    {
        public double X { get; }
        public double Y { get; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 operator *(Vector2 v1, double scalar) => new Vector2(v1.X * scalar, v1.Y * scalar);

        /// <inheritdoc />
        public override string ToString() => $"({X}|{Y})";
    }
}