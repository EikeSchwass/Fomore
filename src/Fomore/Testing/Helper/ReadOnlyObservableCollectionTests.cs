using System.Collections.Generic;
using System.Linq;
using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing.Helper
{
    [TestFixture]
    public class ReadOnlyObservableCollectionTests
    {
        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsEmpty_IsTrue()
        {
            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(Enumerable.Empty<Creature>());

            Assert.AreEqual(0, collectionAcces.Collection.Count);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsAddingElement_Adding()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };
            var creatureThree = new Creature { Name = "Crow", Description = "Can fly" };

            var creatureList = new List<Creature> { creature, creatureTwo };

            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforAddingCollection = collectionAcces.Collection.Count;
            collectionAcces.Add(creatureThree);
            int afterAddingCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforAddingCollection, afterAddingCollection);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsRemovingingElement_Removing()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };

            var creatureList = new List<Creature> { creature, creatureTwo };

            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforRemovingCollection = collectionAcces.Collection.Count;
            collectionAcces.Remove(creatureTwo);
            int afterRemovinCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforRemovingCollection, afterRemovinCollection);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsClearingElements_ClearingElements()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };

            var creatureList = new List<Creature> { creature, creatureTwo };

            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforClearingCollection = collectionAcces.Collection.Count;
            collectionAcces.Clear();
            int afterClearingCollection = collectionAcces.Collection.Count;
            Assert.AreNotEqual(beforClearingCollection, afterClearingCollection);
        }
    }
}
