using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing.Data
{
    [TestFixture]
    public class JointVMTest
    {
        [Test]
        public void Position_SetSameValueTest_ReturnsSameValueTrue()
        {
            var joint = new Joint() { Position = new Vector2(5, 10) };
            var jointVM = new JointVM(joint);
            jointVM.Position = jointVM.Position;

            double actual = jointVM.Position.Length;

            Assert.AreEqual(joint.Position.Length, actual);
        }

        [Test]
        public void Position_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            var joint = new Joint() { Position = new Vector2(5, 10) };
            var jointVM = new JointVM(joint) { Position = new Vector2(3, 10) };

            double actual = jointVM.Position.Length;

            Assert.AreEqual(new Vector2(3, 10).Length, actual);
        }

        [Test]
        public void Position_SetDifferentValueTest_ReturnsFirstValueFalse()
        {
            var joint = new Joint() { Position = new Vector2(5, 10) };
            var jointVM = new JointVM(joint) { Position = new Vector2(3, 10) };

            double actual = jointVM.Position.Length;

            Assert.AreNotEqual(new Vector2(5, 10).Length, actual);
        }
    }
}