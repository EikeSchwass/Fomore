using Core.Creatures;

namespace Fomore.UI.ViewModel
{
    public class BoneModelVM : ViewModelBase
    {
        public BoneModelVM(BoneModel boneModel)
        {
            BoneModel = boneModel;
        }
        public BoneModel BoneModel { get; }
    }
}