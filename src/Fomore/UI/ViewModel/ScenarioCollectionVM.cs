using System.Windows.Input;
using Core.Simulations;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class ScenarioCollectionVM : ListVM<ScenarioVM>
    {
        public ICommand AddNewScenarioCommand { get; }
        public ICommand CloneCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        public ScenarioCollectionVM()
        {
            AddNewScenarioCommand = new DelegateCommand(o => AddNewScenario(), o => true);
            CloneCommand = new DelegateCommand(o => CloneScenario((ScenarioVM)o), o => true);
            EditCommand = new DelegateCommand(o => EditScenario((ScenarioVM)o), o => true);
            RemoveCommand = new DelegateCommand(o => RemoveScenario((ScenarioVM)o), o => true);
        }

        private void RemoveScenario(ScenarioVM scenario)
        {
            Remove(scenario);
        }

        private void EditScenario(ScenarioVM scenario)
        {

        }

        private void CloneScenario(ScenarioVM scenario)
        {
            Add(scenario.Clone());
        }

        private void AddNewScenario()
        {
            Add(new ScenarioVM(new Scenario()));
        }
    }
}