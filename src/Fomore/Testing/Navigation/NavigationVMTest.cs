using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class NavigationVMTest
    {
        [Test]
        public void SelectedTab_SetSameValue_ReturnsSameValueTrue()
        {
            var createCreatureDialogVM = new NavigationVM(new EntityStorageVM(new EntitiyStorage())) { SelectedTab = new Tab1() };
            createCreatureDialogVM.SelectedTab = createCreatureDialogVM.SelectedTab;
            Assert.AreEqual(createCreatureDialogVM.SelectedTab.Header, "Creature");
        }

        [Test]
        public void SelectedTab_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var createCreatureDialogVM = new NavigationVM(new EntityStorageVM(new EntitiyStorage())) { SelectedTab = new Tab1() };
            createCreatureDialogVM.SelectedTab = new Tab2();
            Assert.AreEqual(createCreatureDialogVM.SelectedTab.Header, "Environment");
        }

        [Test]
        public void SelectedTab_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var createCreatureDialogVM = new NavigationVM(new EntityStorageVM(new EntitiyStorage())) { SelectedTab = new Tab1() };
            createCreatureDialogVM.SelectedTab = new Tab2();
            Assert.AreNotEqual(createCreatureDialogVM.SelectedTab.Header, "Creature");
        }
    }

    public class Tab1 : TabPageVM
    {
        public override string Header => "Creature";
    }

    public class Tab2 : TabPageVM
    {
        public override string Header => "Environment";
    }
}
