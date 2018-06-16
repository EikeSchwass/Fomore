using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreateCreatureDialogVM : ViewModelBase
    {
        private Visibility visibility = Visibility.Hidden;
        private string creatureDescription;
        private string creatureName;
        public  ICollection<Creature> creatures;

        public ICollection<Creature> getCreatures { get { return creatures; } }

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

        public CreateCreatureDialogVM()
        {
            CreateCreatureCommand = new DelegateCommand(CreateCreature, CanCreateCeature);
            CancelCreatureCreationCommand = new StubCommand();
            creatures = new List<Creature>();
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

            creatures.Add(creature);
        }
    }
}
