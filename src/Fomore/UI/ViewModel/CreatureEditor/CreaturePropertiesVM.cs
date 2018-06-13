using System.ComponentModel;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.CreatureEditor
{
    public class CreaturePropertiesVM : ViewModelBase
    {
        public CreatureVM CreatureVM { get; }
        public int BoneCount => CreatureVM.BoneCollectionVM.Count;
        public int JointCount => CreatureVM.JointCollectionVM.Count;

        public CreaturePropertiesVM(CreatureVM creatureVM)
        {
            CreatureVM = creatureVM;
            CreatureVM.PropertyChanged += CreatureUpdated;
        }

        private void CreatureUpdated(object sender, PropertyChangedEventArgs e) { }
    }
}