using Core.Simulations;

namespace Fomore.UI.ViewModel
{
    public class ScenarioVM : ViewModelBase
    {
        private Scenario Scenario { get; }

        public CreatureVM Creature { get; set; }
        public EnvironmentVM Environment { get; set; }

        public ScenarioVM(Scenario scenario)
        {
            Scenario = scenario;
            Creature = new CreatureVM(Scenario.Creature);
            Environment = new EnvironmentVM(Scenario.Environment);
        }

        public ScenarioVM Clone() => new ScenarioVM(Scenario.Clone());
    }
}