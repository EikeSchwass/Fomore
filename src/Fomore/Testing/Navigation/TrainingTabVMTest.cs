
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Navigation;
using NUnit.Framework;

namespace Testing.Navigation
{
    [TestFixture]
    public class TrainingTabVMTest
    {
        [Test]
        public void SelectedCreature_SetSameValue_ReturnsSameValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            trainingTabVM.SelectedCreature = creatureVM;
            Assert.AreEqual(trainingTabVM.SelectedCreature.Name, "Cat");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            trainingTabVM.SelectedCreature = creatureVM2;
            Assert.AreEqual(trainingTabVM.SelectedCreature.Name, "Dog");
        }

        [Test]
        public void SelectedCreature_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            trainingTabVM.SelectedCreature = creatureVM2;
            Assert.AreNotEqual(trainingTabVM.SelectedCreature.Name, "Cat");
        }

        [Test]
        public void SelectedMovementPattern_SetSameValue_ReturnsSameValueTrue()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null, null)) { Name = "Fly" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            trainingTabVM.SelectedMovementPattern = movementPatternVM;
            Assert.AreEqual(trainingTabVM.SelectedMovementPattern.Name, "Fly");
        }

        [Test]
        public void SelectedMovementPattern_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null, null)) { Name = "Fly" };
            var movementPatternVM2 = new MovementPatternVM(new MovementPattern(null, null)) { Name = "Run" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            trainingTabVM.SelectedMovementPattern = movementPatternVM2;
            Assert.AreEqual(trainingTabVM.SelectedMovementPattern.Name, "Run");
        }

        [Test]
        public void SelectedMovementPattern_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var movementPatternVM = new MovementPatternVM(new MovementPattern(null, null)) { Name = "Fly" };
            var movementPatternVM2 = new MovementPatternVM(new MovementPattern(null, null)) { Name = "Run" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedMovementPattern = movementPatternVM };
            trainingTabVM.SelectedMovementPattern = movementPatternVM2;
            Assert.AreNotEqual(trainingTabVM.SelectedMovementPattern.Name, "Fly");
        }

        [Test]
        public void SelectedEnvironment_SetSameValue_ReturnsSameValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            trainingTabVM.SelectedEnvironment = environmentVM;
            Assert.AreEqual(trainingTabVM.SelectedEnvironment.Name, "Earth");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Moon" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            trainingTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreEqual(trainingTabVM.SelectedEnvironment.Name, "Moon");
        }

        [Test]
        public void SelectedEnvironment_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var environmentVM = new EnvironmentVM(new Environment()) { Name = "Earth" };
            var environmentVM2 = new EnvironmentVM(new Environment()) { Name = "Moon" };
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedEnvironment = environmentVM };
            trainingTabVM.SelectedEnvironment = environmentVM2;
            Assert.AreNotEqual(trainingTabVM.SelectedEnvironment.Name, "Earth");
        }

        [Test]
        public void OnSelect_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creatureVM = new CreatureVM(new Creature() { Name = "Cat" });
            var creatureVM2 = new CreatureVM(new Creature() { Name = "Dog" });
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { SelectedCreature = creatureVM };
            trainingTabVM.OnSelect(creatureVM2);
            Assert.AreEqual(trainingTabVM.SelectedCreature.Name, "Dog");
        }

        [Test]
        public void TargetSpeed_SetSameValue_ReturnsSameValueTrue()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { TargetSpeed = 9 };
            trainingTabVM.TargetSpeed = 9;
            Assert.AreEqual(trainingTabVM.TargetSpeed, 9);
        }

        [Test]
        public void TargetSpeed_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { TargetSpeed = 9 };
            trainingTabVM.TargetSpeed = 8;
            Assert.AreEqual(trainingTabVM.TargetSpeed, 8);
        }

        [Test]
        public void TargetSpeed_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { TargetSpeed = 9 };
            trainingTabVM.TargetSpeed = 8;
            Assert.AreNotEqual(trainingTabVM.TargetSpeed, 9);
        }

        [Test]
        public void ShowTraining_SetSameValue_ReturnsSameValueTrue()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { ShowTraining = true };
            trainingTabVM.ShowTraining = true;
            Assert.IsTrue(trainingTabVM.ShowTraining);
        }

        [Test]
        public void ShowTraining_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { ShowTraining = false };
            trainingTabVM.ShowTraining = true;
            Assert.IsTrue(trainingTabVM.ShowTraining);
        }

        [Test]
        public void ShowTraining_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var trainingTabVM = new TrainingTabVM(null, new EntityStorageVM(null)) { ShowTraining = false };
            trainingTabVM.ShowTraining = true;
            Assert.IsFalse(!trainingTabVM.ShowTraining);
        }
    }
}
