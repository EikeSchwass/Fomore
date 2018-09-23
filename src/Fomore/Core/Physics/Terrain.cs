using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Core.Physics
{
    public class Terrain
    {
        public World World { get; }
        public SimulationSettings SimulationSettings { get; }

        public IReadOnlyCollection<Vector2> Vertices { get; private set; }

        public Terrain(World world, SimulationSettings simulationSettings)
        {
            World = world;
            SimulationSettings = simulationSettings;
            CreateTerrain();
        }

        private void CreateTerrain()
        {
            var vertices = SimulationSettings.Environment.TerrainGenerators.GenerateAggregated().Take(2000).ToList();
            double vMin = vertices.Min(j => j.X);
            double maxY = SimulationSettings.CreatureMovementPatterns.Max(c => c.Creature.CreatureStructure.Joints.Max(j => j.Position.Y));
            vertices = vertices.Select(v => new Vector2((v.X - vMin) / 4, v.Y + maxY + 0.2)).ToList();

            Vertices = vertices.AsReadOnly();

            for (int i = 0; i < vertices.Count - 1; i++)
            {
                var edge = BodyFactory.CreateEdge(World, vertices[i].ToXna(), vertices[i + 1].ToXna(), this);
                edge.FixtureList.Single().Friction = 1f;
                edge.IsStatic = true;
                edge.BodyType = BodyType.Static;
            }
        }
    }
}