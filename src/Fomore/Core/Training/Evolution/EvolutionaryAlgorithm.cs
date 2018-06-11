// Eike Stein: Fomore/Core/EvolutionaryAlgorithm.cs (2018/06/09)

using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Training.Evolution
{
    /// <summary>
    ///     This class implements the evoltionary algorithm <see href="https://en.wikipedia.org/wiki/Evolutionary_algorithm" />
    /// </summary>
    /// <typeparam name="T">The type of the individuals in the population.</typeparam>
    public class EvolutionaryAlgorithm<T>
    {
        /// <summary>
        ///     This delegate is used to clone individuals.
        /// </summary>
        /// <param name="individual">The individual to clone.</param>
        /// <returns>An identical copy of the original individual.</returns>
        public delegate T CloneIndividualCallback(T individual);

        /// <summary>
        ///     This delegate is used to evaluate the fitness of the individuals in the population.
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        public delegate double EvaluateIndividualCallback(T individual);

        /// <summary>
        ///     This delegate is used to describe methods that generate individuals.
        /// </summary>
        /// <param name="index">
        ///     The index of the individual that should be generated. Starts at 0 and ends at the
        ///     <paramref name="numberOfIndividuals" /> -1.
        /// </param>
        /// <param name="numberOfIndividuals">
        ///     The total number of individuals to create.
        /// </param>
        /// <returns>Returns a new individual.</returns>
        public delegate T GenerateIndividualCallback(int index, int numberOfIndividuals);

        /// <summary>
        ///     This delegate describes methods that implement crossover functionality / breed new offspring.
        /// </summary>
        /// <param name="parentGroup">The group of parents the new offspring should be generated from.</param>
        /// <param name="remainingSlotsInNextGeneration">
        ///     The number of remaining slots that need to be filled for the next
        ///     generation.
        /// </param>
        /// <returns>
        ///     At least one new offspring, may return more if applicable. If more offspring is returned than
        ///     <paramref name="remainingSlotsInNextGeneration" /> only as many will considered as are missing.
        /// </returns>
        public delegate IEnumerable<T> IndividualCrossoverCallback(IReadOnlyCollection<Individual<T>> parentGroup,
                                                                   int remainingSlotsInNextGeneration);

        /// <summary>
        ///     The delegates describes methods that merge the populations of two generations together. This is delegate is used in
        ///     the last step of the evolutionary algorithm to decide which individuals should be in the next generation and if
        ///     individuals should be placed in the next generation unchanged.
        /// </summary>
        /// <param name="newGeneration">The individuals that were bred from the previous generation.</param>
        /// <param name="previousGeneration">The individuals of the previous generation.</param>
        /// <returns>The individuals for the next generation. Must be as many individuals as in the previous generation.</returns>
        public delegate IEnumerable<Individual<T>> MergePopulationCallback(IReadOnlyCollection<Individual<T>> newGeneration,
                                                                           IReadOnlyCollection<Individual<T>>
                                                                               previousGeneration);

        /// <summary>
        ///     This delegate describes methods that implement mutation functionality.
        /// </summary>
        /// <param name="population">The current population.</param>
        /// <returns>
        ///     A collection of mutated individuals. Can be identical to <paramref name="population" /> if nothing should be
        ///     mutated.
        /// </returns>
        public delegate ICollection<T> MutateIndividualsCallback(IReadOnlyCollection<T> population);

        /// <summary>
        ///     This delegates describes methods that are used to select the parents for the next generation.
        ///     <see cref="SelectionMethods" /> may be of help as it implements a number of common selection algorithms.
        /// </summary>
        /// <param name="currentPopulation">The current population</param>
        /// <returns>
        ///     A list of collections of parents. Each collection should contain at least two parents that will breed a child
        ///     for the next generation. Therefor the returned <see cref="IEnumerable{T}" /> should have as many elements as the
        ///     population size. Having individuals in multiple parent groups is obviously allowed. If the number of returned
        ///     parent groups is less than the size of the population, parent groups may breed multiple times.
        /// </returns>
        public delegate IEnumerable<ICollection<Individual<T>>> SelectIndividualsCallback(
            IReadOnlyCollection<Individual<T>> currentPopulation);

        /// <inheritdoc cref="EvaluateIndividualCallback" />
        private EvaluateIndividualCallback EvaluateIndividual { get; }

        /// <inheritdoc cref="MutateIndividualsCallback" />
        private MutateIndividualsCallback MutateIndividuals { get; }

        /// <inheritdoc cref="CloneIndividualCallback" />
        private CloneIndividualCallback CloneIndividual { get; }

        /// <inheritdoc cref="SelectIndividualsCallback" />
        private SelectIndividualsCallback SelectIndividuals { get; }

        /// <inheritdoc cref="GenerateIndividualCallback" />
        private GenerateIndividualCallback GenerateIndividual { get; }

        /// <inheritdoc cref="IndividualCrossoverCallback" />
        private IndividualCrossoverCallback IndividualCrossover { get; }

        /// <inheritdoc cref="MergePopulationCallback" />
        private MergePopulationCallback MergePopulation { get; }

        private bool IsInitialized => Population.Any();

        private IReadOnlyCollection<Individual<T>> Population { get; set; }

        /// <summary>
        ///     Creates a new instance of the <see cref="EvolutionaryAlgorithm{T}" /> class.
        /// </summary>
        /// <param name="evaluateIndividual">
        ///     <inheritdoc cref="EvaluateIndividualCallback" />
        /// </param>
        /// <param name="cloneIndividual">
        ///     <inheritdoc cref="EvaluateIndividualCallback" />
        /// </param>
        /// <param name="selectIndividuals">
        ///     <inheritdoc cref="EvaluateIndividualCallback" />
        /// </param>
        /// <param name="generateIndividual">
        ///     <inheritdoc cref="EvaluateIndividualCallback" />
        /// </param>
        /// <param name="individualCrossover">
        ///     <inheritdoc cref="EvaluateIndividualCallback" />
        /// </param>
        /// <param name="mutateIndividuals">
        ///     <inheritdoc cref="MutateIndividuals" />
        /// </param>
        /// <param name="mergePopulation">
        ///     <inheritdoc cref="MergePopulation" />
        /// </param>
        public EvolutionaryAlgorithm(EvaluateIndividualCallback evaluateIndividual,
                                     CloneIndividualCallback cloneIndividual,
                                     SelectIndividualsCallback selectIndividuals,
                                     GenerateIndividualCallback generateIndividual,
                                     IndividualCrossoverCallback individualCrossover,
                                     MutateIndividualsCallback mutateIndividuals,
                                     MergePopulationCallback mergePopulation)
        {
            EvaluateIndividual = evaluateIndividual;
            CloneIndividual = cloneIndividual;
            SelectIndividuals = selectIndividuals;
            GenerateIndividual = generateIndividual;
            IndividualCrossover = individualCrossover;
            MutateIndividuals = mutateIndividuals;
            MergePopulation = mergePopulation;
        }

        /// <summary>
        ///     Initializes the population. Must be called before the exectution of the actual algorithm can begin.
        /// </summary>
        /// <param name="numberOfIndividuals">The number of individuals in the population.</param>
        public void InitializePopulation(int numberOfIndividuals)
        {
            var population = new List<Individual<T>>();
            for (int i = 0; i < numberOfIndividuals; i++)
            {
                var individual = GenerateIndividual(i, numberOfIndividuals);
                population.Add(new Individual<T>(individual, null));
            }

            Population = population;
            EvaluatePopulation();
        }

        /// <summary>
        ///     This method executes the next generation (selection, crossover, mutation,evaluating).
        ///     <see cref="InitializePopulation" /> must have been called before.
        /// </summary>
        public IReadOnlyCollection<Individual<T>> NextGeneration()
        {
            if (!IsInitialized)
                throw new InvalidOperationException($"{nameof(InitializePopulation)} must be called first.");

            var newPopulation = new List<Individual<T>>();

            var newIndividuals = Crossover();
            newIndividuals = MutateIndividuals(newIndividuals.ToList().AsReadOnly());

            foreach (var newIndividual in newIndividuals)
            {
                double fitness = EvaluateIndividual(newIndividual);
                var individual = new Individual<T>(newIndividual, fitness);
                newPopulation.Add(individual);
            }

            var mergePopulation = MergePopulation(newPopulation.AsReadOnly(), Population);
            Population = mergePopulation.ToList();
            return Population;
        }

        private ICollection<T> Crossover()
        {
            var newIndividuals = new List<T>();
            var parentCollections = RepeatParentGroup(SelectIndividuals(Population));
            foreach (var parentCollection in parentCollections)
            {
                var offSpring = IndividualCrossover(parentCollection.ToList().AsReadOnly(),
                                                    Population.Count - newIndividuals.Count);
                foreach (var child in offSpring)
                {
                    if (newIndividuals.Count < Population.Count)
                        newIndividuals.Add(child);
                    else
                        break;
                }

                if (newIndividuals.Count == Population.Count)
                    break;
                if (newIndividuals.Count > Population.Count)
                    throw new InvalidOperationException("New population size must not be greater than the previous size.");
            }

            return newIndividuals;
        }

        /// <summary>
        /// This method returns a parent collection over and over again, so consuming code doesnt run out of parent groups.
        /// </summary>
        /// <param name="parentGroups">The parent groups this method should repeat.</param>
        /// <returns>An infinite iterator.</returns>
        private IEnumerable<ICollection<Individual<T>>> RepeatParentGroup(
            IEnumerable<ICollection<Individual<T>>> parentGroups)
        {
            var parentGroupsCached = new List<ICollection<Individual<T>>>();
            foreach (var parentGroup in parentGroups)
            {
                parentGroupsCached.Add(parentGroup);
                yield return parentGroup;
            }

            while (true)
            {
                foreach (var parentGroup in parentGroupsCached)
                    yield return parentGroup;
            }
        }

        /// <summary>
        ///     Gets called from the <see cref="NextGeneration" /> method to evaluate each individual of the population.
        /// </summary>
        private void EvaluatePopulation()
        {
            var newPopulation = new List<Individual<T>>();
            foreach (var individual in Population)
            {
                var phenotype = individual.Phenotype;
                double fitness = EvaluateIndividual(phenotype);
                var cloned = CloneIndividual(phenotype);
                newPopulation.Add(new Individual<T>(cloned, fitness));
            }

            Population = newPopulation;
        }
    }
}