namespace Core.Training
{
    public class TrainingSettings
    {
        public int PopulationSize { get; }
        public float IterationDuration { get; }
        public float MutationChance { get; }
        public double MutationIntensity { get; }
        public Creature Creature { get; }
        public MovementPattern MovementPattern { get; }
        public int NumberOfIterations { get; }
        public bool UseRandomness { get; }
        public Environment Environment { get; }

        public TrainingSettings(Creature creature,
                                MovementPattern movementPattern,
                                Environment environment,
                                int numberOfIterations,
                                bool useRandomness = false,
                                int populationSize = 16,
                                float iterationDuration = 15,
                                float mutationChance = 0.004f,
                                double mutationIntensity = 0.5f)
        {
            PopulationSize = populationSize;
            IterationDuration = iterationDuration;
            MutationChance = mutationChance;
            MutationIntensity = mutationIntensity;
            Creature = creature.Clone();
            MovementPattern = movementPattern;
            NumberOfIterations = numberOfIterations;
            UseRandomness = useRandomness;
            Environment = environment.Clone();
        }
    }
}