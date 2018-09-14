using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Helper;

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
            if (!parameter.Creature.CreatureStructureVM.JointCollectionVM.Any())
                return;
            var creatureVM = parameter.Creature;

            var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
            double maxX = jointCollectionVM.Max(j => j.Position.X);
            double minX = jointCollectionVM.Min(j => j.Position.X);
            double maxY = jointCollectionVM.Max(j => j.Position.Y);
            double minY = jointCollectionVM.Min(j => j.Position.Y);
            var center = new Vector2(minX + (maxX - minX) / 2, minY + (maxY - minY) / 2);

            var changeOperation = new ChangeOperation(c =>
                                                      {
                                                          var jointCollection = c.Creature.CreatureStructureVM.JointCollectionVM;
                                                          foreach (var jointVM in jointCollection)
                                                          {
                                                              double y = jointVM.Position.Y;
                                                              y = -(y - center.Y) + center.Y;
                                                              jointVM.Position = new Vector2(jointVM.Position.X, y);
                                                          }
                                                      },
                                                      c =>
                                                      {
                                                          var jointCollection = c.Creature.CreatureStructureVM.JointCollectionVM;
                                                          foreach (var jointVM in jointCollection)
                                                          {
                                                              double y = jointVM.Position.Y;
                                                              y = -(y - center.Y) + center.Y;
                                                              jointVM.Position = new Vector2(jointVM.Position.X, y);
                                                          }
                                                      });
            parameter.CreatureStructureEditorCanvasVM.HistoryStack.AddOperation(changeOperation);
        }
    }
}