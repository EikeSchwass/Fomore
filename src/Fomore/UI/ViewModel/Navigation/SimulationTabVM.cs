using System;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class SimulationTabVM : TabPageVM
    {
        public EntityStorageVM EntityStorageVM { get; }

        public TabNavigationVM TabNavigationVM { get; }

        /// <inheritdoc />
        public override string Header => "Simulation";

        public SimulationTabVM(TabNavigationVM navigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            EntityStorageVM = entitiesStorage;
            CreateStuffCommand = new DelegateCommand(CreateCreaturesAction, o => true);
            StartSimulationCommand = new DelegateCommand(StartSimulationAction,
                                                         o => EntityStorageVM.SelectedCreature != null &&
                                                              EntityStorageVM.SelectedMovementPattern != null &&
                                                              EntityStorageVM.SelectedEnvironment != null);
            EntityStorageVM.EntityDependentCommands.Add(StartSimulationCommand);
        }

        public DelegateCommand CreateStuffCommand { get; }

        private void CreateCreaturesAction(object obj)
        {
            var dinosaur = new CreatureVM(null) {Name = "Dinosaur", Description = "Dinosaurs are a diverse group of reptiles of the clade Dinosauria. They first appeared during the Triassic period, between 243 and 233.23 million years ago, although the exact origin and timing of the evolution of dinosaurs is the subject of active research. They became the dominant terrestrial vertebrates after the Triassic–Jurassic extinction event 201 million years ago; their dominance continued through the Jurassic and Cretaceous periods. Reverse genetic engineering and the fossil record both demonstrate that birds are modern feathered dinosaurs, having evolved from earlier theropods during the late Jurassic Period. As such, birds were the only dinosaur lineage to survive the Cretaceous–Paleogene extinction event 66 million years ago. Dinosaurs can therefore be divided into avian dinosaurs, or birds; and non-avian dinosaurs, which are all dinosaurs other than birds. This article deals primarily with non-avian dinosaurs." };
            var dog = new CreatureVM(null) {Name = "Dog", Description = "My name is Rex"};
            var cat = new CreatureVM(null) {Name = "Cat", Description = "Miau miau miau..."};

            EntityStorageVM.AddCreatureCommand.Execute(dinosaur);
            EntityStorageVM.AddCreatureCommand.Execute(dog);
            EntityStorageVM.AddCreatureCommand.Execute(cat);

            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null)
            {
                Name = "Dog walks on Earth",
                Description = "1337 iterations"
            });
            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null) {Name = "Dog runs on Earth", Description = "42 iterations"});
            dog.AddMovementPatternCommand.Execute(new MovementPatternVM(null) {Name = "Dog walks on Moon", Description = "123 iterations"});

            cat.AddMovementPatternCommand.Execute(new MovementPatternVM(null) {Name = "Cat walks on Earth", Description = "5 iterations"});

            EntityStorageVM.AddEnvironmentCommand.Execute(new EnvironmentVM(null)
            {
                Name = "Earth",
                Description = "Our lovely home planet",
                Gravity = 9.81
            });
            EntityStorageVM.AddEnvironmentCommand.Execute(new EnvironmentVM(null)
            {
                Name = "Moon",
                Description = "The beautiful moon surrounding our earth",
                Gravity = 1.62
            });
        }

        public DelegateCommand StartSimulationCommand { get; }

        private void StartSimulationAction(object obj)
        {
            EntityStorageVM.SelectedMovementPattern.Iterations++;
        }
    }
}