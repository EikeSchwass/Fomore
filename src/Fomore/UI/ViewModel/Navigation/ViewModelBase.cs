using System.ComponentModel;
using System.Runtime.CompilerServices;
using Fomore.UI.Annotations;

namespace Fomore.UI.ViewModel.Navigation
{
    /// <summary>
    /// The base class for all view models. Implements <see cref="INotifyPropertyChanged"/> and the handler logic.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// The base class for all view models that have a concrete model as a backend. For example a creature model.
    /// </summary>
    /// <typeparam name="T">The Type of the class of the model.</typeparam>
    public abstract class ViewModelBase<T> : ViewModelBase
    {
        public T Model { get; }

        protected ViewModelBase(T model)
        {
            Model = model;
        }
    }
}