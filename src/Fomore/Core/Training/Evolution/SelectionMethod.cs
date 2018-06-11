using System.Collections.Generic;
using System.Linq;

namespace Core.Training.Evolution
{
    public static class SelectionMethods
    {
        /// <summary>
        ///     Weighted Randomness. Individuals have a chance of being selected proportional to their fitness.
        /// </summary>
        public static IEnumerable<Individual<T>> Roulette<T>(this ICollection<Individual<T>> population)
        {
            var totalFitness = population.Sum(i => i.Fitness ?? 0);
            while (true)
            {
                double weightedIndex = AdvancedRandom.Random.NextDouble() * totalFitness;
                double currentIndex = 0;
                foreach (var individual in population)
                {
                    if (!(currentIndex + weightedIndex <= individual.Fitness)) continue;
                    yield return individual;
                    break;

                }
            }
        }

        /// <summary>
        ///     Pure random selection. Propably only useful in some corner cases.
        /// </summary>
        public static IEnumerable<Individual<T>> Random<T>(this ICollection<Individual<T>> population)
        {
            while (true)
            {
                int index = AdvancedRandom.Random.Next(population.Count);
                var result = population.ElementAt(index);
                yield return result;
            }
        }

        /// <summary>
        ///     Returns population sorted by their fitness (descending).
        /// </summary>
        public static IEnumerable<Individual<T>> Best<T>(this IEnumerable<Individual<T>> population)
        {
            return population.OrderByDescending(i => i.Fitness);
        }

        /// <summary>
        ///     The individuals are selected pairwise with pure randomness and then their fitness values are compared. The better
        ///     one get's to breed.
        /// </summary>
        public static IEnumerable<Individual<T>> Tournament<T>(this ICollection<Individual<T>> population)
        {
            while (true)
            {
                var first = population.Random().First();
                var second = population.Random().First();
                while (first == second)
                    second = population.Random().First();
                yield return first.Fitness > second.Fitness ? first : second;
            }
        }
    }
}