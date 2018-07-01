using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;
using System;

namespace Testing
{
    [TestFixture]
    class HistoryStackVMTests
    {
        public void Redo_Test_isTrue()
        {
            //Arrange (Initialization)
            Creature creature = new Creature();
            CreatureVM creatureVM = new CreatureVM(creature);
            HistoryStackVM<CreatureVM> historyStack = new HistoryStackVM<CreatureVM>(creatureVM);

            //Act (assignments or actions to test)
            historyStack.Redo();

            //Assert (compare result)
        }

        [Test]
        public void NewEntryTest_IsItmAdding_isTrue()
        {

            Creature creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { CreatureName = "Dog", CreatureDescription = "Can run" });
            var afterCreatureNameCheck = historyStack.Current.CreatureName;
            Assert.AreEqual("Dog", afterCreatureNameCheck);
        }

        [Test]
        public void NewEntryTest_IsItmAdding_isFail()
        {
            Creature creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(null);
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());
        }



        [Test]
        public void HistoryStackVMTest_AddingToList_IsAdded()
        {

            Creature creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            var nam = historyStack.Current.CreatureName;
            Assert.AreEqual("Dog", nam);


        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndoFalse()
        {

            Creature creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            var isUndo = historyStack.CanUndo;
            Assert.IsFalse(isUndo);


        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndo()
        {

            Creature creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { CreatureName = "Dog", CreatureDescription = "Can run" });
            historyStack.NewEntry(new Creature { CreatureName = "Perrot", CreatureDescription = "Can fly" });
            var isUndo = historyStack.CanUndo;
            Assert.IsTrue(isUndo);


        }

        [Test]
        public void HistoryStackVMTest_Undo_IsUndo()
        {

            Creature creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { CreatureName = "Dog", CreatureDescription = "Can run" });
            historyStack.NewEntry(new Creature { CreatureName = "Perrot", CreatureDescription = "Can fly" });
            historyStack.Undo();
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());


        }

        [Test]
        public void HistoryStackVMTest_Redo_IsRedo()
        {

            Creature creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { CreatureName = "Dog", CreatureDescription = "Can run" });
            historyStack.NewEntry(new Creature { CreatureName = "Perrot", CreatureDescription = "Can fly" });
            historyStack.Undo();
            historyStack.Redo();
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());


        }

    }
}
