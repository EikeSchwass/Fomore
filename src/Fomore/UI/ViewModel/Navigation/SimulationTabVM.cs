using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
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

        private TabNavigationVM tabNavigationVM;
        private CreatureVM selectedCreature;

        public TabNavigationVM TabNavigationVM
        {
            get => tabNavigationVM;
            set
            {
                if (Equals(value, tabNavigationVM)) return;
                tabNavigationVM = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM(EntityStorageVM entitiesStorage)
        {
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