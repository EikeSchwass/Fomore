using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class BaseBehaviour : ViewModelBase
    {
        public abstract ImageSource Image { get; }

        public abstract BehaviourType BehaviourType { get; }

        public DelegateCommand Command { get; }

        protected BaseBehaviour()
        {
            Command = new DelegateCommand(OnInvoked, CanExecute);
        }

        protected void OnCanExecuteChanged() => Command.OnCanExecuteChanged();

        protected virtual bool CanExecute(object arg) => true;

        public virtual void OnInvoked(object parameter) { }
    }

    public enum BehaviourType
    {
        Clipboard,
        Storage,
        History,
        Operations
    }
}