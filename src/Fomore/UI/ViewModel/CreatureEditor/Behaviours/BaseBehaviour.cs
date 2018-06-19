using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class BaseBehaviour : ViewModelBase
    {
        public abstract ImageSource Image { get; }

        public abstract BehaviourType BehaviourType { get; }

        public DelegateCommand<CreatureEditorPanelVM> Command { get; }

        protected BaseBehaviour()
        {
            Command = new DelegateCommand<CreatureEditorPanelVM>(OnInvoked, CanExecute);
        }

        protected void OnCanExecuteChanged() => Command.OnCanExecuteChanged();

        protected virtual bool CanExecute(CreatureEditorPanelVM parameter) => true;

        public virtual void OnInvoked(CreatureEditorPanelVM parameter) { }
    }

    public enum BehaviourType
    {
        Clipboard,
        Storage,
        History,
        Operations
    }
}