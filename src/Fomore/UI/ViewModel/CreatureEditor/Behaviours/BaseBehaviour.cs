using System.Globalization;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class BaseBehaviour : ViewModelBase, IHasInputBinding
    {
        public static Brush BehaviourBrush = new SolidColorBrush(Color.FromRgb(221, 221, 221));

        public abstract ImageSource Image { get; }

        public abstract BehaviourType BehaviourType { get; }

        public DelegateCommand Command { get; protected set; }

        protected abstract InputGesture InputGesture { get; }

        public string ToolTip => $"{ToString()} ({(InputGesture as KeyGesture)?.GetDisplayStringForCulture(CultureInfo.CurrentCulture)})";

        protected BaseBehaviour()
        {
            Command = new DelegateCommand<CreatureEditorPanelVM>(vm => OnInvoked(vm, Keyboard.Modifiers), CanExecute);
        }

        protected void OnCanExecuteChanged() => Command.OnCanExecuteChanged();

        protected virtual bool CanExecute(CreatureEditorPanelVM parameter) => true;

        public virtual void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys) { }

        /// <inheritdoc />
        public InputBinding GetInputBinding() => new InputBinding(Command, InputGesture ?? new KeyGesture(Key.None));

        /// <inheritdoc />
        public abstract override string ToString();
    }

    public enum BehaviourType
    {
        Clipboard,
        Storage,
        History,
        Operations
    }
}