using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FontAwesome.WPF;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class UndoBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Reply, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.History;
    }

    public class RedoBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.MailForward, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.History;
    }

    public class CopyBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Copy, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;
    }

    public class CutBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Cut, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;
    }

    public class PasteBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Paste, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Clipboard;
    }

    public class RotateLeftBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateLeft, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }

    public class RotateRightBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.RotateRight, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }

    public class SaveBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Save, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Storage;
    }

    public class DeleteBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Trash, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }

    public class ClearBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = ImageAwesome.CreateImageSource(FontAwesomeIcon.Remove, Brushes.Black);

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }

    public class FlipHorizontalBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/fliphorizontal.png", UriKind.Relative));

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }

    public class FlipVeticalBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/flipvertical.png", UriKind.Relative));

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;
    }
}