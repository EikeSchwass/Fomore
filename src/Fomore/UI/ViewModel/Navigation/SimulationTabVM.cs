using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

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
            }
        }

        public TabNavigationVM TabNavigationVM { get; }


        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM(TabNavigationVM navigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            EntityStorageVM = entitiesStorage;
            CreateStuffCommand = new DelegateCommand(CreateCreaturesAction, o => true);
        }

        public DelegateCommand CreateStuffCommand { get; }

        private void CreateCreaturesAction(object obj)
        {
            var dinosaur = new CreatureVM(null) { Name = "Dinosaur", Description = "Dangerous like me when I'm hungry" };
            var dog = new CreatureVM(null) {Name = "Dog", Description = "My name is Rex"};
            var cat = new CreatureVM(null) {Name = "Cat", Description = "Miau miau miau..."};

            EntityStorageVM.AddCreatureCommand.Execute(dinosaur);
            EntityStorageVM.AddCreatureCommand.Execute(dog);
            EntityStorageVM.AddCreatureCommand.Execute(cat);

            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null) { Name = "Dog walks on Earth", Description = "1337 iterations"});
            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null) { Name = "Dog runs on Earth", Description = "42 iterations"});
            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null) { Name = "Dog walks on Moon", Description = "123 iterations"});

            cat.AddMovementPatternCommand.Execute(new MovementPatternVM(null) { Name = "Cat walks on Earth", Description = "5 iterations"});
            
            EntityStorageVM.AddEnvironmentCommand.Execute(new EnvironmentVM(null) { Name = "Earth", Description = "Our lovely home planet", Gravity = 9.81 });
            EntityStorageVM.AddEnvironmentCommand.Execute(new EnvironmentVM(null) { Name = "Moon", Description = "The beautiful moon surrounding our earth", Gravity = 1.62 });
        }
    }
}