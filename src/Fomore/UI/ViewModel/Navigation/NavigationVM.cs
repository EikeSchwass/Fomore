using System.Collections.ObjectModel;
using Fomore.UI.ViewModel.Application;
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
                TabNavigationVM.OnCanExecuteChanged();
            }
        }

        /// <summary>
        /// A collection of all the tabs of the main window.
        /// </summary>
        public ObservableCollection<TabPageVM> TabCollection { get; }

        public TabNavigationVM TabNavigationVM { get; }

        private CreatureTabVM CreatureTab { get; }
        private EnvironmentTabVM EnvironmentTab { get; }
        private TrainingTabVM TrainingTab { get; }
        private SimulationTabVM SimulationTab { get; }

        public NavigationVM(EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = new TabNavigationVM(new DelegateCommand(SwitchToCreatureTab, o => !ReferenceEquals(SelectedTab, CreatureTab)),
                                                  new DelegateCommand(SwitchToEnvironmentTab, o => !ReferenceEquals(SelectedTab, EnvironmentTab)),
                                                  new DelegateCommand(SwitchToTrainingTab, o => !ReferenceEquals(SelectedTab, TrainingTab)),
                                                  new DelegateCommand(SwitchToSimulationTab, o => !ReferenceEquals(SelectedTab, SimulationTab)));

            CreatureTab = new CreatureTabVM(TabNavigationVM, entitiesStorage);
            EnvironmentTab = new EnvironmentTabVM(entitiesStorage);
            TrainingTab = new TrainingTabVM();
            SimulationTab = new SimulationTabVM(entitiesStorage);

            TabCollection = new ObservableCollection<TabPageVM> { CreatureTab, EnvironmentTab, TrainingTab, SimulationTab };
        }

        private void SwitchToSimulationTab(object obj)
        {
            SelectedTab = SimulationTab;
            SelectedTab.OnSelect(obj);
        }

        private void SwitchToTrainingTab(object obj)
        {
            SelectedTab = TrainingTab;
            SelectedTab.OnSelect(obj);
        }

        private void SwitchToEnvironmentTab(object obj)
        {
            SelectedTab = EnvironmentTab;
            SelectedTab.OnSelect(obj);
        }

        private void SwitchToCreatureTab(object obj)
        {
            SelectedTab = CreatureTab;
            SelectedTab.OnSelect(obj);
        }
    }

    public class TabNavigationVM : ViewModelBase
    {
        public DelegateCommand SwitchToCreatureTabCommand { get; }
        public DelegateCommand SwitchToEnvironmentTabCommand { get; }
        public DelegateCommand SwitchToTrainingTabCommand { get; }
        public DelegateCommand SwitchToSimulationTabCommand { get; }

        /// <inheritdoc />
        public TabNavigationVM(DelegateCommand switchToCreatureTabCommand, DelegateCommand switchToEnvironmentTabCommand, DelegateCommand switchToTrainingTabCommand, DelegateCommand switchToSimulationTabCommand)
        {
            SwitchToCreatureTabCommand = switchToCreatureTabCommand;
            SwitchToEnvironmentTabCommand = switchToEnvironmentTabCommand;
            SwitchToTrainingTabCommand = switchToTrainingTabCommand;
            SwitchToSimulationTabCommand = switchToSimulationTabCommand;
        }

        public void OnCanExecuteChanged()
        {
            SwitchToCreatureTabCommand.OnCanExecuteChanged();
            SwitchToEnvironmentTabCommand.OnCanExecuteChanged();
            SwitchToTrainingTabCommand.OnCanExecuteChanged();
            SwitchToSimulationTabCommand.OnCanExecuteChanged();
        }
    }
}