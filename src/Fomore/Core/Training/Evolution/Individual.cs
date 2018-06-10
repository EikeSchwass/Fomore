// Eike Stein: Fomore/Core/EvolutionaryAlgorithm.cs (2018/06/09)


namespace Core.Training.Evolution
{
    public class Individual<T>
    {
        public T Phenotype { get; }
        public double? Fitness { get; }

        public Individual(T phenotype, double? fitness)
        {
            Fitness = fitness;
            Phenotype = phenotype;
        }

    }
}