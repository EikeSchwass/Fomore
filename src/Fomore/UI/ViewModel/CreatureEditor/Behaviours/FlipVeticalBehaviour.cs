using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public class FlipVeticalBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override ImageSource Image { get; } = new BitmapImage(new Uri("/assets/images/flipvertical.png", UriKind.Relative));

        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        /// <inheritdoc />
        protected override InputGesture InputGesture { get; } = null;

        /// <inheritdoc />
        public override string ToString() => "Flip Vertical";

        /// <inheritdoc />
        public override void OnInvoked(CreatureEditorPanelVM parameter, ModifierKeys modifierKeys)
        {
            base.OnInvoked(parameter, modifierKeys);

            var changeOperation = new ChangeOperation(c =>
                                                      {
                                                          if (!c.Creature.CreatureStructureVM.JointCollectionVM.Any())
                                                              return;
                                                          var creatureVM = c.Creature;

                                                          var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
                                                          double maxX = jointCollectionVM.Max(j => j.Position.X);
                                                          double minX = jointCollectionVM.Min(j => j.Position.X);
                                                          double maxY = jointCollectionVM.Max(j => j.Position.Y);
                                                          double minY = jointCollectionVM.Min(j => j.Position.Y);
                                                          var center = new Vector2(minX + (maxX - minX) / 2, minY + (maxY - minY) / 2);
                                                          foreach (var jointVM in jointCollectionVM)
                                                          {
                                                              double x = jointVM.Position.X;
                                                              x = -(x - center.X) + center.X;
                                                              jointVM.Position = new Vector2(x, jointVM.Position.Y);
                                                          }
                                                      },
                                                      c =>
                                                      {
                                                          if (!c.Creature.CreatureStructureVM.JointCollectionVM.Any())
                                                              return;
                                                          var creatureVM = c.Creature;

                                                          var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
                                                          double maxX = jointCollectionVM.Max(j => j.Position.X);
                                                          double minX = jointCollectionVM.Min(j => j.Position.X);
                                                          double maxY = jointCollectionVM.Max(j => j.Position.Y);
                                                          double minY = jointCollectionVM.Min(j => j.Position.Y);
                                                          var center = new Vector2(minX + (maxX - minX) / 2, minY + (maxY - minY) / 2);
                                                          foreach (var jointVM in jointCollectionVM)
                                                          {
                                                              double x = jointVM.Position.X;
                                                              x = -(x - center.X) + center.X;
                                                              jointVM.Position = new Vector2(x, jointVM.Position.Y);
                                                          }
                                                      });
            parameter.CreatureStructureEditorCanvasVM.HistoryStack.AddOperation(changeOperation);
        }
    }
}