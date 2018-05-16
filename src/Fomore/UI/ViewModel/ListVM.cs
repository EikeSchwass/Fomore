using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Fomore.UI.ViewModel
{
    public class ListVM<T> : ViewModelBase, IEnumerable<T>, INotifyCollectionChanged where T : ViewModelBase
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
            OnPropertyChanged(nameof(Count));
        }

        public bool Remove(T item)
        {
            int index = Items.IndexOf(item);
            bool returnValue = Items.Remove(item);
            CollectionChanged?.Invoke(this,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove,
                                                                           item,
                                                                           index));
            OnPropertyChanged(nameof(Count));
            return returnValue;
        }

        public void Clear()
        {
            var list = Items.ToList();
            foreach (var item in list) Remove(item);
            OnPropertyChanged(nameof(Count));
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items) Items.Add(item);
        }

        public bool Contains(T item) => Items.Contains(item);
    }
}