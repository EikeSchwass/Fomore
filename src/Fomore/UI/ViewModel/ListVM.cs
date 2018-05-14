using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Fomore.UI.ViewModel
{
    public class ListVM<T> : IEnumerable<T>, INotifyCollectionChanged where T : ViewModelBase
    {

        private List<T> Items { get; } = new List<T>();

        public int Count => Items.Count;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(T item)
        {
            Items.Add(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public bool Remove(T item)
        {
            bool returnValue = Items.Remove(item);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return returnValue;
        }

        public void Clear()
        {
            Items.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void AddRange(IEnumerable<T> items)
        {
            var itemList = items.ToList();
            Items.AddRange(itemList);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, itemList));
        }

        public bool Contains(T item) => Items.Contains(item);
    }
}