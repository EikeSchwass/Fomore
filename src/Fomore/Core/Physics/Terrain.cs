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
            var vertices = new List<Vector2>();
            foreach (var vertex in SimulationSettings.Environment.TerrainGenerators.GenerateAggregated())
            {
                vertices.Add(new Vector2(vertex.X / 12, vertex.Y));
                if (vertices.Count > 1000)
                    break;
            }

            double maxY = SimulationSettings.CreatureMovementPatterns.Max(c => c.Creature.CreatureStructure.Joints.Max(j => j.Position.Y));
            double maxX = SimulationSettings.CreatureMovementPatterns.Max(c => c.Creature.CreatureStructure.Joints.Max(j => j.Position.X));
            double minY = SimulationSettings.CreatureMovementPatterns.Min(c => c.Creature.CreatureStructure.Joints.Min(j => j.Position.Y));
            double minX = SimulationSettings.CreatureMovementPatterns.Min(c => c.Creature.CreatureStructure.Joints.Min(j => j.Position.X));

            double startMin = vertices.Where(k => k.X >= minX - (maxX - minX) && k.X <= maxX + (maxX - minX)).Min(k => k.Y);
            for (int i = 0; i < vertices.Count; i++)
            {
                var v = vertices[i];
                vertices[i] = new Vector2(v.X - vertices.Min(k => k.X) + minX - (maxX - minX) * 2, v.Y - startMin + (maxY - minY) + 1);
            }

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