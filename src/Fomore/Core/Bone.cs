namespace Core
{
    public class Bone
    {
        public float Density { get; set; }
        public Joint FirstJoint { get; set; }
        public Joint SecondJoint { get; set; }

        public Bone(Joint firstJoint, Joint secondJoint)
        {
            FirstJoint = firstJoint;
            SecondJoint = secondJoint;
        }

        public Bone Clone()
        {
            return new Bone(FirstJoint, SecondJoint) { Density = Density };
        }
    }
}