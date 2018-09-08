using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class EnvironmentTabVMTest
    {
        [Test]
        public void SelectedEnvironment_SetSameValue_ReturnsSameValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Eir" };
            var environmentTabVM = new EnvironmentTabVM(null, null) { SelectedEnvironment = environmentVM };
            Assert.AreEqual(environmentTabVM.SelectedEnvironment.Name, "Eir");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Eir" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Space" };
            var environmentTabVM = new EnvironmentTabVM(null, null) { SelectedEnvironment = environmentVM };
            environmentTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreEqual(environmentTabVM.SelectedEnvironment.Name, "Space");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Eir" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Space" };
            var environmentTabVM = new EnvironmentTabVM(null, null) { SelectedEnvironment = environmentVM };
            environmentTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreEqual(environmentTabVM.SelectedEnvironment.Name, "Space");
        }
    }
}
