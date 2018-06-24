using System;
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
        public EntityStorageVM EntitiesStorage { get; }

        public TabNavigationVM TabNavigationVM { get; }

        /// <inheritdoc />
        public override string Header => "New Creature";

        public string EnterName => "Enter Name*:";
        public string Description => "Description";
        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

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

        public List<string> CreatureList;
        

        private CreateCreatureDialogVM cretureCreateCreatureDialogVM;
        private CreatureVM selectedCreature;

        public CreatureTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage; 
             ShowCreatureCreationDialogCommand = new DelegateCommand(ShowCreatureCreationDialog, o => true);
            HideCreatureCreationDialogCommand = new DelegateCommand(HideCreatureCreationDialog, o => true);
            CreateCreatureDialogVM = new CreateCreatureDialogVM(EntitiesStorage);
            TabNavigationVM = tabNavigationVM;
            SimulateCommand = new DelegateCommand(SimulateAction, o => true);
        }

        private void SimulateAction(object obj)
        {
            TabNavigationVM.SwitchToSimulationTabCommand.Execute(obj);
        }
            DeleteCreatureCommand = new DelegateCommand(DeleteCreature, o => true);

        }    

        private void HideCreatureCreationDialog(object obj)
        {
            CreateCreatureDialogVM.Visibility = Visibility.Hidden;
        }

        private void ShowCreatureCreationDialog(object obj)
        {
            CreateCreatureDialogVM.Visibility = Visibility.Visible;
        }

        public CreateCreatureDialogVM CreateCreatureDialogVM
        {
            get => cretureCreateCreatureDialogVM;
            set
            {
                if (Equals(value, cretureCreateCreatureDialogVM)) return;
                cretureCreateCreatureDialogVM = value;
                OnPropertyChanged();
            }
        }

        private void DeleteCreature(object obj)
        {
            EntitiesStorage.RemoveCreatureCommand.Execute((CreatureVM)obj);
        }
        public ICommand ShowCreatureCreationDialogCommand { get; }

        public ICommand HideCreatureCreationDialogCommand { get; }

        public DelegateCommand SimulateCommand { get; }

        public ICommand DeleteCreatureCommand { get; }
    }
}