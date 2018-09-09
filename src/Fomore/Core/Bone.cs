namespace Core
{
    public class Bone
    {
        public object Tracker { get; }
        public float Density { get; set; }
        public Joint FirstJoint { get; set; }
        public Joint SecondJoint { get; set; }

        public Bone(Joint firstJoint, Joint secondJoint) : this(new object())
        {
            FirstJoint = firstJoint;
            SecondJoint = secondJoint;
        }

        private Bone(object tracker)
        {
            Tracker = tracker;
        }

        public Bone Clone()
        {
            return new Bone(Tracker) { Density = Density, FirstJoint = FirstJoint, SecondJoint = SecondJoint };
        }
    }
}