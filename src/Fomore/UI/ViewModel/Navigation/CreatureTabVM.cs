using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "New Creature";

        public EntityStorageVM EntitiesStorage { get; }

        private CreatureVM selectedCreature;
        private TabNavigationVM tabNavigationVM;

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

        public DelegateCommand SimulateCreatureCommand { get; }

        public TabNavigationVM TabNavigationVM { get; }

        public string EnterName => "Enter Name*:";
        public string Description => "Description";
        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }

        public CreatureTabVM(TabNavigationVM navigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            EntitiesStorage = entitiesStorage;
            SimulateCreatureCommand = new DelegateCommand(SimulateCreatureAction, o => true);
        }

        private void SimulateCreatureAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(null);
        }
    }
}