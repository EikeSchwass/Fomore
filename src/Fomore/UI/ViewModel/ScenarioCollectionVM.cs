using System.Windows.Input;
using Core.Simulations;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class ScenarioCollectionVM : ListVM<ScenarioVM>
    {
        public ICommand AddNewScenarioCommand { get; }

        public ScenarioCollectionVM()
        {
            AddNewScenarioCommand = new DelegateCommand(o => AddNewScenario(), o => true);
            Add(new ScenarioVM(new Scenario()));
            Add(new ScenarioVM(new Scenario()));
            Add(new ScenarioVM(new Scenario()));
        }

        /// <inheritdoc />
        protected override void OnItemAdded(ScenarioVM scenario)
        {
            base.OnItemAdded(scenario);
            scenario.CloneRequested += CloneScenario;
            scenario.EditViewRequested += EditScenario;
            scenario.RemoveRequested += RemoveScenario;
        }

        /// <inheritdoc />
        protected override void OnItemRemoved(ScenarioVM scenario)
        {
            base.OnItemRemoved(scenario);
            scenario.CloneRequested -= CloneScenario;
            scenario.EditViewRequested -= EditScenario;
            scenario.RemoveRequested -= RemoveScenario;
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