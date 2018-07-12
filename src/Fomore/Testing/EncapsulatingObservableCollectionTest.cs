using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Testing
{
    [TestFixture]
    class EncapsulatingObservableCollectionTest
    {
        [Test]
        public void EncapsulatingObservableCollection_Test_List()
        {
            var creatureStructure = new CreatureStructure();
            var creatureStructureVM = new CreatureStructureVM(creatureStructure);
            EncapsulatingObservableCollection<BoneVM, Bone> collObjectCollection = new EncapsulatingObservableCollection<BoneVM, Bone>(creatureStructure.Bones, b => new BoneVM(b));
        }

    }
}