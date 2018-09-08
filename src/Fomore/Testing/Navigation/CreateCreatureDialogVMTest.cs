using System.Windows;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class CreateCreatureDialogVMTest
    {
        [Test]
        public void CreatureName_SetSameValue_ReturnsSameValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureName = "Dog" };
            createCreatureDialogVM.CreatureName = "Dog";
            Assert.AreEqual(createCreatureDialogVM.CreatureName, "Dog");
        }

        [Test]
        public void CreatureName_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureName = "Dog" };
            createCreatureDialogVM.CreatureName = "Cat";
            Assert.AreEqual(createCreatureDialogVM.CreatureName, "Cat");
        }

        [Test]
        public void CreatureName_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureName = "Dog" };
            createCreatureDialogVM.CreatureName = "Cat";
            Assert.AreNotEqual(createCreatureDialogVM.CreatureName, "Dog");
        }

        [Test]
        public void CreatureDescription_SetSameValue_ReturnsSameValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureDescription = "Can run" };
            createCreatureDialogVM.CreatureDescription = "Can run";
            Assert.AreEqual(createCreatureDialogVM.CreatureDescription, "Can run");
        }

        [Test]
        public void CreatureDescription_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureDescription = "Can run" };
            createCreatureDialogVM.CreatureDescription = "Can bark";
            Assert.AreEqual(createCreatureDialogVM.CreatureDescription, "Can bark");
        }

        [Test]
        public void CreatureDescription_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { CreatureDescription = "Can run" };
            createCreatureDialogVM.CreatureDescription = "Can bark";
            Assert.AreNotEqual(createCreatureDialogVM.CreatureDescription, "Can run");
        }

        [Test]
        public void Visibility_SetSameValue_ReturnsSameValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createCreatureDialogVM.Visibility = Visibility.Visible;
            Assert.AreEqual(createCreatureDialogVM.Visibility, Visibility.Visible);
        }

        [Test]
        public void Visibility_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createCreatureDialogVM.Visibility = Visibility.Hidden;
            Assert.AreEqual(createCreatureDialogVM.Visibility, Visibility.Hidden);
        }

        [Test]
        public void Visibility_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { Visibility = Visibility.Visible };
            createCreatureDialogVM.Visibility = Visibility.Hidden;
            Assert.AreNotEqual(createCreatureDialogVM.Visibility, Visibility.Visible);
        }

        [Test]
        public void IsOpen_SetSameValue_ReturnsSameValueTrue()
        {
            var createCreatureDialogVM = new CreateCreatureDialogVM(new EntityStorageVM(new EntitiyStorage())) { IsOpen = true };
            createCreatureDialogVM.IsOpen = true;
            Assert.True(createCreatureDialogVM.IsOpen);
        }
    }
}
