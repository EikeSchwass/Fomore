using Fomore.UI.ViewModel.CreatureEditor;

namespace Fomore.UI.ViewModel.Helper
{
    public class ChangeOperation : IOperation<CreatureStructureEditorCanvasVM>
    {
        private OperationDelegate<CreatureStructureEditorCanvasVM> Operation { get; }
        private OperationDelegate<CreatureStructureEditorCanvasVM> InverseOperation { get; }

        public ChangeOperation(OperationDelegate<CreatureStructureEditorCanvasVM> operation,
                               OperationDelegate<CreatureStructureEditorCanvasVM> inverseOperation)
        {
            Operation = operation;
            InverseOperation = inverseOperation;
        }

        public void PerformOperation(CreatureStructureEditorCanvasVM entity)
        {
            Operation(entity);
        }

        /// <inheritdoc />
        public void Undo(CreatureStructureEditorCanvasVM entity)
        {
            InverseOperation(entity);
        }
    }
}