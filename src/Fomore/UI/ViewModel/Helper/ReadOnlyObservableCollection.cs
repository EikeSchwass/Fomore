﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Fomore.UI.ViewModel.Helper
{
    public class ReadOnlyObservableCollection<T> : ViewModelBase, IReadOnlyCollection<T>, INotifyCollectionChanged
    {
        private List<T> Collection { get; } = new List<T>();

        /// <inheritdoc />
        public int Count => Collection.Count;

        private ReadOnlyObservableCollection() { }

        public static CollectionAccess<T> Create() => Create(Enumerable.Empty<T>());

        public static CollectionAccess<T> Create(IEnumerable<T> elements)
        {
            var collection = new ReadOnlyObservableCollection<T>();
            collection.Collection.AddRange(elements);
            var collectionAccess = new CollectionAccess<T>(collection, collection.Add, collection.Remove, collection.Clear, collection.Insert);
            return collectionAccess;
        }

        private void Insert(T item, int index)
        {
            Collection.Insert(index, item);
            CollectionChanged?.Invoke(this,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            OnPropertyChanged(nameof(Count));

        }

        private void Add(T item)
        {
            Collection.Add(item);
            CollectionChanged?.Invoke(this,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, Collection.Count - 1));
            OnPropertyChanged(nameof(Count));
        }

        private void Remove(T item)
        {
            int oldIndex = Collection.IndexOf(item);
            Collection.Remove(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, oldIndex));
            OnPropertyChanged(nameof(Count));
        }

        private void Clear()
        {
            Collection.Clear();
            OnPropertyChanged(nameof(Count));
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => Collection.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}