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

        /// <inheritdoc />
        public override string Header => "New Creature";

        public string EnterName => "Enter Name*:";
        public string Description => "Description";
        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

        private CreateCreatureDialogVM cretureCreateCreatureDialogVM;

        public CreatureTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage; 
             ShowCreatureCreationDialogCommand = new DelegateCommand(ShowCreatureCreationDialog, o => true);
            HideCreatureCreationDialogCommand = new DelegateCommand(HideCreatureCreationDialog, o => true);
            CreateCreatureDialogVM = new CreateCreatureDialogVM(EntitiesStorage);
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

        public ICommand DeleteCreatureCommand { get; }
    }
}