// : Fomore/UI/Change.cs (2018/08/09)

using Core;

namespace Fomore.UI.ViewModel.CreatureEditor.Changes
{
    /// <summary>
    /// This class represents a change made inside of the CreatureStructureEditor. Every change can be applied and undone.
    /// </summary>
    public interface IChange
    {
        void Apply();

        void Undo();
    }
}