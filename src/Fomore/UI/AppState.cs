using Fomore.UI.ViewModel;
using Fomore.UI.Views.Controls;

namespace Fomore.UI
{
    public class AppState : ViewModelBase
    {
        private ViewViewModelBase currentViewModel;
        private MessageBoxParameters messageBoxParameters;

        public ViewViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                if (Equals(value, currentViewModel)) return;
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public CreatureCollectionVM CreatureCollection { get; } = new CreatureCollectionVM();
        public ScenarioCollectionVM ScenarioCollection { get; } = new ScenarioCollectionVM();

        public MessageBoxParameters MessageBoxParameters
        {
            get => messageBoxParameters;
            set
            {
                if (Equals(value, messageBoxParameters)) return;
                messageBoxParameters = value;
                OnPropertyChanged();
            }
        }

        public AppState()
        {
            CurrentViewModel = new WelcomeScreenVM(this);
        }
    }
}