using System;

namespace Core
{
    [Serializable]
    public class Joint
    {
        public object Tracker { get; }

        public Vector2 Position { get; set; }

        public Joint()
        {
            Tracker = new object();
        }

        private Joint(object tracker)
        {
            Tracker = tracker;
        }

        public Joint Clone()
        {
            return new Joint(Tracker) { Position = new Vector2(Position.X, Position.Y) };
        }

        protected bool Equals(Joint other) => Position.Equals(other.Position);

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Joint)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() => Position.GetHashCode();
    }
}