using System;
using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing.Helper
{
    [TestFixture]
    public class EncapsulatingObservableCollectionTest
    {
        [Test]
        public void Add_AddItem_AddedTrue()
        {//updates
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Add(boneVM);

            int afterCount = collObjectCollection.Count;

            Assert.AreEqual(afterCount, 3);
        }

        [Test]
        public void Add_AddItem_AddedFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Add(boneVM);

            int afterCount = collObjectCollection.Count;

            Assert.AreNotEqual(afterCount, 2);
        }

        [Test]
        public void Add_AddSameViewModelItem_NotSupportedExceptionTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Add(boneVM);

            Assert.Throws<NotSupportedException>(() => collObjectCollection.Add(boneVM));
        }

        [Test]
        public void Add_AddSameModelItem_NotSupportedExceptionTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Add(boneVM);
            var boneVM2 = new BoneVM(model);

            Assert.Throws<NotSupportedException>(() => collObjectCollection.Add(boneVM2));
        }

        [Test]
        public void Clear_ClearTest_ClearTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Add(boneVM);

            collObjectCollection.Clear();

            Assert.AreEqual(collObjectCollection.Count, 0);
        }

        [Test]
        public void Clear_ClearTest_ClearFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Add(boneVM);

            collObjectCollection.Clear();

            Assert.AreNotEqual(collObjectCollection.Count > 0, true);
        }

        [Test]
        public void Remove_RemoveTest_RemoveTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Add(boneVM);

            bool removed = collObjectCollection.Remove(boneVM);

            Assert.AreEqual(removed, true);
        }

        [Test]
        public void Remove_RemoveTest_RemoveFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Add(boneVM);

            bool removed = collObjectCollection.Remove(boneVM);

            Assert.AreNotEqual(removed, false);
        }

        [Test]
        public void Insert_InsertItem_InsertedTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Insert(0, boneVM);

            int afterCount = collObjectCollection.Count;

            Assert.AreEqual(afterCount, 3);
        }

        [Test]
        public void Insert_InsertItem_InsertedFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Insert(0, boneVM);

            int afterCount = collObjectCollection.Count;

            Assert.AreNotEqual(afterCount, 2);
        }

        [Test]
        public void Insert_InsertSameViewModelItem_NotSupportedExceptionTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var boneVM = new BoneVM(new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density });
            collObjectCollection.Insert(0, boneVM);

            Assert.Throws<NotSupportedException>(() => collObjectCollection.Insert(0, boneVM));
        }

        [Test]
        public void Insert_InsertSameModelItem_NotSupportedExceptionTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(0, boneVM);
            var boneVM2 = new BoneVM(model);

            Assert.Throws<NotSupportedException>(() => collObjectCollection.Insert(0, boneVM2));
        }

        [Test]
        public void RemoveAt_RemoveAtTest_RemovedTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));

            collObjectCollection.RemoveAt(0);

            Assert.AreEqual(collObjectCollection.Count, 1);
        }

        [Test]
        public void RemoveAt_RemoveAtTest_RemovedFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));

            collObjectCollection.RemoveAt(0);

            Assert.AreNotEqual(collObjectCollection.Count, 2);
        }

        [Test]
        public void IndexOf_ModelIndexTest_SameIndexTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    model
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));

            int index = collObjectCollection.IndexOf(model);

            Assert.AreEqual(index, 1);
        }

        [Test]
        public void IndexOf_ModelIndexTest_SameIndexFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    model
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));

            int index = collObjectCollection.IndexOf(model);

            Assert.IsFalse(index != 1);
        }

        [Test]
        public void IndexOf_ViewModelIndexTest_SameIndexTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);

            int index = collObjectCollection.IndexOf(boneVM);

            Assert.AreEqual(index, 1);
        }

        [Test]
        public void IndexOf_ViewModelIndexTest_SameIndexFalse()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);

            int index = collObjectCollection.IndexOf(boneVM);

            Assert.IsFalse(index != 1);
        }

        [Test]
        public void Contains_ModelContainsTest_ContainsTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);

            bool contains = collObjectCollection.Contains(model);

            Assert.True(contains);
        }

        [Test]
        public void Contains_ViewModelContainsTest_ContainsTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);

            bool contains = collObjectCollection.Contains(boneVM);

            Assert.True(contains);
        }

        [Test]
        public void CopyTo_CopyTest_CopyTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);
            var boneVms = new BoneVM[collObjectCollection.Count];

            collObjectCollection.CopyTo(boneVms, 0);

            bool contains = boneVms[1].Density.Equals(density);

            Assert.True(contains);
        }

        [Test]
        public void GetEnumrator_GetEnumratorTest_GetEnumratorTrue()
        {
            const float density = 2;
            var firstJoint = new Joint() { Position = new Vector2(5, 10) };
            var secondJoint = new Joint() { Position = new Vector2(5, 10) };
            var creatureStructure = new CreatureStructure()
            {
                Bones =
                {
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density},
                    new Bone() {SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density}
                },
                Joints = { new Joint() { Position = new Vector2(5, 10) }, new Joint() { Position = new Vector2(5, 10) } }
            };
            var collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
            var model = new Bone() { SecondJoint = secondJoint, FirstJoint = firstJoint, Density = density };
            var boneVM = new BoneVM(model);
            collObjectCollection.Insert(1, boneVM);

            // ReSharper disable once GenericEnumeratorNotDisposed
            var enumrator = collObjectCollection.GetEnumerator();

            int count = 0;
            while (enumrator.MoveNext())
            {
                count++;
            }

            Assert.AreEqual(count, 3);
        }
    }
}