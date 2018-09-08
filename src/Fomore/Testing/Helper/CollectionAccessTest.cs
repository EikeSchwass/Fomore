using System.Collections.Generic;
using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing.Helper
{
    [TestFixture]
    public class CollectionAccessTest
    {
        [Test]
        public void CollectionAccess_AddingCollectionTest_Adding()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };
            var creatureThree = new Creature { Name = "perrot", Description = "Can fly" };
            var creatureList = new List<Creature> { creature, creatureTwo };
            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforAddingCollection = collectionAcces.Collection.Count;
            collectionAcces.Add(creatureThree);
            int afterAddingCollection = collectionAcces.Collection.Count;
            Assert.AreNotEqual(beforAddingCollection, afterAddingCollection);
        }

        [Test]
        public void CollectionAccess_RemovingCollectionTest_Removing()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };
            var creatureList = new List<Creature> { creature, creatureTwo };
            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforRemovingCollection = collectionAcces.Collection.Count;
            collectionAcces.Remove(creatureTwo);
            int afterRemovingCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforRemovingCollection, afterRemovingCollection);
        }

        [Test]
        public void CollectionAccess_ClearingCollectionTest_Clearing()
        {
            var creature = new Creature { Name = "Dog", Description = "Can run" };
            var creatureTwo = new Creature { Name = "turtle", Description = "Can crawl" };
            var creatureList = new List<Creature> { creature, creatureTwo };
            var collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            int beforClearningCollection = collectionAcces.Collection.Count;
            collectionAcces.Clear();
            int afterClearingCollection = collectionAcces.Collection.Count;
            Assert.AreNotEqual(beforClearningCollection, afterClearingCollection);
        }
    }
}
