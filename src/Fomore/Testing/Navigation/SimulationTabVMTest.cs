
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class SimulationTabVMTest
    {
        [Test]
        public void SelectedCreature_SetSameValue_ReturnsSameValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            simulationTabVM.SelectedCreature = creatureVM;
            Assert.AreEqual(simulationTabVM.SelectedCreature.Name, "Cat");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            simulationTabVM.SelectedCreature = creatureVM2;
            Assert.AreEqual(simulationTabVM.SelectedCreature.Name, "Dog");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            simulationTabVM.SelectedCreature = creatureVM2;
            Assert.AreNotEqual(simulationTabVM.SelectedCreature.Name, "Cat");
        }

        [Test]
        public void SelectedMovementPattern_SetSameValue_ReturnsSameValueTrue()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null)) { Name = "Fly" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            simulationTabVM.SelectedMovementPattern = movementPatternVM;
            Assert.AreEqual(simulationTabVM.SelectedMovementPattern.Name, "Fly");
        }

        [Test]
        public void SelectedMovementPattern_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null)) { Name = "Fly" };
            var movementPatternVM2 = new MovementPatternVM(new MovementPattern(null)) { Name = "Run" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            simulationTabVM.SelectedMovementPattern = movementPatternVM2;
            Assert.AreEqual(simulationTabVM.SelectedMovementPattern.Name, "Run");
        }

        [Test]
        public void SelectedMovementPattern_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null)) { Name = "Fly" };
            var movementPatternVM2 = new MovementPatternVM(new MovementPattern(null)) { Name = "Run" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            simulationTabVM.SelectedMovementPattern = movementPatternVM2;
            Assert.AreNotEqual(simulationTabVM.SelectedMovementPattern.Name, "Fly");
        }

        [Test]
        public void SelectedEnvironment_SetSameValue_ReturnsSameValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            simulationTabVM.SelectedEnvironment = environmentVM;
            Assert.AreEqual(simulationTabVM.SelectedEnvironment.Name, "Earth");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Moon" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            simulationTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreEqual(simulationTabVM.SelectedEnvironment.Name, "Moon");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Moon" };
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            simulationTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreNotEqual(simulationTabVM.SelectedEnvironment.Name, "Earth");
        }

        [Test]
        public void OnSelect_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var simulationTabVM = new SimulationTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            simulationTabVM.OnSelect(creatureVM2);
            Assert.AreEqual(simulationTabVM.SelectedCreature.Name, "Dog");
        }
    }
}
