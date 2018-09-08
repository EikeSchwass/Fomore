using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;
using System;

namespace Testing.Helper
{
    [TestFixture]
    public class HistoryStackVMTests
    {
        [Test]
        public void NewEntryTest_IsItmAdding_isTrue()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });

            string afterNameCheck = historyStack.Current.Name;

            Assert.AreEqual("Dog", afterNameCheck);
        }

        [Test]
        public void NewEntryTest_IsItmAdding_isFail()
        {
            var creature = new Creature { Name = "Cat", Description = "Can run" };
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can bark" });

            string afterNameCheck = historyStack.Current.Name;

            Assert.AreNotEqual("Cat", afterNameCheck);
        }



        [Test]
        public void HistoryStackVMTest_AddingToList_IsAdded()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var historyStack = new HistoryStackVM<Creature>(creature);
            string nam = historyStack.Current.Name;
            Assert.AreEqual("Dog", nam);
        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndoFalse()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var historyStack = new HistoryStackVM<Creature>(creature);

            bool isUndo = historyStack.CanUndo;

            Assert.IsFalse(isUndo);
        }

        [Test]
        public void HistoryStackVMTest_CanUndo_IsUndo()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });

            bool isUndo = historyStack.CanUndo;

            Assert.IsTrue(isUndo);
        }

        [Test]
        public void HistoryStackVMTest_Undo_UndoTrue()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });

            historyStack.Undo();
            string afterNameCheck = historyStack.Current.Name;

            Assert.AreEqual("Dog", afterNameCheck);
        }

        [Test]
        public void HistoryStackVMTest_Undo_UndoFalse()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });

            historyStack.Undo();
            string afterNameCheck = historyStack.Current.Name;

            Assert.AreNotEqual("Perrot", afterNameCheck);
        }

        [Test]
        public void HistoryStackVMTest_Undo_UndoExceptionTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var historyStack = new HistoryStackVM<Creature>(creature);
            Assert.Throws<InvalidOperationException>(() => historyStack.Undo());
        }

        [Test]
        public void HistoryStackVMTest_Redo_RedoTrue()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });

            historyStack.Undo();
            historyStack.Redo();
            string afterNameCheck = historyStack.Current.Name;

            Assert.AreEqual("Perrot", afterNameCheck);
        }

        [Test]
        public void HistoryStackVMTest_Redo_RedoFalse()
        {
            var creature = new Creature();
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Dog", Description = "Can run" });
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });

            historyStack.Undo();
            historyStack.Redo();
            string afterNameCheck = historyStack.Current.Name;

            Assert.AreNotEqual("Dog", afterNameCheck);
        }

        [Test]
        public void HistoryStackVMTest_Redo_RedoExceptionTrue()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var historyStack = new HistoryStackVM<Creature>(creature);
            historyStack.NewEntry(new Creature { Name = "Perrot", Description = "Can fly" });
            Assert.Throws<InvalidOperationException>(() => historyStack.Redo());
        }
    }
}
