using System;
using System.Linq;
using Core.Physics;

namespace Core.Training
{
    public class IsolatedMovementPatternEvaluation : MarshalByRefObject
    {
        public float Evaluate(MovementPattern movementPattern, Creature creature, Environment environment, float evaluationDuration)
        {
            var simulation = new Simulation(new SimulationSettings(new[] {new CreatureMovementPattern(creature, movementPattern)},
                                                                   environment));
            simulation.RunFor(evaluationDuration);

            float average = simulation.SimulationEntities.Single().Bodies.Average(b => b.Position.X);
            return average;
        }
    }
}
