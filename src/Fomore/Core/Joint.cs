namespace Core
{
    public class Joint
    {
        public Vector2 Position { get; set; }

        public Joint Clone()
        {
            return new Joint {Position = new Vector2(Position.X, Position.Y)};
        }
    }
}