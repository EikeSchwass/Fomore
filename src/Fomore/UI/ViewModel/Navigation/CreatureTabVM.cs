using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.CreatureEditor;
using Fomore.UI.ViewModel.Data;
using Fomore.UI.Views.Windows;

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

        public ICommand NewCreature { get; }
        public ICommand TrainCommand { get; }
        public ICommand SimulateCommand { get; }
        public ICommand EditCreature { get; }

        private void NewCreatureAction(object obj)
        {
            var creature = new Creature() {Name = "New Creature", Description = "No description available..."};
            var creatureVM = new CreatureVM(creature);
            EntitiesStorage.AddCreatureCommand.Execute(creatureVM);
            SelectedCreature = creatureVM;
        }

        private void TrainAction(object obj)
        {
            TabNavigationVM.SwitchToTrainingTabCommand.Execute(obj);
        }

        private void SimulateAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(obj);
        }

        private void EditAction(object obj)
        {
            if (obj is CreatureVM creature)
            {
                var creatureStructureEditor = new CreatureStructureEditor {DataContext = new CreatureEditorVM(creature)};
                creatureStructureEditor.Show();
            }
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public CreatureTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            NewCreature = new DelegateCommand(NewCreatureAction, o => true);
            TrainCommand = new DelegateCommand(TrainAction, o => true);
            SimulateCommand = new DelegateCommand(SimulateAction, o => true);
            EditCreature = new DelegateCommand(EditAction, o => true);
        }
    }
}