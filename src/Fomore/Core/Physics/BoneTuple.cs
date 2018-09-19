namespace Core.Physics
{
    internal class BoneTuple
    {
        public Bone Bone1 { get; }
        public Bone Bone2 { get; }

        public BoneTuple(Bone bone1, Bone bone2)
        {
            Bone1 = bone1;
            Bone2 = bone2;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is BoneTuple other)
            {
                if (other.Bone1 == Bone1 && other.Bone2 == Bone2)
                    return true;
                if (other.Bone2 == Bone1 && other.Bone1 == Bone2)
                    return true;
                return false;
            }
            return ReferenceEquals(this, obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Bone1 != null ? Bone1.GetHashCode() : 0) * 397) ^ (Bone2 != null ? Bone2.GetHashCode() : 0);
            }
        }
    }
}