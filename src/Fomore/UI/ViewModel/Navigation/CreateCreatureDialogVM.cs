using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public EntityStorageVM EntitiesStorage { get; }
        private Visibility visibility = Visibility.Hidden;
        private string creatureDescription;
        private string creatureName;
        private bool isOpen;

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

        public bool IsOpen
        {
            get => isOpen;
            set { isOpen = value; }
        }

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

        private bool CanCreateCeature(object arg)
        {
            return true;
        }

        private void CreateCreature(object obj)
        {
            Visibility = Visibility.Hidden;
            Creature creature = new Creature();
            creature.CreatureName = this.CreatureName;
            creature.CreatureDescription = this.creatureDescription;
            creature.isOpen = this.isOpen;
            CreatureVM creatureVM = new CreatureVM(creature);
            EntitiesStorage.AddCreatureCommand.Execute(creatureVM);
        }
    }
}
