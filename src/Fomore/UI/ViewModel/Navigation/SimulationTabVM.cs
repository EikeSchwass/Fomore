using System.Collections.Specialized;
using System.Linq;
using Core.Physics;
using Core.Renderer;
using Core.Training;
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
        private bool simulationRunning;

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

        public bool SimulationRunning
        {
            get => simulationRunning;
            set
            {
                if (value == simulationRunning) return;
                simulationRunning = value;
                OnPropertyChanged();
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------
        public DelegateCommand ResetSelectionCommand { get; }
        public DelegateCommand StartSimulationCommand { get; }
        public DelegateCommand StopSimulationCommand { get; }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
        }

        private void StartSimulationAction(object obj)
        {
            using (var isolation = new Isolated<RemoteRenderer>())
            {
                IsEnabled = false;
                isolation.Value.Render(SelectedCreature.Model, SelectedMovementPattern.Model, SelectedEnvironment.Model);
                IsEnabled = true;
            }
        }

        private void StopSimulationAction(object obj)
        {
            SimulationRunning = false;
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
            StopSimulationCommand = new DelegateCommand(StopSimulationAction, o => true);

            EntitiesStorage.Creatures.CollectionChanged += CreatureStorageChanged;
            EntitiesStorage.Environments.CollectionChanged += EnvironmentStorageChanged;
        }

        private void CreatureStorageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (EntitiesStorage.Creatures.Contains(SelectedCreature))
                return;
            SelectedCreature = null;
            SelectedMovementPattern = null;
        }

        private void EnvironmentStorageChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (EntitiesStorage.Environments.Contains(SelectedEnvironment))
                return;
            SelectedEnvironment = null;
        }

        public override void OnSelect(object obj)
        {
            if (obj is CreatureVM vm)
                SelectedCreature = vm;

            if (obj is CreatureTabVM.CreatureMovementPattern cmp)
            {
                if (cmp.Creature != null)
                    SelectedCreature = cmp.Creature;
                if (cmp.MovementPattern != null)
                    SelectedMovementPattern = cmp.MovementPattern;
            }
        }
    }
}