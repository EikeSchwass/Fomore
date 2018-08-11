using System.Linq;
using Core;

namespace Fomore.UI.ViewModel.CreatureEditor.Behaviours
{
    public abstract class RotateBehaviour : BaseBehaviour
    {
        /// <inheritdoc />
        public override BehaviourType BehaviourType { get; } = BehaviourType.Operations;

        protected void RotateCreatureStructure(CreatureEditorVM creatureEditorPanelVM, double angleInRadians)
        {
        //     var creatureVM = creatureEditorPanelVM.HistoryStack.Current.Clone();
        //
        //     var jointCollectionVM = creatureVM.CreatureStructureVM.JointCollectionVM;
        //     var center = new Vector2(jointCollectionVM.Average(j => j.Position.X), jointCollectionVM.Average(j => j.Position.Y));
        //
        //     foreach (var jointVM in jointCollectionVM) jointVM.Position = jointVM.Position.RotateAround(center, angleInRadians);
        //
        //     // Move Horizontically in view
        //     {
        //         double min = jointCollectionVM.Min(j => j.Position.X) - 10;
        //         if (min < 0)
        //         {
        //             foreach (var jointVM in jointCollectionVM)
        //                 jointVM.Position = new Vector2(jointVM.Position.X - min, jointVM.Position.Y);
        //         }
        //
        //         double max = jointCollectionVM.Max(j => j.Position.X) + 10;
        //         if (max > CreatureStructureEditorCanvasVM.CanvasWidth)
        //         {
        //             foreach (var jointVM in jointCollectionVM)
        //             {
        //                 jointVM.Position = new Vector2(jointVM.Position.X - (max - CreatureStructureEditorCanvasVM.CanvasWidth),
        //                                                jointVM.Position.Y);
        //             }
        //         }
        //     }
        //
        //     // Move Vertically in view
        //     {
        //         double min = jointCollectionVM.Min(j => j.Position.Y) - 10;
        //         if (min < 0)
        //         {
        //             foreach (var jointVM in jointCollectionVM) jointVM.Position = new Vector2(jointVM.Position.X, jointVM.Position.Y - min);
        //         }
        //
        //         double max = jointCollectionVM.Max(j => j.Position.Y) + 10;
        //         if (max > CreatureStructureEditorCanvasVM.CanvasHeight)
        //         {
        //             foreach (var jointVM in jointCollectionVM)
        //             {
        //                 jointVM.Position = new Vector2(jointVM.Position.X,
        //                                                jointVM.Position.Y - (max - CreatureStructureEditorCanvasVM.CanvasHeight));
        //             }
        //         }
        //     }
        //
        //     creatureEditorPanelVM.HistoryStack.NewEntry(creatureVM);
        }
    }
}