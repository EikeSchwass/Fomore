// Eike Stein: Fomore/Core/TerrainExtensions.cs (2018/09/10)

using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class TerrainExtensions
    {
        public static IEnumerable<Vector2> GenerateAggregated(this IEnumerable<TerrainGenerator.TerrainGenerator> terrainGenerators)
        {
            var generators = terrainGenerators.ToList();
            for (int i = 0; ; i++)
            {
                var currentResult = generators.Select(g => g.Generate().Skip(i).Take(1).Single()).ToList();
                double x = currentResult.Average(v => v.X);
                double y = currentResult.Sum(v => v.Y);
                yield return new Vector2(x, y);
            }
        }
    }
}