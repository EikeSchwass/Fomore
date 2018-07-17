using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class ReadOnlyObservableCollectionTests
    {
        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsEmpty_IsTrue()
        {
            var creature = new Creature();
            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(Enumerable.Empty<Creature>());

            Assert.AreEqual(0, collectionAcces.Collection.Count);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsAddingElement_Adding()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            var creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };
            var creatureThree = new Creature { CreatureName = "Crow", CreatureDescription = "Can fly" };

            var creatureList = new List<Creature> {creature, creatureTwo};

            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforAddingCollection = collectionAcces.Collection.Count;
            collectionAcces.Add(creatureThree);
            var afterAddingCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforAddingCollection, afterAddingCollection);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsRemovingingElement_Removing()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            var creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };

            var creatureList = new List<Creature> {creature, creatureTwo};

            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforRemovingCollection = collectionAcces.Collection.Count;
            collectionAcces.Remove(creatureTwo);
            var afterRemovinCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforRemovingCollection, afterRemovinCollection);
        }

        [Test]
        public void ReadOnlyObervationCollectio_CheckingIsClearingElements_ClearingElements()
        {
            var creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            var creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };

            var creatureList = new List<Creature> {creature, creatureTwo};

            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforClearingCollection = collectionAcces.Collection.Count;
            collectionAcces.Clear();
            var afterClearingCollection = collectionAcces.Collection.Count;
            Console.WriteLine("Clearing collection  ", afterClearingCollection);
            Assert.AreNotEqual(beforClearingCollection, afterClearingCollection);
        }
    }
}
