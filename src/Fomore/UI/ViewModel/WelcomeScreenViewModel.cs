namespace Fomore.UI.ViewModel
{
    public class WelcomeScreenViewModel : ContentViewModelBase
    {
        private BoneLibraryVM boneLibrary;

        public BoneLibraryVM BoneLibrary
        {
            get => boneLibrary;
            set
            {
                if (Equals(value, boneLibrary)) return;
                boneLibrary = value;
                OnPropertyChanged();
            }
        }
    }
}