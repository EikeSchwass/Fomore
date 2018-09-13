using System;

namespace Core
{
    [Serializable]
    public class Bone
    {
        public object Tracker { get; }
        public float Density { get; set; } = 0.75f;
        public Joint FirstJoint { get; set; }
        public Joint SecondJoint { get; set; }
        public float Width { get; set; } = 0.05f;

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