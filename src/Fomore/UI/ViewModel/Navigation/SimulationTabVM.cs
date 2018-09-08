using System.Windows;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Simulation";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private EnvironmentVM selectedEnvironment;
        private MovementPatternVM selectedMovementPattern;
        private CreatureVM selectedCreature;

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                selectedCreature = value;
                OnPropertyChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
                StartSimulationCommand.OnCanExecuteChanged();
            }
        }

        public MovementPatternVM SelectedMovementPattern
        {
            get => selectedMovementPattern;
            set
            {
                if (Equals(value, selectedMovementPattern)) return;
                selectedMovementPattern = value;
                OnPropertyChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
                StartSimulationCommand.OnCanExecuteChanged();
            }
        }

        public EnvironmentVM SelectedEnvironment
        {
            get => selectedEnvironment;
            set
            {
                if (Equals(value, selectedEnvironment)) return;
                selectedEnvironment = value;
                OnPropertyChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
                StartSimulationCommand.OnCanExecuteChanged();
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------
        public DelegateCommand ResetSelectionCommand { get; }
        public DelegateCommand StartSimulationCommand { get; }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
        }

        private void StartSimulationAction(object obj)
        {
            MessageBox.Show("The simulation has started...\n\nParameters:\nCreature:\t\t\t" + SelectedCreature.Name + "\nMovement Pattern:\t" + SelectedMovementPattern.Name + "\nEnvironment:\t\t" + SelectedEnvironment.Name, "Simulation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public SimulationTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            // Init commands
            ResetSelectionCommand = new DelegateCommand(ResetSelectionAction,
                                                        o => SelectedCreature != null ||
                                                             SelectedMovementPattern != null ||
                                                             SelectedEnvironment != null);
            StartSimulationCommand = new DelegateCommand(StartSimulationAction,
                                                         o => SelectedCreature != null &&
                                                              SelectedMovementPattern != null &&
                                                              SelectedEnvironment != null);
        }

        public override void OnSelect(object obj)
        {
            if (obj is CreatureVM vm)
                SelectedCreature = vm;
        }
    }
}