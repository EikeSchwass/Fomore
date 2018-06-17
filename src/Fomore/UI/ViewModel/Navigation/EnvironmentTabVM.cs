using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;


namespace Fomore.UI.ViewModel.Navigation
{
    public class EnvironmentTabVM : TabPageVM
    {
        public EntityStorageVM EntitiesStorage { get; }

        /// <inheritdoc />
        public override string Header => "Environment";

        public string EnterName => "Enter Name*:";
        public string Description => "Description";

        public string Terrain => "Terrain:";
        public string GroundType => "Ground Type:";
        public string Gravity => "Gravity:";
        public string Friction => "Friction:";

        public string CreateButton => "Create";
        public string CancelButton => "Cancel";

        public List<string> EnvironmentList;

        public string EnvironmentName { get; set; }
        public string EnterDescription { get; set; }

        public CreateEnvironmentDialogVM CreateEnvironmentDialogVM { get; }

        public EnvironmentTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
            ShowEnvironCreationDialogCommand = new DelegateCommand(ShowEnvironCreationDialog, o => true);
            HideEnvironCreationDialogCommand = new DelegateCommand(HideEnvironCreationDialog, o => true);
            CreateEnvironmentDialogVM = new CreateEnvironmentDialogVM(EntitiesStorage);
        }

        private void ShowEnvironCreationDialog(object obj)
        {
            CreateEnvironmentDialogVM.Visibility = Visibility.Visible;
        }

        private void HideEnvironCreationDialog(object obj)
        {
            CreateEnvironmentDialogVM.Visibility = Visibility.Hidden;
        }

        public ICommand ShowEnvironCreationDialogCommand { get; }

        public ICommand HideEnvironCreationDialogCommand { get; }
    }
}