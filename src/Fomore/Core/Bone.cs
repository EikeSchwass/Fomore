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
        public string Name { get; set; } = "Unnamed Bone";
        public ConnectorInformation ConnectorInformation { get; private set; }

        public Vector2 Position => FirstJoint.Position + (SecondJoint.Position - FirstJoint.Position) * 0.5;

        public Bone(Joint firstJoint, Joint secondJoint) : this(new object())
        {
            FirstJoint = firstJoint;
            SecondJoint = secondJoint;
        }

        private Bone(object tracker) : this()
        {
            Tracker = tracker;
        }

        public Bone()
        {
            ConnectorInformation = new ConnectorInformation { Bone = this };
        }

        public Bone Clone()
        {
            var bone = new Bone(Tracker) { Density = Density, FirstJoint = FirstJoint, SecondJoint = SecondJoint };
            bone.ConnectorInformation = ConnectorInformation.Clone(bone);
            return bone;
        }
    }
}