using Fomore.UI.ViewModel.Application;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        public EntityStorageVM EntitiesStorage { get; }

        /// <inheritdoc />
        public override string Header => "New Creature";

        public EntityStorageVM EntitiesStorage { get; }

        public DelegateCommand SimulateCreatureCommand { get; }

        public TabNavigationVM TabNavigationVM { get; }

        public SimulationTabVM SimulationTabVM { get; }

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }

        public CreatureTabVM(TabNavigationVM navigationVM, SimulationTabVM simulationTab, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            SimulationTabVM = simulationTab;
            EntitiesStorage = entitiesStorage;
        }
    }
}