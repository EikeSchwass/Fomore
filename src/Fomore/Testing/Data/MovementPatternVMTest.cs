using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing.Data
{
    [TestFixture]
    public class MovementPatternVMTest
    {
        [Test]
        public void Name_SetSameValue_SameValueTrue()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 1";

            string actual = movementPatternVM.Name;

            Assert.AreEqual("Pattern 1", actual);
        }

        [Test]
        public void Name_SetDifferentValue_SecondValueTrue()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 2";

            string actual = movementPatternVM.Name;

            Assert.AreEqual("Pattern 2", actual);
        }

        [Test]
        public void Name_SetDifferentValue_FirstValueFalse()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 2";

            string actual = movementPatternVM.Name;

            Assert.AreNotEqual("Pattern 1", actual);
        }

        [Test]
        public void Iterations_SetSameValue_SameValueTrue()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Iterations = 5 };
            movementPatternVM.Iterations = 5;

            int actual = movementPatternVM.Iterations;

            Assert.AreEqual(5, actual);
        }

        [Test]
        public void Iterations_SetDifferentValue_SecondValueTrue()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Iterations = 5 };
            movementPatternVM.Iterations = 8;

            int actual = movementPatternVM.Iterations;

            Assert.AreEqual(8, actual);
        }

        [Test]
        public void Iterations_SetDifferentValue_FirstValueFalse()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Iterations = 5 };
            movementPatternVM.Iterations = 8;

            int actual = movementPatternVM.Iterations;

            Assert.AreNotEqual(5, actual);
        }

        [Test]
        public void LastAccess_SetSameTime_ReturnsSameTimeTrue()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Iterations = 5 };
            var expected = movementPatternVM.LastAccess;
            System.Threading.Thread.Sleep(100);
            movementPatternVM.Iterations = 8;

            var actual = movementPatternVM.LastAccess;

            Assert.AreEqual(expected.Second, actual.Second);
        }

        [Test]
        public void LastAccess_SetDifferentTime_ReturnsSameTimeFalse()
        {
            var movementPattern = new MovementPattern();
            var movementPatternVM = new MovementPatternVM(movementPattern) { Iterations = 5 };
            var expected = movementPatternVM.LastAccess;
            System.Threading.Thread.Sleep(1000);
            movementPatternVM.Iterations = 8;

            var actual = movementPatternVM.LastAccess;

            Assert.AreNotEqual(expected.Second, actual.Second);
        }
    }
}