using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core;
using Fomore.UI.ViewModel.Commands;
using Environment = Core.Environment;

namespace Fomore.UI.ViewModel.Navigation
{
    public class CreateEnvironDialogVM : ViewModelBase
    {
        private Visibility visibility = Visibility.Hidden;
        private string EnvironmentDescription;
        private string EnvironmentName;
        public ICollection<Environment> Environs;

        public ICollection<Environment> GetEnvironments { get { return Environs; } }

        public string EnvironName
        {
            get => EnvironmentName;
            set
            {
                if (value == EnvironmentName) return;
                EnvironmentName = value;
                OnPropertyChanged();
            }
        }

        public string EnvironDescription
        {
            get => EnvironmentDescription;
            set
            {
                if (value == EnvironmentDescription) return;
                EnvironmentDescription = value;
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

        public ICommand CreateEnvironCommand { get; }
        public ICommand CancelEnvironCreationCommand { get; }

        public CreateEnvironDialogVM()
        {
            CreateEnvironCommand = new DelegateCommand(CreateEnviron, CanCreateEnviron);
            CancelEnvironCreationCommand = new StubCommand();
            Environs = new List<Environment>();
        }

        private bool CanCreateEnviron(object arg)
        {
            return true;
        }

        private void CreateEnviron(object obj)
        {
            Visibility = Visibility.Hidden;
            Environment Environs = new Environment();
            Environs.EnvironmentName = this.EnvironmentName;
            Environs.EnvironmentDescription = this.EnvironmentDescription;

            Environs.Add(Environs);
        }

    }
}
