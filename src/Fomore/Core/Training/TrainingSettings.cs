namespace Core.Training
{
    public class TrainingSettings
    {
        public int PopulationSize { get; }
        public float IterationDuration { get; }
        public float MutationChance { get; }
        public float MutationIntensity { get; }
        public Creature Creature { get; }
        public MovementPattern MovementPattern { get; }
        public int NumberOfIterations { get; }
        public Environment Environment { get; }

        public TrainingSettings(Creature creature, MovementPattern movementPattern, Environment environment, int numberOfIterations, int populationSize = 32, float iterationDuration = 15, float mutationChance = 0.004f, float mutationIntensity = 0.0005f)
        {
            PopulationSize = populationSize;
            IterationDuration = iterationDuration;
            MutationChance = mutationChance;
            MutationIntensity = mutationIntensity;
            Creature = creature.Clone();
            MovementPattern = movementPattern;
            NumberOfIterations = numberOfIterations;
            Environment = environment.Clone();
        }
    }
}