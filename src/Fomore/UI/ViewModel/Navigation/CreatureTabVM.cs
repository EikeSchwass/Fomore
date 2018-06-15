using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;

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

        public string CreatureName { get; set; }
        public string EnterDescription { get; set; }
        private CreateCreatureDialogVM cretureCreateCreatureDialogVM;

        public CreatureTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
            ShowCreatureCreationDialogCommand = new DelegateCommand(ShowCreatureCreationDialog, o => true);
            CreateCreatureDialogVM = new CreateCreatureDialogVM();
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
        //not showing
        public ICommand ShowCreatureCreationDialogCommand { get; }
    }
}