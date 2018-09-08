using System.Windows;
using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreateCreatureDialogVM : ViewModelBase
    {
        private string creatureDescription;
        private string creatureName;
        private Visibility visibility = Visibility.Hidden;
        public EntityStorageVM EntitiesStorage { get; }

        public string CreatureName
        {
            get => creatureName;
            set
            {
                if (value == creatureName) return;
                creatureName = value;
                OnPropertyChanged();
            }
        }

        public string CreatureDescription
        {
            get => creatureDescription;
            set
            {
                if (value == creatureDescription) return;
                creatureDescription = value;
                OnPropertyChanged();
            }
        }

        public bool IsOpen { get; set; }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                if (value == visibility) return;
                visibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateCreatureCommand { get; }
        public ICommand CancelCreatureCreationCommand { get; }

        public CreateCreatureDialogVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
            CreateCreatureCommand = new DelegateCommand(CreateCreature, CanCreateCeature);
            CancelCreatureCreationCommand = new StubCommand();
        }

        private bool CanCreateCeature(object arg) => true;

        private void CreateCreature(object obj)
        {
            Visibility = Visibility.Hidden;
            var creature = new Creature {Name = CreatureName, Description = creatureDescription};
            var creatureVM = new CreatureVM(creature);
            EntitiesStorage.AddCreatureCommand.Execute(creatureVM);
        }
    }
}