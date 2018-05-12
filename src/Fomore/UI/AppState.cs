using Fomore.UI.ViewModel;

namespace Fomore.UI
{
    public class AppState : ViewModelBase
    {
        private ContentViewModelBase currentViewModel = new WelcomeScreenViewModel();

        public ContentViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                if (Equals(value, currentViewModel)) return;
                currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}