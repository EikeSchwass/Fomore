using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Fomore.UI.ViewModel;

namespace Fomore.UI
{
    public class ViewViewModelNavigator : ViewModelBase, INotifyCollectionChanged, IEnumerable<ViewViewModelBase>
    {
        private Stack<ViewViewModelBase> ViewViewModelStack { get; } = new Stack<ViewViewModelBase>();

        public ViewViewModelBase CurrentViewViewModel => ViewViewModelStack.Peek();

        /// <inheritdoc />
        public IEnumerator<ViewViewModelBase> GetEnumerator() => ViewViewModelStack.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void PushView(ViewViewModelBase viewViewModel)
        {
            ViewViewModelStack.Push(viewViewModel);
            CollectionChanged?.Invoke(this,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                                                                           viewViewModel,
                                                                           ViewViewModelStack.Count - 1));
            OnPropertyChanged(nameof(CurrentViewViewModel));
        }

        public ViewViewModelBase PopView()
        {
            var viewViewModel = ViewViewModelStack.Pop();
            CollectionChanged?.Invoke(this,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,
                                                                           viewViewModel,
                                                                           ViewViewModelStack.Count));
            OnPropertyChanged(nameof(CurrentViewViewModel));
            return viewViewModel;
        }
    }
}