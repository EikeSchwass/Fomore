using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Testing
{
    [TestFixture]
    class HistoryStackVMTests
    {
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
    }
}
