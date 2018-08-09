namespace Core
{
    public class Bone : ICloneable<Bone>
    {
        public float Density { get; set; }
        public Joint FirstJoint { get; set; }
        public Joint SecondJoint { get; set; }

        public Bone Clone()
        {
            return new Bone { Density = Density, FirstJoint = FirstJoint, SecondJoint = SecondJoint };
        }
    }
}