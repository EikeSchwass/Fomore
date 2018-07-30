using System.Windows;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class CreateEnvironmentDialogVMTest
    {
        [Test]
        public void EnvironmentName_SetSameValue_ReturnsSameValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentName = "Earth" };
            createEnvironmentDialogVM.EnvironmentName = "Earth";
            Assert.AreEqual(createEnvironmentDialogVM.EnvironmentName, "Earth");
        }

        [Test]
        public void EnvironmentName_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentName = "Earth" };
            createEnvironmentDialogVM.EnvironmentName = "Moon";
            Assert.AreEqual(createEnvironmentDialogVM.EnvironmentName, "Moon");
        }

        [Test]
        public void EnvironmentName_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentName = "Earth" };
            createEnvironmentDialogVM.EnvironmentName = "Moon";
            Assert.AreNotEqual(createEnvironmentDialogVM.EnvironmentName, "Earth");
        }

        [Test]
        public void EnvironmentDescription_SetSameValue_ReturnsSameValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentDescription = "Can Move" };
            createEnvironmentDialogVM.EnvironmentDescription = "Can Move";
            Assert.AreEqual(createEnvironmentDialogVM.EnvironmentDescription, "Can Move");
        }

        [Test]
        public void EnvironmentDescription_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentDescription = "Can Move" };
            createEnvironmentDialogVM.EnvironmentDescription = "Can rotate";
            Assert.AreEqual(createEnvironmentDialogVM.EnvironmentDescription, "Can rotate");
        }

        [Test]
        public void EnvironmentDescription_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { EnvironmentDescription = "Can Move" };
            createEnvironmentDialogVM.EnvironmentDescription = "Can rotate";
            Assert.AreNotEqual(createEnvironmentDialogVM.EnvironmentDescription, "Can Move");
        }

        [Test]
        public void Visibility_SetSameValue_ReturnsSameValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createEnvironmentDialogVM.Visibility = Visibility.Visible;
            Assert.AreEqual(createEnvironmentDialogVM.Visibility, Visibility.Visible);
        }

        [Test]
        public void Visibility_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createEnvironmentDialogVM.Visibility = Visibility.Hidden;
            Assert.AreEqual(createEnvironmentDialogVM.Visibility, Visibility.Hidden);
        }

        [Test]
        public void Visibility_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createEnvironmentDialogVM = new CreateEnvironmentDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createEnvironmentDialogVM.Visibility = Visibility.Hidden;
            Assert.AreNotEqual(createEnvironmentDialogVM.Visibility, Visibility.Visible);
        }
    }
}
