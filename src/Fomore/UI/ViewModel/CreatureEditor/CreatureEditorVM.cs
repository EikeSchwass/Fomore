// Eike Stein: Fomore/UI/CreatureEditorVM.cs (2018/06/12)

using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreatureEditorVM : ViewModelBase
    {
        public CreatureVM OriginalCreature { get; }

        public CreatureEditorPanelVM CreatureEditorPanelVM { get; }

        public CreatureEditorVM(CreatureVM creatureVM)
        {
            OriginalCreature = creatureVM;
            CreatureEditorPanelVM = new CreatureEditorPanelVM(OriginalCreature.Clone());
            CreatureEditorPanelVM.SaveRequested += (o, c) => SaveCreature(c);
        }

        public void SaveCreature(CreatureVM creatureVM)
        {
            var original = OriginalCreature;
            original.CreatureStructureVM.BoneCollectionVM.Clear();
            foreach (var boneVM in creatureVM.CreatureStructureVM.BoneCollectionVM)
            {
                original.CreatureStructureVM.BoneCollectionVM.Add(boneVM);
            }
            original.CreatureStructureVM.JointCollectionVM.Clear();
            foreach (var jointVM in creatureVM.CreatureStructureVM.JointCollectionVM)
            {
                original.CreatureStructureVM.JointCollectionVM.Add(jointVM);
            }
        }
    }
}