using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Creatures;

namespace Core.Simulations
{
    public class SimulationResult
    {
        public Scenario Scenario { get; }
        public IReadOnlyDictionary<CreatureBehavior, EvaluationResult> EvaluationResults { get; }

        public SimulationResult(Scenario scenario, IDictionary<CreatureBehavior, EvaluationResult> evaluationResults)
        {
            Scenario = scenario;
            EvaluationResults = new ReadOnlyDictionary<CreatureBehavior, EvaluationResult>(evaluationResults);
        }
    }
}