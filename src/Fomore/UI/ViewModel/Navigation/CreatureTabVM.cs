using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        private CreatureVM selectedCreature;

        /// <inheritdoc />
        public override string Header => "New Creature";

        public TabNavigationVM TabNavigationVM { get; }

        public EntityStorageVM EntitiesStorage { get; }

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

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }

        public CreatureTabVM(TabNavigationVM navigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            EntitiesStorage = entitiesStorage;
            SimulateCreatureCommand = new DelegateCommand(SimulateCreatureAction, o => true);
        }

        public DelegateCommand SimulateCreatureCommand { get; }
        
        private void SimulateCreatureAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(null);
        }
    }
}