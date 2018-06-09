using System.Collections.ObjectModel;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Navigation
{
    public class NavigationVM : ViewModelBase
    {
        private TabPageVM selectedTab;

        public TabPageVM SelectedTab
        {
            get => selectedTab;
            set
            {
                if (Equals(value, selectedTab)) return;
                selectedTab = value;
                OnPropertyChanged();
                SwitchToCreatureTabCommand.OnCanExecuteChanged();
                SwitchToEnvironmentTabommand.OnCanExecuteChanged();
                SwitchToTrainingTabCommand.OnCanExecuteChanged();
                SwitchToSimulationTabCommand.OnCanExecuteChanged();
            }
        }

        public ObservableCollection<TabPageVM> TabCollection { get; }

        public DelegateCommand SwitchToCreatureTabCommand { get; }
        public DelegateCommand SwitchToEnvironmentTabommand { get; }
        public DelegateCommand SwitchToTrainingTabCommand { get; }
        public DelegateCommand SwitchToSimulationTabCommand { get; }

        private CreatureTabVM CreatureTab { get; }
        private EnvironmentTabVM EnvironmentTab { get; }
        private TrainingTabVM TrainingTab { get; }
        private SimulationTabVM SimulationTab { get; }

        public NavigationVM()
        {
            CreatureTab = new CreatureTabVM { NavigationVM = this };
            EnvironmentTab = new EnvironmentTabVM { NavigationVM = this };
            TrainingTab = new TrainingTabVM { NavigationVM = this };
            SimulationTab = new SimulationTabVM { NavigationVM = this };

            SwitchToCreatureTabCommand = new DelegateCommand(SwitchToCreatureTab, o => SelectedTab != CreatureTab);
            SwitchToEnvironmentTabommand = new DelegateCommand(SwitchToEnvironmentTab, o => SelectedTab != EnvironmentTab);
            SwitchToTrainingTabCommand = new DelegateCommand(SwitchToTrainingTab, o => SelectedTab != TrainingTab);
            SwitchToSimulationTabCommand = new DelegateCommand(SwitchToSimulationTab, o => SelectedTab != SimulationTab);

            TabCollection = new ObservableCollection<TabPageVM> { CreatureTab, EnvironmentTab, TrainingTab, SimulationTab };
        }

        private void SwitchToSimulationTab(object obj)
        {
            SelectedTab = SimulationTab;
        }

        private void SwitchToTrainingTab(object obj)
        {
            SelectedTab = TrainingTab;
        }

        private void SwitchToEnvironmentTab(object obj)
        {
            SelectedTab = EnvironmentTab;
        }

        private void SwitchToCreatureTab(object obj)
        {
            SelectedTab = CreatureTab;
        }
    }
}