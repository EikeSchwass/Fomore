using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Core.Creatures;

namespace Fomore.UI.ViewModel
{
    public class BoneCollectionVM : ViewModelBase, ICollection<BoneModelVM>, INotifyCollectionChanged
    {
        private BoneCollection BoneCollection { get; }

        public BoneCollectionVM(BoneCollection boneCollection)
        {
            BoneCollection = boneCollection;
        }

        /// <inheritdoc />
        public IEnumerator<BoneModelVM> GetEnumerator() => BoneCollection.Select(bm => new BoneModelVM(bm)).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Add(BoneModelVM item)
        {
            BoneCollection.Add(item?.BoneModel);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public void Clear()
        {
            BoneCollection.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public bool Contains(BoneModelVM item) => BoneCollection.Contains(item?.BoneModel);

        /// <inheritdoc />
        public void CopyTo(BoneModelVM[] array, int arrayIndex)
        {
            BoneCollection.Select(bm => new BoneModelVM(bm)).ToArray().CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(BoneModelVM item)
        {
            bool returnValue = BoneCollection.Remove(item?.BoneModel);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            OnPropertyChanged(nameof(Count));
            return returnValue;
        }

        /// <inheritdoc />
        public int Count => BoneCollection.Count;

        /// <inheritdoc />
        public bool IsReadOnly => BoneCollection.IsReadOnly;

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}