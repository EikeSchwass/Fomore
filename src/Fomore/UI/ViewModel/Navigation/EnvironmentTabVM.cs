using Core;
using Fomore.UI.ViewModel.Application;
using Fomore.UI.ViewModel.Commands;
using Fomore.UI.ViewModel.Data;

namespace Fomore.UI.ViewModel.Navigation
{
    public class EnvironmentTabVM : TabPageVM
    {
        /// <inheritdoc />
        public override string Header => "Environment";

        public TabNavigationVM TabNavigationVM { get; }
        public EntityStorageVM EntitiesStorage { get; }

        // ------------------------------------------------------------
        // Properties and private members
        // ------------------------------------------------------------

        private EnvironmentVM selectedEnvironment;

        public EnvironmentVM SelectedEnvironment
        {
            get => selectedEnvironment;
            set
            {
                if (Equals(value, selectedEnvironment)) return;
                selectedEnvironment = value;
                OnPropertyChanged();
            }
        }

        // ------------------------------------------------------------
        // Commands and Actions
        // ------------------------------------------------------------

        public DelegateCommand NewEnvironment { get; }
        public DelegateCommand CloneCommand { get; }
    
        private void CloneAction(object obj)
        {
            if (obj is EnvironmentVM environment)
            {
                var clone = environment.Clone();
                clone.Name = "Clone of " + clone.Name;
                EntitiesStorage.AddEnvironmentCommand.Execute(clone);
                SelectedEnvironment = clone;
            }
        }

        private void NewEnvironmentAction(object obj)
        {
            var environment = new Environment()
            {
                Name = "New Environment",
                Description = "No description available...",
                Gravity = 0.0,
                Friction = 0.0
            };
            var environmentVM = new EnvironmentVM(environment);
            EntitiesStorage.AddEnvironmentCommand.Execute(environmentVM);
            SelectedEnvironment = environmentVM;
        }

        // ------------------------------------------------------------
        // Entry point & other methods
        // ------------------------------------------------------------

        public EnvironmentTabVM(TabNavigationVM tabNavigationVM, EntityStorageVM entitiesStorage)
        {
            TabNavigationVM = tabNavigationVM;
            EntitiesStorage = entitiesStorage;

            NewEnvironment = new DelegateCommand(NewEnvironmentAction, o => true);
            CloneCommand = new DelegateCommand(CloneAction, o => true);
        }
    }
}