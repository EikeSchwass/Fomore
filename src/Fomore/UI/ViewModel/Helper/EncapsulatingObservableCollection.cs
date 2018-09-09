// Eike Stein: Fomore/UI/EncapsulatingObservableCollection.cs (2018/06/12)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Helper
{
    public class EncapsulatingObservableCollection<TViewModel, TModel> : ViewModelBase, ICollection<TViewModel>, INotifyCollectionChanged
        where TViewModel : ViewModelBase<TModel>
        where TModel : class
    {
        private List<TViewModel> ViewModels { get; }
        private IList<TModel> EncapsulatedList { get; }

        /// <inheritdoc />
        public int Count => ViewModels.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        public DelegateCommand<TViewModel> AddItemCommand { get; }
        public DelegateCommand<TViewModel> RemoveItemCommand { get; }
        public DelegateCommand ClearItemCsommand { get; }

        /*public TViewModel this[int index]
        {
            get => ViewModels[index];
            set
            {
                EncapsulatedList[index] = value.Model;
                ViewModels[index] = value;
            }
        }*/

        public EncapsulatingObservableCollection(IList<TModel> encapsulatedList, IList<TViewModel> viewModels)
        {
            EncapsulatedList = encapsulatedList;
            ViewModels = viewModels.ToList();
            AddItemCommand = new DelegateCommand<TViewModel>(Add, o => true);
            RemoveItemCommand = new DelegateCommand<TViewModel>(item => Remove(item), o => ViewModels.Any());
            ClearItemCsommand = new DelegateCommand(o => Clear(), o => ViewModels.Any());
        }

        /// <inheritdoc />
        public IEnumerator<TViewModel> GetEnumerator() => ViewModels.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Add(TViewModel item)
        {
            if (ViewModels.Contains(item) || EncapsulatedList.Contains(item?.Model))
                throw new NotSupportedException("The item was already added to the collection");
            ViewModels.Add(item);
            EncapsulatedList.Add(item?.Model);
            CollectionChanged?.Invoke(ViewModels,
                                      new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(null);
            ClearItemCsommand.OnCanExecuteChanged();
            RemoveItemCommand.OnCanExecuteChanged();
        }

        /// <inheritdoc />
        public void Clear()
        {
            ViewModels.Clear();
            EncapsulatedList.Clear();
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            ClearItemCsommand.OnCanExecuteChanged();
            RemoveItemCommand.OnCanExecuteChanged();
        }

        /// <inheritdoc />
        public bool Contains(TViewModel item) => ViewModels.Contains(item);

        public bool Contains(TModel item) => ViewModels.Any(vm => vm.Model == item);

        /// <inheritdoc />
        public void CopyTo(TViewModel[] array, int arrayIndex)
        {
            ViewModels.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(TViewModel item)
        {
            int index = ViewModels.IndexOf(item);
            bool returnValue = ViewModels.Remove(item);
            EncapsulatedList.Remove(item?.Model);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            ClearItemCsommand.OnCanExecuteChanged();
            RemoveItemCommand.OnCanExecuteChanged();

            return returnValue;
        }

        public int IndexOf(TViewModel item) => ViewModels.IndexOf(item);
        public int IndexOf(TModel item) => ViewModels.IndexOf(ViewModels.First(vm => vm.Model == item));

        public void Insert(int index, TViewModel item)
        {
            if (ViewModels.Contains(item) || EncapsulatedList.Contains(item?.Model))
                throw new NotSupportedException("The item was already added to the collection");
            ViewModels.Insert(index, item);
            EncapsulatedList.Insert(index, item?.Model);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            ClearItemCsommand.OnCanExecuteChanged();
            RemoveItemCommand.OnCanExecuteChanged();
        }

        public void RemoveAt(int index) => Remove(ViewModels[index]);

        /// <inheritdoc />
        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}