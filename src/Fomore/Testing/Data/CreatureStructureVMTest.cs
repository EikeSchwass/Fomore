using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing.Data
{
    [TestFixture]
    public class CreatureStructureVMTest
    {
        [Test]
        public void BoneCollectionVM_BonesAddTest_ReturnsEqualCountTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var creatureStructureVM = new CreatureStructureVM(creatureStructure);

            int expected = creatureStructureVM.BoneCollectionVM.Count;

            Assert.AreEqual(expected, creatureStructure.Bones.Count);
        }

        [Test]
        public void BoneCollectionVM_BonesAddTest_ReturnsEqualCountFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            int actual = creatureStructure.Bones.Count;
            var creatureStructureVM = new CreatureStructureVM(creatureStructure);
            creatureStructureVM.BoneCollectionVM.Add(new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density }));
            int expected = creatureStructureVM.BoneCollectionVM.Count;

            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void JointCollectionVM_JointsAddTest_ReturnsEqualCountTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var creatureStructureVM = new CreatureStructureVM(creatureStructure);

            int expected = creatureStructureVM.JointCollectionVM.Count;

            Assert.AreEqual(expected, creatureStructure.Joints.Count);
        }

        [Test]
        public void JointCollectionVM_BonesAddTest_ReturnsEqualCountFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            int actual = creatureStructure.Joints.Count;
            var creatureStructureVM = new CreatureStructureVM(creatureStructure);
            creatureStructureVM.JointCollectionVM.Add(new JointVM(new Joint() { Position = new Vector2(5, 10) }));
            int expected = creatureStructureVM.JointCollectionVM.Count;

            Assert.AreNotEqual(expected, actual);
        }
    }
}