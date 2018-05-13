using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class WelcomeScreenVM : ContentViewModelBase
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

        public WelcomeScreenVM()
        {
            NewCreatureCommand =
                new DelegateCommand(parameter => App.Instance.AppState.CurrentViewModel = new CreatureEditorVM(),
                                    parameter => true);
        }
    }
}