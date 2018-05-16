using System.Windows.Input;
using Core.Simulations;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class EnvironmentCollectionVM : ListVM<EnvironmentVM>
    {
        public ICommand AddNewEnvironmentCommand { get; }
        public ICommand CloneCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public EnvironmentCollectionVM()
        {
            AddNewEnvironmentCommand = new DelegateCommand(o => AddNewEnvironment(), o => true);
            CloneCommand = new DelegateCommand(o => CloneEnvironment((EnvironmentVM)o), o => true);
            EditCommand = new DelegateCommand(o => EditEnvironment((EnvironmentVM)o), o => true);
            RemoveCommand = new DelegateCommand(o => RemoveEnvironment((EnvironmentVM)o), o => true);
        }

        private void RemoveEnvironment(EnvironmentVM environment)
        {
            Remove(environment);
        }

        private void EditEnvironment(EnvironmentVM environment)
        {

        }

        private void CloneEnvironment(EnvironmentVM environment)
        {
            Add(environment.Clone());
        }

        private void AddNewEnvironment()
        {
            Add(new EnvironmentVM(new Environment()));
        }
    }
}