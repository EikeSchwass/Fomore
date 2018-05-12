using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Core.Creatures;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class BoneLibraryVM : ViewModelBase, INotifyCollectionChanged, IEnumerable<BoneCollectionVM>
    {
        private BoneLibrary BoneLibrary { get; }

        public ICommand AddBoneCollectionCommand { get; } = new StubCommand();

        public int Count => BoneLibrary.BoneCollections.Count;

        public BoneLibraryVM(BoneLibrary boneLibrary)
        {
            BoneLibrary = boneLibrary;
            CollectionChanged += (o, e) => OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <inheritdoc />
        public IEnumerator<BoneCollectionVM> GetEnumerator() => BoneLibrary.BoneCollections.Select(bc => new BoneCollectionVM(bc)).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}