using System;
using System.Windows.Input;
using Core.Simulations;
using Fomore.UI.ViewModel.Commands;

namespace Fomore.UI.ViewModel
{
    public class ScenarioVM : ViewModelBase
    {
        private Scenario Scenario { get; }

        public ICommand CloneScenarionCommand { get; }
        public ICommand EditScenarionCommand { get; }
        public ICommand RemoveScenarionCommand { get; }

        public CreatureVM Creature { get; set; }
        public EnvironmentVM Environment { get; set; }

        public ScenarioVM(Scenario scenario)
        {
            Scenario = scenario;
            Creature = new CreatureVM(Scenario.Creature);
            Environment = new EnvironmentVM(Scenario.Environment);
            CloneScenarionCommand = new DelegateCommand(o => OnCloneRequested(), o => true);
            EditScenarionCommand = new DelegateCommand(o => OnEditViewRequested(), o => true);
            RemoveScenarionCommand = new DelegateCommand(o => OnRemoveRequested(), o => true);
        }

        public event Action<ScenarioVM> CloneRequested;
        public event Action<ScenarioVM> EditViewRequested;
        public event Action<ScenarioVM> RemoveRequested;

        public void OnCloneRequested()
        {
            CloneRequested?.Invoke(new ScenarioVM(Scenario.Clone()));
        }

        public void OnEditViewRequested() => EditViewRequested?.Invoke(this);
        public void OnRemoveRequested() => RemoveRequested?.Invoke(this);

        public ScenarioVM Clone() => new ScenarioVM(Scenario.Clone());
    }
}