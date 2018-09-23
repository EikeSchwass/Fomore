using System;
using System.Collections.Generic;
using System.Linq;
using Core.Physics;

namespace Core.Training
{
    public class IsolatedMovementPatternEvaluation : MarshalByRefObject
    {
        public float Evaluate(MovementPattern movementPattern, Creature creature, Environment environment, float evaluationDuration, int simulationCount = 3)
        {
            var results = new List<float>();
            for (int i = 0; i < simulationCount; i++)
            {
                var simulation = new Simulation(new SimulationSettings(new[] { new CreatureMovementPattern(creature.Clone(), movementPattern.Clone()) },
                                                                       environment.Clone(), simulationCount > 1));
                simulation.RunFor(evaluationDuration);

                float average = simulation.SimulationEntities.Single().Bodies.Average(b => b.Position.X);
                results.Add(average);
            }

            return results.Average();
        }
    }
}
