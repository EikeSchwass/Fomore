using Fomore.UI.ViewModel;

namespace Fomore.UI
{
    public class AppState : ViewModelBase
    {
        private ViewViewModelBase currentViewModel;
        
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

        public AppState()
        {
            CurrentViewModel=new WelcomeScreenVM(this);
        }


        public CreatureCollectionVM CreatureCollection { get; } = new CreatureCollectionVM();
        public ScenarioCollectionVM ScenarioCollection { get; } = new ScenarioCollectionVM();
    }
}