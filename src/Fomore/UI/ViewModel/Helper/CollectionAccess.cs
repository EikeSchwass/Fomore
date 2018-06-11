using System;
using System.Collections.Generic;

namespace Fomore.UI.ViewModel.Helper
{
    public class CollectionAccess<T>
    {
        public ReadOnlyObservableCollection<T> Collection { get; }
        private Action<T> AddCallback { get; }
        private Action<T> RemoveCallback { get; }
        private Action ClearCallback { get; }

        public CollectionAccess(ReadOnlyObservableCollection<T> collection, Action<T> add, Action<T> remove, Action clear)
        {
            Collection = collection;
            AddCallback = add;
            RemoveCallback = remove;
            ClearCallback = clear;
        }

        public void Add(T item)
        {
            AddCallback?.Invoke(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public void Remove(T item)
        {
            RemoveCallback?.Invoke(item);
        }

        public void Clear()
        {
            ClearCallback?.Invoke();
        }
    }
}