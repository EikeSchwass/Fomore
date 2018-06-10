using System.Collections.ObjectModel;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Navigation
{
    public class NavigationVM : ViewModelBase
    {
        private TabPageVM selectedTab;

        /// <summary>
        /// The currently selected tab.
        /// </summary>
        public TabPageVM SelectedTab
        {
            get => selectedTab;
            set
            {
                if (Equals(value, selectedTab)) return;
                selectedTab = value;
                OnPropertyChanged();
                SwitchToCreatureTabCommand.OnCanExecuteChanged();
                SwitchToEnvironmentTabCommand.OnCanExecuteChanged();
                SwitchToTrainingTabCommand.OnCanExecuteChanged();
                SwitchToSimulationTabCommand.OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// A collection of all the tabs of the main window.
        /// </summary>
        public ObservableCollection<TabPageVM> TabCollection { get; }

        public DelegateCommand SwitchToCreatureTabCommand { get; }
        public DelegateCommand SwitchToEnvironmentTabCommand { get; }
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
            SwitchToEnvironmentTabCommand = new DelegateCommand(SwitchToEnvironmentTab, o => SelectedTab != EnvironmentTab);
            SwitchToTrainingTabCommand = new DelegateCommand(SwitchToTrainingTab, o => SelectedTab != TrainingTab);
            SwitchToSimulationTabCommand = new DelegateCommand(SwitchToSimulationTab, o => SelectedTab != SimulationTab);

            TabCollection = new ObservableCollection<TabPageVM> { CreatureTab, EnvironmentTab, TrainingTab, SimulationTab };
        }

        /// <summary>
        /// Handler for the <see cref="SwitchToSimulationTabCommand"/>.
        /// </summary>
        /// <param name="obj">Not used right now.</param>
        private void SwitchToSimulationTab(object obj)
        {
            SelectedTab = SimulationTab;
        }

        /// <summary>
        /// Handler for the <see cref="SwitchToTrainingTabCommand"/>.
        /// </summary>
        /// <param name="obj">Not used right now.</param>
        private void SwitchToTrainingTab(object obj)
        {
            SelectedTab = TrainingTab;
        }

        /// <summary>
        /// Handler for the <see cref="SwitchToEnvironmentTabCommand"/>.
        /// </summary>
        /// <param name="obj">Not used right now.</param>
        private void SwitchToEnvironmentTab(object obj)
        {
            SelectedTab = EnvironmentTab;
        }

        /// <summary>
        /// Handler for the <see cref="SwitchToCreatureTabCommand"/>.
        /// </summary>
        /// <param name="obj">Not used right now.</param>
        private void SwitchToCreatureTab(object obj)
        {
            SelectedTab = CreatureTab;
        }
    }
}