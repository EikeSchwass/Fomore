namespace Core.Physics
{
    internal class BoneTuple
    {
        public BoneBody FirstBoneBody { get; }
        public BoneBody SecondBoneBody { get; }

        public BoneTuple(BoneBody firstBoneBody, BoneBody secondBoneBody)
        {
            FirstBoneBody = firstBoneBody;
            SecondBoneBody = secondBoneBody;
        }
    }
}