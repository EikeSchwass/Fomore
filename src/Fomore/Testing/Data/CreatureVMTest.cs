using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing.Data
{
    [TestFixture]
    public class CreatureVMTest
    {
        [Test]
        public void Name_SetSameValueTest_ReturnsSameValueTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature);
            creatureVM.Name = creatureVM.Name;

            string expected = creatureVM.Name;

            Assert.AreEqual(expected, creature.Name);
        }

        [Test]
        public void Name_SetDifferentValueTest_ReturnsSecondValueTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature) { Name = "Cat" };

            string expected = creatureVM.Name;

            Assert.AreEqual(expected, "Cat");
        }

        [Test]
        public void Name_SetDifferentValueTest_ReturnsFirstValueFalse()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature) { Name = "Cat" };

            string expected = creatureVM.Name;

            Assert.AreNotEqual(expected, "Dog");
        }

        [Test]
        public void Description_SetSameValue_ReturnsSameValueTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature);
            creatureVM.Description = creatureVM.Description;

            string expected = creatureVM.Description;

            Assert.AreEqual(expected, "Can run");
        }

        [Test]
        public void Description_SetDifferentValue_ReturnsSecondValueTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature) { Description = "Can Bark" };

            string expected = creatureVM.Description;

            Assert.AreEqual(expected, "Can Bark");
        }

        [Test]
        public void Description_SetDifferentValue_ReturnsFirstValueFalse()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature) { Description = "Can Bark" };

            string expected = creatureVM.Description;

            Assert.AreNotEqual(expected, "Can run");

        }

        [Test]
        public void LastAccess_SetSameTime_ReturnsSameTimeTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature);
            System.Threading.Thread.Sleep(100);
            creatureVM.Description = "Can Bark";
            var expected = creatureVM.LastAccess;
            creatureVM.Description = "Can run";

            var actual = creatureVM.LastAccess;

            Assert.AreEqual(expected.Second, actual.Second);
        }

        [Test]
        public void LastAccess_SetDifferentTime_ReturnsSameTimeFalse()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureVM = new CreatureVM(creature);
            var expected = creatureVM.LastAccess;
            System.Threading.Thread.Sleep(1000);
            creatureVM.Description = "Can Bark";

            var actual = creatureVM.LastAccess;

            Assert.AreNotEqual(expected.Second, actual.Second);
        }

        [Test]
        public void CreatureStructureVM_WithoutValueTest_ReturnsZeroCountTrue()
        {
            var creature = new Creature { MovementPatterns = { new MovementPattern() } };
            var creatureVM = new CreatureVM(creature);

            var creatureStructureVM = creatureVM.CreatureStructureVM;

            Assert.AreEqual(true, creatureStructureVM.BoneCollectionVM.Count == 0);
        }

        [Test]
        public void CreatureStructureVM_WithValueTest_ReturnsZeroCountFalse()
        {
            var creature = new Creature { MovementPatterns = { new MovementPattern() } };
            var creatureVM = new CreatureVM(creature);

            var creatureStructureVM = creatureVM.CreatureStructureVM;
            creatureStructureVM.BoneCollectionVM.Add(new BoneVM(new Bone(null, null)
            {
                Density = 2,
                SecondJoint = new Joint() { Position = new Vector2(5, 10) }
            }));

            Assert.AreNotEqual(true, creatureStructureVM.BoneCollectionVM.Count == 0);
        }

        [Test]
        public void MovementPatternCollectionVM_GetValueTest_ReturnsSameCountTrue()
        {
            var creature = new Creature { MovementPatterns = { new MovementPattern() } };
            var creatureVM = new CreatureVM(creature);

            var movementPatternCollectionVM = creatureVM.MovementPatternCollectionVM;

            Assert.AreEqual(true, movementPatternCollectionVM.Count != 0);
        }

        [Test]
        public void MovementPatternCollectionVM_GetValueTest_ReturnsSameCountFalse()
        {
            var creature = new Creature { MovementPatterns = { new MovementPattern() } };
            var creatureVM = new CreatureVM(creature);

            var movementPatternCollectionVM = creatureVM.MovementPatternCollectionVM;

            Assert.AreNotEqual(true, movementPatternCollectionVM.Count == 0);
        }
        [Test]
        public void Clone_GetCloneTest_ReturnsTrue()
        {
            var creature = new Creature { MovementPatterns = { new MovementPattern() } };
            var creatureVM = new CreatureVM(creature);

            var clone = creatureVM.Clone();

            Assert.AreEqual(true, clone != null);
        }
    }
}