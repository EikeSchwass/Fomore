using Core.Creatures;

namespace Fomore.UI.ViewModel
{
    public class BoneVM : ViewModelBase
    {
        public Bone Bone { get; }

        public BoneVM(Bone bone)
        {
            Bone = bone;
        }
    }
}