using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Core;
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

        private CreateEnvironDialogVM CreateEnvironDialogVM;

        public EnvironmentTabVM(EntityStorageVM entitiesStorage)
        {
            EntitiesStorage = entitiesStorage;
            ShowEnvironCreationDialogCommand = new DelegateCommand(ShowEnvironCreationDialog, o => true);
            HideEnvironCreationDialogCommand = new DelegateCommand(HideEnvironCreationDialog, o => true);
            CreateEnvironDialogVM = new CreateEnvironDialogVM();
        }

        private void ShowEnvironCreationDialog(object obj)
        {
            CreateEnvironDialogVM.Visibility = Visibility.Visible;
        }

        private void HideEnvironCreationDialog(object obj)
        {
            CreateEnvironDialogVM.Visibility = Visibility.Hidden;
        }

        public CreateEnvironDialogVM CreateCreatureDialogVM
        {
            get => CreateEnvironDialogVM;
            set
            {
                if (Equals(value, CreateEnvironDialogVM)) return;
                CreateEnvironDialogVM = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowEnvironCreationDialogCommand { get; }

        public ICommand HideEnvironCreationDialogCommand { get; }
    }
}