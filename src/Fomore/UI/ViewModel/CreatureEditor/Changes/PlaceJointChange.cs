// : Fomore/UI/PlaceJointChange.cs (2018/08/11)

using Core;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor.Changes
{
    public class PlaceJointChange : IChange
    {
        private CreatureStructureVM CreatureStructure { get; }
        public Vector2 Position { get; }
        public JointVM Joint { get; private set; }

        public PlaceJointChange(CreatureStructureVM structure, Vector2 position)
        {
            CreatureStructure = structure;
            Position = position;
        }

        public void Apply()
        {
            Joint = Joint ?? new JointVM(new Joint() {Position = Position});
            CreatureStructure.JointCollectionVM.Add(Joint);
        }

        public void Undo()
        {
            CreatureStructure.JointCollectionVM.Remove(Joint);
        }
    }
}