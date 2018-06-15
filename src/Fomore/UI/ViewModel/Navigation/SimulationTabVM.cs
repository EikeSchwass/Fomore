using System;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Environment = Core.Environment;

namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
        private CreatureVM selectedCreature;
        private MovementPatternVM selectedMovementPattern;
        private EnvironmentVM selectedEnvironment;

        public EntityStorageVM EntityStorageVM { get; }

        public CreatureVM SelectedCreature
        {
            get => selectedCreature;
            set
            {
                if (Equals(value, selectedCreature)) return;
                selectedCreature = value;
                OnPropertyChanged();
                StartSimulationCommand.OnCanExecuteChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
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
                StartSimulationCommand.OnCanExecuteChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
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
                StartSimulationCommand.OnCanExecuteChanged();
                ResetSelectionCommand.OnCanExecuteChanged();
            }
        }

        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM(EntityStorageVM entitiesStorage)
        {
            EntityStorageVM = entitiesStorage;
            CreateStuffCommand = new DelegateCommand(CreateCreaturesAction, o => true);
            StartSimulationCommand = new DelegateCommand(StartSimulationAction,
                                                         o => SelectedCreature != null &&
                                                              SelectedMovementPattern != null &&
                                                              SelectedEnvironment != null);

            ResetSelectionCommand = new DelegateCommand(ResetSelectionAction,
                                                        o => SelectedCreature != null ||
                                                             SelectedMovementPattern != null ||
                                                             SelectedEnvironment != null);
        }

        public DelegateCommand ResetSelectionCommand { get; }

        private void ResetSelectionAction(object obj)
        {
            SelectedCreature = null;
            SelectedMovementPattern = null;
            SelectedEnvironment = null;
        }

        public DelegateCommand CreateStuffCommand { get; }

        private void CreateCreaturesAction(object obj)
        {
            var dinosaur = new CreatureVM(new Creature()) {Name = "Dinosaur", Description = "Dangerous like me when I'm hungry"};
            var dog = new CreatureVM(new Creature()) {Name = "Dog", Description = "My name is Rex"};
            var cat = new CreatureVM(new Creature()) {Name = "Cat", Description = "Miau miau miau..."};

            EntityStorageVM.AddCreatureCommand.Execute(dinosaur);
            EntityStorageVM.AddCreatureCommand.Execute(dog);
            EntityStorageVM.AddCreatureCommand.Execute(cat);

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

            EntityStorageVM.AddEnvironmentCommand.Execute(earth);
            EntityStorageVM.AddEnvironmentCommand.Execute(moon);
        }

        public DelegateCommand StartSimulationCommand { get; }

        private void StartSimulationAction(object obj)
        {
            SelectedMovementPattern.Iterations++;
        }
    }
}