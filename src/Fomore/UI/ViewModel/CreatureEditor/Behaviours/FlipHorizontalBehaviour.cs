using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class FlipHorizontalBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/fliphorizontal.png", UriKind.Relative));

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = null;

        /// <inheritdoc />
        public override string ToString() => "Flip Horizontal";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);
            if (!parameter.HistoryStack.Current.CreatureStructureVM.JointCollectionVM.Any())
                return;
            var creatureVM = parameter.HistoryStack.Current.Clone();

            var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
            var center = new Vector2(jointCollectionVM.Average(j => j.Position.X),
                                     jointCollectionVM.Average(j => j.Position.Y));

            foreach (var jointVM in jointCollectionVM)
            {
                double y = jointVM.Position.Y;
                y = -(y - center.Y) + center.Y;
                jointVM.Position = new Vector2(jointVM.Position.X, y);
            }


            // Move Horizontically in view
            {
                double min = jointCollectionVM.Min(j => j.Position.X) - 10;
                if (min < 0)
                {
                    foreach (var jointVM in jointCollectionVM)
                        jointVM.Position = new Vector2(jointVM.Position.X - min, jointVM.Position.Y);
                }

                double max = jointCollectionVM.Max(j => j.Position.X) + 10;
                if (max > CreatureStructureEditorCanvasVM.CanvasWidth)
                {
                    foreach (var jointVM in jointCollectionVM)
                    {
                        jointVM.Position = new Vector2(jointVM.Position.X - (max - CreatureStructureEditorCanvasVM.CanvasWidth),
                                                       jointVM.Position.Y);
                    }
                }
            }

            // Move Vertically in view
            {
                double min = jointCollectionVM.Min(j => j.Position.Y) - 10;
                if (min < 0)
                {
                    foreach (var jointVM in jointCollectionVM)
                    {
                        jointVM.Position = new Vector2(jointVM.Position.X, jointVM.Position.Y - min);
                    }
                }

                double max = jointCollectionVM.Max(j => j.Position.Y) + 10;
                if (max > CreatureStructureEditorCanvasVM.CanvasHeight)
                {
                    foreach (var jointVM in jointCollectionVM)
                    {
                        jointVM.Position = new Vector2(jointVM.Position.X,
                                                       jointVM.Position.Y - (max - CreatureStructureEditorCanvasVM.CanvasHeight));
                    }
                }
            }

            parameter.HistoryStack.NewEntry(creatureVM);
        }
    }
}