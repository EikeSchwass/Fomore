using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreatureTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Creature";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private CreatureVM selectedCreature;

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

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------

        public ICommand SimulateCommand { get; }
        public ICommand EditCreature { get; }
        public ICommand TrainCommand { get; }

        private void SimulateAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(obj);
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public CreatureTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            TrainCommand = new StubCommand();
            SimulateCommand = new DelegateCommand(SimulateAction, o => true);
            EditCreature = new StubCommand();
        }
    }
}