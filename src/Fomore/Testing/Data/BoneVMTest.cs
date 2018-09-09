using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing.Data
{
    [TestFixture]
    public class BoneVMTest
    {
        [Test]
        public void Density_SetSameValueTest_ReturnsSameValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { Density = 2 };

            float densityResult = boneVM.Density;

            Assert.AreEqual(2, densityResult);
        }

        [Test]
        public void Density_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var bone = new Bone(firstJoint, secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { Density = 3 };

            float densityResult = boneVM.Density;

            Assert.AreEqual(3, densityResult);
        }

        [Test]
        public void Density_SetDifferentValueTest_ReturnsSecondValueFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var bone = new Bone(firstJoint, secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { Density = 3 };

            float densityResult = boneVM.Density;

            Assert.AreNotEqual(2, densityResult);
        }

        [Test]
        public void FirstJoint_SetSameValueTest_ReturnsSameValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone);
            boneVM.FirstJoint = boneVM.FirstJoint;

            double lengthRLength = boneVM.FirstJoint.Position.Length;

            Assert.AreEqual(firstJoint.Position.Length, lengthRLength);
        }

        [Test]
        public void FirstJoint_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(3, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { FirstJoint = new JointVM(secondJoint) };

            double lengthRLength = boneVM.FirstJoint.Position.Length;

            Assert.AreEqual(secondJoint.Position.Length, lengthRLength);
        }

        [Test]
        public void FirstJoint_SetDifferentValueTest_ReturnsSecondValueFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(3, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { FirstJoint = new JointVM(secondJoint) };

            double lengthRLength = boneVM.FirstJoint.Position.Length;

            Assert.AreNotEqual(firstJoint.Position.Length, lengthRLength);
        }

        [Test]
        public void SecondJoint_SetSameValueTest_ReturnsSameValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone);
            boneVM.SecondJoint = boneVM.SecondJoint;

            double lengthRLength = boneVM.SecondJoint.Position.Length;

            Assert.AreEqual(secondJoint.Position.Length, lengthRLength);
        }

        [Test]
        public void SecondJoint_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(3, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { SecondJoint = new JointVM(firstJoint) };

            double lengthRLength = boneVM.SecondJoint.Position.Length;

            Assert.AreEqual(firstJoint.Position.Length, lengthRLength);
        }

        [Test]
        public void SecondJoint_SetDifferentValueTest_ReturnsSecondValueFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(3, 10) };
            var bone = new Bone(firstJoint,secondJoint) { Density = density, FirstJoint = firstJoint, SecondJoint = secondJoint };
            var boneVM = new BoneVM(bone) { SecondJoint = new JointVM(firstJoint) };

            double lengthRLength = boneVM.SecondJoint.Position.Length;

            Assert.AreNotEqual(secondJoint.Position.Length, lengthRLength);
        }


    }
}