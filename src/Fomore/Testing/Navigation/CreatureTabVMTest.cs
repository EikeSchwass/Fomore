using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class CreatureTabVMTest
    {
        [Test]
        public void SelectedCreature_SetSameValue_ReturnsSameValueTrue()
        {
            var creatureVm = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureTabVM = new CreatureTabVM(null, null) { SelectedCreature = creatureVm };
            creatureTabVM.SelectedCreature = creatureVm;
            Assert.AreEqual(creatureTabVM.SelectedCreature.Name, "Cat");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creatureVm = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVm2 = new CreatureVM(new Creature() { Name = "Dog" });
            var creatureTabVM = new CreatureTabVM(null, null) { SelectedCreature = creatureVm };
            creatureTabVM.SelectedCreature = creatureVm2;
            Assert.AreEqual(creatureTabVM.SelectedCreature.Name, "Dog");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var creatureVm = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVm2 = new CreatureVM(new Creature() { Name = "Dog" });
            var creatureTabVM = new CreatureTabVM(null, null) { SelectedCreature = creatureVm };
            creatureTabVM.SelectedCreature = creatureVm2;
            Assert.AreNotEqual(creatureTabVM.SelectedCreature.Name, "Cat");
        }
    }
}
