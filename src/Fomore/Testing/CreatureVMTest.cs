using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Fomore.UI.ViewModel.Data;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    class CreatureVMTest
    {
        [Test]
        public void Name_GetMethodCall_GettingValue()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            var creatureVM = new CreatureVM(creature);
            Assert.AreEqual("Dog", creatureVM.Name);
        }

        [Test]
        public void Name_SetMethodCall_SettingValue()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            var creatureVM = new CreatureVM(creature);
            creatureVM.Name = "Cat";
            Assert.AreEqual("Cat", creatureVM.Name);
        }

        [Test]
        public void Name_SetMethodCall_SettingValueOveride()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            var creatureVM = new CreatureVM(creature);
            creatureVM.Name = "Dog";
            Assert.AreEqual("Dog", creatureVM.Name);
        }

        [Test]
        public void Description_GetMethodCall_GettingValue()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            CreatureVM creatureVM = new CreatureVM(creature);
            Assert.AreEqual("Can run", creatureVM.Description);
        }

        [Test]
        public void Description_SetMethodCall_SettingValue()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            CreatureVM creatureVM = new CreatureVM(creature);
            creatureVM.Description = "Can Bark";
            Assert.AreEqual("Can Bark", creatureVM.Description);
        }

        [Test]
        public void Description_SetMethodCall_SettingValueOveride()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            var creatureVM = new CreatureVM(creature);
            creatureVM.Description = "Can run";
            Assert.AreEqual("Can run", creatureVM.Description);

        }

        [Test]
        public void LastAccess_GetMethodCall_GettingValue()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };

            var creatureVM = new CreatureVM(creature);
            var dateTime = creatureVM.LastAccess;
            Assert.AreEqual(dateTime, creatureVM.LastAccess);
        }



    }
}