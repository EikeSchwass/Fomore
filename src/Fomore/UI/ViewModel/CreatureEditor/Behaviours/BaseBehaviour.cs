using System.Globalization;
using System.Windows.Input;
using System.Windows.Media;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class BaseBehaviour : ViewModelBase, IHasInputBinding
    {
        public abstract ImageSource Image { get; }

        public abstract BehaviourType BehaviourType { get; }

        public DelegateCommand Command { get; protected set; }

        protected abstract InputGesture InputGesture { get; }

        public string ToolTip =>
            $"{ToString()} ({(InputGesture as KeyGesture)?.GetDisplayStringForCulture(CultureInfo.CurrentCulture)})";

        protected BaseBehaviour()
        {
            Command = new DelegateCommand(o => OnInvoked(Keyboard.Modifiers), o=>CanExecute());
        }

        protected void OnCanExecuteChanged() => Command.OnCanExecuteChanged();

        protected virtual bool CanExecute() => true;

        public virtual void OnInvoked(ModifierKeys modifierKeys) { }

        /// <inheritdoc />
        public InputBinding GetInputBinding() => new InputBinding(Command, InputGesture ?? new KeyGesture(Key.None));

        /// <inheritdoc />
        public abstract override string ToString();
    }

    
}