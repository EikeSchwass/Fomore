using System;
using Core.Creatures;

namespace Core.Simulations
{
    public class Training
    {
        public Scenario Scenario { get; }

        public Training(Scenario scenario)
        {
            Scenario = scenario;
        }

        public void Initialize()
        {
            throw new NotImplementedException("Create multiple behaviors for the creature of the scenario");
        }

        public void Initialize(CreatureBehavior creatureBehavior)
        {
            throw new NotImplementedException("Create multiple behaviors for the creature of the scenario");
        }

        public void TrainGeneration()
        {
            var simulation = new Simulation(Scenario, new CreatureBehavior[0]);
            simulation.Evaluate();
            throw new NotImplementedException();
        }
    }
}