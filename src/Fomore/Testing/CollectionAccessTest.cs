using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class CollectionAccessTest
    {
        [Test]
        public void CollectionAccess_AddingCollectionTest_Adding()
        {
            Creature creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            Creature creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };
            Creature creatureThree = new Creature { CreatureName = "perrot", CreatureDescription = "Can fly" };
            List<Creature> creatureList = new List<Creature>();
            creatureList.Add(creature);
            creatureList.Add(creatureTwo);
            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforAddingCollection = collectionAcces.Collection.Count;
            collectionAcces.Add(creatureThree);
            var afterAddingCollection = collectionAcces.Collection.Count;
            Assert.AreNotEqual(beforAddingCollection, afterAddingCollection);
        }

        [Test]
        public void CollectionAccess_RemovingCollectionTest_Removing()
        {
            Creature creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            Creature creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };
            List<Creature> creatureList = new List<Creature>();
            creatureList.Add(creature);
            creatureList.Add(creatureTwo);
            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforRemovingCollection = collectionAcces.Collection.Count;
            collectionAcces.Remove(creatureTwo);
            var afterRemovingCollection = collectionAcces.Collection.Count;

            Assert.AreNotEqual(beforRemovingCollection, afterRemovingCollection);
        }

        [Test]
        public void CollectionAccess_ClearingCollectionTest_Clearing()
        {
            Creature creature = new Creature { CreatureName = "Dog", CreatureDescription = "Can run" };
            Creature creatureTwo = new Creature { CreatureName = "turtle", CreatureDescription = "Can crawl" };
            List<Creature> creatureList = new List<Creature>();
            creatureList.Add(creature);
            creatureList.Add(creatureTwo);
            CollectionAccess<Creature> collectionAcces = ReadOnlyObservableCollection<Creature>.Create(creatureList);
            var beforClearningCollection = collectionAcces.Collection.Count;
            collectionAcces.Clear();
            var afterClearingCollection = collectionAcces.Collection.Count;
            //hello worl
            Assert.AreNotEqual(beforClearningCollection, afterClearingCollection);
        }
    }
}
