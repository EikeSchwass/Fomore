using System.Linq;
using Core;
using Fomore.UI.ViewModel.Helper;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class RotateBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        protected void RotateCreatureStructure(CreatureEditorPanelVM creatureEditorPanelVM, double angleInRadians)
        {
            if (!creatureEditorPanelVM.Creature.CreatureStructureVM.JointCollectionVM.Any())
                return;
            var creatureVM = creatureEditorPanelVM.Creature;

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
                                                              jointVM.Position = jointVM.Position.RotateAround(center, angleInRadians);
                                                      },
                                                      c =>
                                                      {
                                                          var jointCollection = c.Creature.CreatureStructureVM.JointCollectionVM;
                                                          foreach (var jointVM in jointCollection)
                                                              jointVM.Position = jointVM.Position.RotateAround(center, -angleInRadians);
                                                      });
            creatureEditorPanelVM.CreatureStructureEditorCanvasVM.HistoryStack.AddOperation(changeOperation);
        }
    }
}