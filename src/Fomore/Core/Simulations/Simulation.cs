using System;
using System.Collections.Generic;
using System.Linq;
using Core.Creatures;

namespace Core.Simulations
{
    public class Simulation
    {
        public Scenario Scenarios { get; }
        public Terrain Terrain => Scenarios.Environment.Terrain;
        public IReadOnlyCollection<CreatureBehavior> CreatureBehaviors { get; }

        public Simulation(Scenario scenario, params CreatureBehavior[] creatureBehaviors)
        {
            Scenarios = scenario;
            CreatureBehaviors = creatureBehaviors.ToList().AsReadOnly();
        }

        public SimulationResult Evaluate() => throw new NotImplementedException();
    }
}