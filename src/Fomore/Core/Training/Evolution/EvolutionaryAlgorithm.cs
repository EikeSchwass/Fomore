// Eike Stein: Fomore/Core/EvolutionaryAlgorithm.cs (2018/06/09)

using System.Collections.Generic;

namespace Core.Training.Evolution
{
    /// <summary>
    ///     This class implements the evoltionary algorithm <see href="https://en.wikipedia.org/wiki/Evolutionary_algorithm" />
    /// </summary>
    /// <typeparam name="T">The type of the individuals in the population.</typeparam>
    public class EvolutionaryAlgorithm<T>
    {
        public EvaluateIndividual EvaluateIndividualDelegate { get; }
        public CloneIndividual CloneIndividualDelegate { get; }

        /// <summary>
        ///     This delegate is used to describe methods that generate individuals.
        /// </summary>
        /// <param name="index">
        ///     The index of the individual that should be generated. Starts at 0 and ends at the 
        ///     <paramref name="numberOfIndividuals"/> -1.
        /// </param>
        /// <param name="numberOfIndividuals">
        ///     The total number of individuals to create.
        /// </param>
        /// <param name="randomNumberGenerator">A random number generator that can aid the process of creating individuals.</param>
        /// <returns>Returns a new individual.</returns>
        public delegate T IndividualGeneration(int index, int numberOfIndividuals, AdvancedRandom randomNumberGenerator);

        /// <summary>
        /// This delegate is used to evaluate the fitness of the individuals in the population.
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public delegate double EvaluateIndividual(T individual);

        /// <summary>
        /// This delegate is used to clone individuals.
        /// </summary>
        /// <param name="individual">The individual to clone.</param>
        /// <returns>An identical copy of the original individual.</returns>
        public delegate T CloneIndividual(T individual);

        private List<Individual> Population { get; set; }

        /// <summary>
        ///     Creates a new instance of the <see cref="EvolutionaryAlgorithm{T}" /> class.
        /// </summary>
        public EvolutionaryAlgorithm(EvaluateIndividual evaluateIndividualDelegate, CloneIndividual cloneIndividualDelegate)
        {
            EvaluateIndividualDelegate = evaluateIndividualDelegate;
            CloneIndividualDelegate = cloneIndividualDelegate;
        }

        /// <summary>
        ///     Initializes the population. Must be called before the exectution of the actual algorithm can begin.
        /// </summary>
        /// <param name="individualGenerationDelegate">Gets called based on how many <see cref="numberOfIndividuals"/> should be present in the population.</param>
        /// <param name="numberOfIndividuals">The number of individuals in the population.</param>
        public void InitializePopulation(IndividualGeneration individualGenerationDelegate, int numberOfIndividuals)
        {
            var advancedRandom = new AdvancedRandom();
            var population = new List<Individual>();
            for (int i = 0; i < numberOfIndividuals; i++)
            {
                var individual = individualGenerationDelegate(i,numberOfIndividuals, advancedRandom);
                population.Add(new Individual(individual, null));
            }

            Population = population;
            EvaluatePopulation();
        }

        /// <summary>
        /// This method executes the next generation (selection, crossover, mutation,evaluating). <see cref="InitializePopulation"/> must have been called before.
        /// </summary>
        /// <param name="generationCompositionProportions">Describes how the next generations should be build.</param>
        public void NextGeneration(GenerationCompositionProportions generationCompositionProportions)
        {

        }

        /// <summary>
        /// Gets called from the <see cref="NextGeneration"/> method to evaluate each individual of the population.
        /// </summary>
        private void EvaluatePopulation()
        {
            foreach (var individual in Population)
            {
                var phenotype = individual.Phenotype;
                double fitness = EvaluateIndividualDelegate(phenotype);
            }
        }

        private class Individual
        {
            public Individual(T phenotype, double? fitness)
            {
                Fitness = fitness;
                Phenotype = phenotype;
            }

            public T Phenotype { get; }
            public double? Fitness { get; }
        }
    }

    public struct GenerationCompositionProportions
    {
        /// <summary>
        /// Number of best individuals that should remain unchanged for the next generation
        /// </summary>
        public int NumberOfUnchangedIndividuals { get; }
    }
}