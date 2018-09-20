// Eike Stein: Fomore/Core/TraningSession.cs (2018/09/20)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Physics;
using Core.Training.Evolution;

namespace Core.Training
{
    public class TraningSession
    {
        public delegate bool IterationCompletedCallback(TraningSession sender, IReadOnlyCollection<Individual<MovementPattern>> population);

        public TrainingSettings TrainingSettings { get; }

        private List<Individual<MovementPattern>> CurrentPopulation { get; } = new List<Individual<MovementPattern>>();

        public TraningSession(TrainingSettings trainingSettings)
        {
            TrainingSettings = trainingSettings;
            Initialize();
        }

        private void Initialize()
        {
            var population = GeneratePopulation();
            CurrentPopulation.Clear();
            CurrentPopulation.AddRange(population);
        }

        public async Task RunTrainingSessionAsync() => await RunTrainingSessionAsync(null);

        public async Task RunTrainingSessionAsync(IterationCompletedCallback iterationCompletedCallback)
        {
            await Task.Run(() => { RunTrainingSession(iterationCompletedCallback); });
        }

        public void RunTrainingSession(IterationCompletedCallback iterationCompletedCallback)
        {
            for (int i = 0; i < TrainingSettings.NumberOfIterations; i++)
            {
                RunTrainingIteration();
                if (iterationCompletedCallback?.Invoke(this, CurrentPopulation.AsReadOnly()) == true)
                    break;
            }
        }

        private void RunTrainingIteration()
        {
            if (CurrentPopulation.Any(individual => individual.Fitness == null))
                EvaluatePopulation(CurrentPopulation);

            var newPopulation = new List<Individual<MovementPattern>>();
            var currentPopulation = CurrentPopulation.OrderByDescending(i => i.Fitness).ToList();

            int takeBestCount = 1;
            int takeBestAndMutateCount = 1 + TrainingSettings.PopulationSize * 80 / 100;

            newPopulation.AddRange(currentPopulation.Take(takeBestAndMutateCount).Select(i => new Individual<MovementPattern>(new MovementPattern(i.Phenotype, i.Phenotype.NeuralNetwork), i.Fitness)));

            while (newPopulation.Count < TrainingSettings.PopulationSize - takeBestCount) newPopulation.Add(SelectIndividual(currentPopulation));

            newPopulation = MutatePopulation(newPopulation);
            newPopulation.AddRange(currentPopulation.Take(takeBestCount));
            EvaluatePopulation(newPopulation);
            CurrentPopulation.Clear();
            CurrentPopulation.AddRange(newPopulation);
        }

        private List<Individual<MovementPattern>> MutatePopulation(List<Individual<MovementPattern>> population)
        {
            var newPopulation = new List<Individual<MovementPattern>>();
            foreach (var individual in population)
            {
                var mutatedNetwork = individual.Phenotype.NeuralNetwork.MutateNetworkWeights(TrainingSettings.MutationChance,
                                                                                             TrainingSettings.MutationIntensity);
                var newMovementPattern = new MovementPattern(individual.Phenotype, mutatedNetwork);
                var newIndividual = new Individual<MovementPattern>(newMovementPattern, null);
                newPopulation.Add(newIndividual);
            }

            return newPopulation;
        }

        /// <summary>
        ///     Returns an individual for the next generation.
        /// </summary>
        /// <param name="population">Sorted list by fitness</param>
        /// <returns>Returns an individual for the next generation.</returns>
        private Individual<MovementPattern> SelectIndividual(List<Individual<MovementPattern>> population)
        {
            double min = population.Min(i => i.Fitness ?? 0);
            double sum = population.Sum(p => p.Fitness - min + 0.01 ?? 0);
            double weightedIndex = AdvancedRandom.Random.NextDouble() * sum;
            double currentIndex = 0;
            foreach (var individual in population)
            {
                if (currentIndex + (individual.Fitness - min + 0.01) >= weightedIndex)
                    return individual;
                currentIndex += (individual.Fitness - min + 0.01) ?? 0;
            }

            throw new InvalidOperationException();
        }

        private async Task EvaluatePopulationAsync(ICollection<Individual<MovementPattern>> population)
        {
            await Task.Run(() => { EvaluatePopulation(population); });
        }

        private void EvaluatePopulation(ICollection<Individual<MovementPattern>> population)
        {
            var creatureMovementPatterns =
                population.Select(individual => new CreatureMovementPattern(TrainingSettings.Creature, individual.Phenotype)).ToList();

            Parallel.ForEach(creatureMovementPatterns,
                             cmp =>
                             {
                                 try
                                 {
                                     float rating;
                                     using (var isolated = new Isolated<IsolatedMovementPatternEvaluation>())
                                     {
                                         rating = isolated.Value.Evaluate(cmp.MovementPattern,
                                                                          cmp.Creature,
                                                                          TrainingSettings.Environment,
                                                                          TrainingSettings.IterationDuration);
                                     }


                                     lock (population)
                                     {
                                         var individual =
                                             population.Single(i => ReferenceEquals(i.Phenotype,
                                                                                    cmp.MovementPattern));
                                         individual.Fitness = rating;
                                     }
                                 }
                                 catch (Exception e)
                                 {
                                     Debug.WriteLine(e);
                                 }
                             });
        }

        private IEnumerable<Individual<MovementPattern>> GeneratePopulation()
        {
            yield return new Individual<MovementPattern>(TrainingSettings.MovementPattern.Clone(), null);
            for (int i = 1; i < TrainingSettings.PopulationSize; i++)
            {
                var neuralNetwork = TrainingSettings.MovementPattern.NeuralNetwork;
                neuralNetwork = neuralNetwork.MutateNetworkWeights(TrainingSettings.MutationChance, TrainingSettings.MutationIntensity);
                var individual =
                    new Individual<MovementPattern>(new MovementPattern(TrainingSettings.MovementPattern, neuralNetwork), null);
                yield return individual;
            }
        }
    }
}