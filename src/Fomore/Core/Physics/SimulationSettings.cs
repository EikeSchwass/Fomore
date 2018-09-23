using System.Collections.Generic;
using System.Linq;

namespace Core.Physics
{
    public class SimulationSettings
    {
        public IReadOnlyCollection<CreatureMovementPattern> CreatureMovementPatterns { get; }
        public Environment Environment { get; }
        public bool UseRandomness { get; }
        public float TickStepSize { get; }

        public SimulationSettings(IEnumerable<CreatureMovementPattern> creatureMovementPatterns, Environment environment, bool useRandomness = false, float tickStepSize = 0.0416f)
        {
            CreatureMovementPatterns = creatureMovementPatterns.ToList().AsReadOnly();
            Environment = environment;
            UseRandomness = useRandomness;
            TickStepSize = tickStepSize;
        }
    }
}