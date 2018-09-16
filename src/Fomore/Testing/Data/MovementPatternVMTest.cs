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
            var movementPattern = new MovementPattern(null);
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 1";

            string actual = movementPatternVM.Name;

            Assert.AreEqual("Pattern 1", actual);
        }

        [Test]
        public void Name_SetDifferentValue_SecondValueTrue()
        {
            var movementPattern = new MovementPattern(null);
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 2";

            string actual = movementPatternVM.Name;

            Assert.AreEqual("Pattern 2", actual);
        }

        [Test]
        public void Name_SetDifferentValue_FirstValueFalse()
        {
            var movementPattern = new MovementPattern(null);
            var movementPatternVM = new MovementPatternVM(movementPattern) { Name = "Pattern 1" };
            movementPatternVM.Name = "Pattern 2";

            string actual = movementPatternVM.Name;

            Assert.AreNotEqual("Pattern 1", actual);
        }


        [Test]
        public void LastAccess_SetSameTime_ReturnsSameTimeTrue()
        {
            var movementPattern = new MovementPattern(null);
            var movementPatternVM = new MovementPatternVM(movementPattern);
            var expected = movementPatternVM.LastAccess;
            System.Threading.Thread.Sleep(100);

            var actual = movementPatternVM.LastAccess;

            Assert.AreEqual(expected.Second, actual.Second);
        }
    }
}