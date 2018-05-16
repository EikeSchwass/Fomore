using Fomore.UI.ViewModel;

namespace Fomore.UI
{
    public class AppState : ViewModelBase
    {
        private MessageBoxVM messageBox;

        public ViewViewModelNavigator ViewModelNavigator { get; } = new ViewViewModelNavigator();

        public MessageBoxVM MessageBox
        {
            get => messageBox;
            set
            {
                if (Equals(value, messageBox)) return;
                messageBox = value;
                OnPropertyChanged();
            }
        }

        public CreatureCollectionVM CreatureCollection { get; } = new CreatureCollectionVM();
        public ScenarioCollectionVM ScenarioCollection { get; } = new ScenarioCollectionVM();
        public EnvironmentCollectionVM EnvironmentCollection { get; } = new EnvironmentCollectionVM();

        public AppState()
        {
            MessageBox = new MessageBoxVM();
            ViewModelNavigator.PushView(new WelcomeScreenVM());
        }
    }
}