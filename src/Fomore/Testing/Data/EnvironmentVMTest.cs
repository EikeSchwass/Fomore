using Fomore.UI.ViewModel.Data;
using NUnit.Framework;
using Environment = Core.Environment;

namespace Testing.Data
{
    [TestFixture()]
    public class EnvironmentVMTest
    {
        [Test]
        public void Name_SetSameValueTest_ReturnsSameValueTrue()
        {
            var environment = new Environment() { Description = "Earth ...", Name = "Earth" };
            var environmentVM = new EnvironmentVM(environment) { Name = "Earth" };

            string actual = environmentVM.Name;

            Assert.AreEqual(actual, "Earth");
        }

        [Test]
        public void Name_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            var environment = new Environment() { Description = "Earth ...", Name = "Earth" };
            var environmentVM = new EnvironmentVM(environment) { Name = "Moon" };

            string actual = environmentVM.Name;

            Assert.AreEqual(actual, "Moon");
        }

        [Test]
        public void Name_SetDifferentValueTest_ReturnsFirstValueFalse()
        {
            var environment = new Environment() { Description = "Earth ...", Name = "Earth" };
            var environmentVM = new EnvironmentVM(environment) { Name = "Moon" };

            string actual = environmentVM.Name;

            Assert.AreNotEqual(actual, "Earth");
        }

        [Test]
        public void Description_SetSameValue_ReturnsSameValueTrue()
        {
            var environment = new Environment() { Description = "Earth ..." };
            var environmentVM = new EnvironmentVM(environment) { Description = "Earth ..." };

            string actual = environmentVM.Description;

            Assert.AreEqual("Earth ...", actual);
        }

        [Test]
        public void Description_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environment = new Environment() { Description = "Earth ..." };
            var environmentVM = new EnvironmentVM(environment) { Description = "Moon ..." };

            string actual = environmentVM.Description;

            Assert.AreEqual("Moon ...", actual);
        }

        [Test]
        public void Description_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environment = new Environment() { Description = "Earth ..." };
            var environmentVM = new EnvironmentVM(environment) { Description = "Moon ..." };

            string actual = environmentVM.Description;

            Assert.AreNotEqual("Earth ...", actual);
        }

        [Test]
        public void Gravity_SetSameValue_ReturnsSameValueTrue()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Gravity = 9.5 };
            environmentVM.Gravity = 9.5;

            double actual = environmentVM.Gravity;

            Assert.AreEqual(9.5, actual);
        }

        [Test]
        public void Gravity_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Gravity = 9.5 };
            environmentVM.Gravity = 9.8;

            double actual = environmentVM.Gravity;

            Assert.AreEqual(9.8, actual);
        }

        [Test]
        public void Gravity_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Gravity = 9.5 };
            environmentVM.Gravity = 9.8;

            double actual = environmentVM.Gravity;

            Assert.AreNotEqual(9.5, actual);
        }

        [Test]
        public void Friction_SetSameValue_ReturnsSameValueTrue()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Friction = 9.5 };
            environmentVM.Friction = 9.5;

            double actual = environmentVM.Friction;

            Assert.AreEqual(9.5, actual);
        }

        [Test]
        public void Friction_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Friction = 9.5 };
            environmentVM.Friction = 9.8;

            double actual = environmentVM.Friction;

            Assert.AreEqual(9.8, actual);
        }

        [Test]
        public void Friction_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Friction = 9.5 };
            environmentVM.Friction = 9.8;

            double actual = environmentVM.Friction;

            Assert.AreNotEqual(9.5, actual);
        }

        [Test]
        public void LastAccess_SetSameTime_ReturnsSameTimeTrue()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Friction = 9.5 };
            var expected = environmentVM.LastAccess;
            System.Threading.Thread.Sleep(100);
            environmentVM.Friction = 9.8;

            var actual = environmentVM.LastAccess;

            Assert.AreEqual(expected.Second, actual.Second);
        }

        [Test]
        public void LastAccess_SetDifferentTime_ReturnsSameTimeFalse()
        {
            var environment = new Environment();
            var environmentVM = new EnvironmentVM(environment) { Friction = 9.5 };
            var expected = environmentVM.LastAccess;
            System.Threading.Thread.Sleep(1000);
            environmentVM.Friction = 9.8;

            var actual = environmentVM.LastAccess;

            Assert.AreNotEqual(expected.Second, actual.Second);
        }
    }
}