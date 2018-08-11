using System;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class DeleteBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = new KeyGesture(Key.Delete);

        protected override bool CanExecute()
        {
            return true;
        }

        public override void OnInvoked(ModifierKeys modifierKeys)
        {
            
        }

        /// <inheritdoc />
        public override string ToString() => "Delete Selection";
    }
}