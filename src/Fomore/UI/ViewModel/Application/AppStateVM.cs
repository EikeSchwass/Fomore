using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.ViewModel.Navigation;

namespace Fomore.UI.ViewModel.Application
{
    public class AppStateVM : ViewModelBase
    {
        public NavigationVM NavigationVM { get; }
        public EntityStorageVM EntitiesStorageVM { get; }
        public ICommand LoadCommand { get; }

        public ICommand CreateSomeStuff { get; }

        public AppStateVM()
        {
            LoadCommand = new DelegateCommand(Load, o => true);
            CreateSomeStuff = new DelegateCommand(CreateCreaturesAction, o => true);
            EntitiesStorageVM = new EntityStorageVM(new EntitiyStorage());
            NavigationVM = new NavigationVM(EntitiesStorageVM);
        }

        private void Load(object obj)
        {
            EntitiesStorageVM.Load();
        }

        // ------------------------------------------------------------

        private void CreateCreaturesAction(object obj)
        {
            var dinosaur = new CreatureVM(new Creature()) { Name = "Dinosaur", Description = "Dangerous like me when I'm hungry" };
            var dog = new CreatureVM(new Creature()) { Name = "Dog", Description = "My name is Rex" };
            var cat = new CreatureVM(new Creature()) { Name = "Cat", Description = "Miau miau miau..." };

            EntitiesStorageVM.AddCreatureCommand.Execute(dinosaur);
            EntitiesStorageVM.AddCreatureCommand.Execute(dog);
            EntitiesStorageVM.AddCreatureCommand.Execute(cat);

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

            EntitiesStorageVM.AddEnvironmentCommand.Execute(earth);
            EntitiesStorageVM.AddEnvironmentCommand.Execute(moon);
        }

    }
}