using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        public EntityStorageVM EntitiesStorage { get; }

        /// <inheritdoc />
        public override string Header => "New Creature";

        public DelegateCommand SimulateCreatureCommand { get; }

        public TabNavigationVM TabNavigationVM { get; }

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }

        public CreatureTabVM(TabNavigationVM navigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = navigationVM;
            EntitiesStorage = entitiesStorage;
        }
    }
}