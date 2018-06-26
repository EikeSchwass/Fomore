using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Environment = Core.Environment;

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

        public DelegateCommand CreateStuffCommand { get; }
        public DelegateCommand ResetSelectionCommand { get; }
        public DelegateCommand StartSimulationCommand { get; }

        private void CreateCreaturesAction(object obj)
        {
            var dinosaur = new CreatureVM(new Creature()) { Name = "Dinosaur", Description = "Dangerous like me when I'm hungry" };
            var dog = new CreatureVM(new Creature()) { Name = "Dog", Description = "My name is Rex" };
            var cat = new CreatureVM(new Creature()) { Name = "Cat", Description = "Miau miau miau..." };

            EntitiesStorage.AddCreatureCommand.Execute(dinosaur);
            EntitiesStorage.AddCreatureCommand.Execute(dog);
            EntitiesStorage.AddCreatureCommand.Execute(cat);

            dog.MovementPatternCollectionVM.AddItemCommand.Execute(new MovementPatternVM(new MovementPattern())
            {
                Name = "Dog walks on Earth",
                Iterations = 1337
            });
            dog.MovementPatternCollectionVM.AddItemCommand.Execute(new MovementPatternVM(new MovementPattern())
            {
                Name = "Dog runs on Earth",
                Iterations = 42
            });
            dog.MovementPatternCollectionVM.AddItemCommand.Execute(new MovementPatternVM(new MovementPattern())
            {
                Name = "Dog walks on Moon",
                Iterations = 123
            });

            var earth = new EnvironmentVM(new Environment())
            {
                Name = "Earth",
                Description = "Our wonderful world",
                Gravity = 9.81,
                Friction = 3
            };
            var moon = new EnvironmentVM(new Environment())
            {
                Name = "Moon",
                Description = "The lovely ball surrounding our home planet",
                Gravity = 1.62,
                Friction = 0
            };

            EntitiesStorage.AddEnvironmentCommand.Execute(earth);
            EntitiesStorage.AddEnvironmentCommand.Execute(moon);
        }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
        }

        private void StartSimulationAction(object obj)
        {
            SelectedMovementPattern.Iterations++;
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public SimulationTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            // Init commands
            CreateStuffCommand = new DelegateCommand(CreateCreaturesAction, o => true);
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