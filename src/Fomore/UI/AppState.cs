using Fomore.UI.ViewModel;

namespace Fomore.UI
{
    public class AppState : ViewModelBase
    {
        private ViewModelBase currentViewModel = new ImportBonesViewModel();

        public ViewModelBase CurrentViewModel
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