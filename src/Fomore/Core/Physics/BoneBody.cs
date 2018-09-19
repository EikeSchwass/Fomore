using FarseerPhysics.Dynamics;

namespace Core.Physics
{
    public class BoneBody
    {
        public Body Body { get; }
        public Bone Bone { get; }

        public bool IsColliding { get; internal set; }

        public BoneBody(Body body, Bone bone)
        {
            Body = body;
            Bone = bone;
        }
    }
}