using System.Windows;
using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreateEnvironmentDialogVM : ViewModelBase
    {
        private string environmentDescription;
        private string environmentName;
        private Visibility visibility = Visibility.Hidden;
        public EntityStorageVM EntityStorageVM { get; }

        public string EnvironmentName
        {
            get => environmentName;
            set
            {
                if (value == environmentName) return;
                environmentName = value;
                OnPropertyChanged();
            }
        }

        public string EnvironmentDescription
        {
            get => environmentDescription;
            set
            {
                if (value == environmentDescription) return;
                environmentDescription = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                if (value == visibility) return;
                visibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand CreateEnvironmentCommand { get; }
        public ICommand CancelEnvironmentCreationCommand { get; }

        public CreateEnvironmentDialogVM(EntityStorageVM entityStorageVM)
        {
            EntityStorageVM = entityStorageVM;
            CreateEnvironmentCommand = new DelegateCommand(CreateEnvironment, CanCreateEnvironment);
            CancelEnvironmentCreationCommand = new StubCommand();
        }

        private bool CanCreateEnvironment(object arg) => true;

        private void CreateEnvironment(object obj)
        {
            Visibility = Visibility.Hidden;
            var environment = new EnvironmentVM(new Environment {Name = EnvironmentName, Description = EnvironmentDescription});
            EntityStorageVM.AddEnvironmentCommand.Execute(environment);
        }
    }
}