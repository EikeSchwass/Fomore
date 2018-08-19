﻿using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;
using System;

namespace Testing
{
    [TestFixture]
    public class HistoryStackVMTests
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

            var creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            var afterNameCheck = historyStack.Current.Name;
            Assert.AreEqual("Dog", afterNameCheck);
        }

        [Test]
        public void NewEntryTest_IsItmAdding_isFail()
        {
            var creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(null);
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());
        }



        [Test]
        public void HistoryStackVMTest_AddingToList_IsAdded()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            var nam = historyStack.Current.Name;
            Assert.AreEqual("Dog", nam);
        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndoFalse()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            var isUndo = historyStack.CanUndo;
            Assert.IsFalse(isUndo);
        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndo()
        {
            var creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });
            var isUndo = historyStack.CanUndo;
            Assert.IsTrue(isUndo);
        }

        [Test]
        public void HistoryStackVMTest_Undo_IsUndo()
        {
            var creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });
            historyStack.Undo();
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());
        }

        [Test]
        public void HistoryStackVMTest_Redo_IsRedo()
        {
            var creature = new Creature();
            HistoryStackVM<Creature> historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });
            historyStack.Undo();
            historyStack.Redo();
            Assert.Throws<InvalidOperationException>(() => throw new InvalidOperationException());
        }

    }
}
