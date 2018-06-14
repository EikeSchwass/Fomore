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
            CreatureTab = new CreatureTabVM(entitiesStorage);
            EnvironmentTab = new EnvironmentTabVM(entitiesStorage);
            TrainingTab = new TrainingTabVM();
            SimulationTab = new SimulationTabVM(entitiesStorage);

            TabNavigationVM = new TabNavigationVM(new DelegateCommand(SwitchToCreatureTab, o => SelectedTab != CreatureTab),
                                                  new DelegateCommand(SwitchToEnvironmentTab, o => SelectedTab != EnvironmentTab),
                                                  new DelegateCommand(SwitchToTrainingTab, o => SelectedTab != TrainingTab),
                                                  new DelegateCommand(SwitchToSimulationTab, o => SelectedTab != SimulationTab));

            TabCollection = new ObservableCollection<TabPageVM> { CreatureTab, EnvironmentTab, TrainingTab, SimulationTab };

            SimulationTab.TabNavigationVM = TabNavigationVM;
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