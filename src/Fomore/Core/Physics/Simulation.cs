using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace Core.Physics
{
    public class Simulation
    {
        public SimulationSettings SimulationSettings { get; }
        private World World { get; }
        private float RemainingDeltaTime { get; set; }
        private float TotalTime { get; set; }

        public List<SimulationEntity> SimulationEntities { get; } = new List<SimulationEntity>();

        public Terrain Terrain { get; private set; }

        public Simulation(SimulationSettings simulationSettings)
        {
            SimulationSettings = simulationSettings;
            World = new World(new Microsoft.Xna.Framework.Vector2(0, (float)SimulationSettings.Environment.Gravity / 10));
            ResetSimulation();
        }

        public void ResetSimulation()
        {
            World.Clear();
            World.ClearForces();
            foreach (var creatureMovementPattern in SimulationSettings.CreatureMovementPatterns)
            {
                var joints = creatureMovementPattern.Creature.CreatureStructure.Joints;
                foreach (var joint in joints)
                {
                    joint.Position *= creatureMovementPattern.Creature.CreatureStructure.Scale;
                }
                double minX = joints.Min(j => j.Position.X);
                double minY = joints.Min(j => j.Position.Y);
                foreach (var joint in joints)
                {
                    joint.Position = new Vector2(joint.Position.X - minX, (joint.Position.Y - minY));
                }
                var simulationEntity = new SimulationEntity(World, creatureMovementPattern);
                SimulationEntities.Add(simulationEntity);
            }

            Terrain = new Terrain(World, SimulationSettings);
            RemainingDeltaTime = 0;
            TotalTime = 0;
            SimulationReset?.Invoke();
        }

        /// <summary>
        /// Single Step
        /// </summary>
        /// <param name="elapsedTime">In seconds</param>
        public void Tick(float elapsedTime)
        {
            RemainingDeltaTime += elapsedTime;
            while (RemainingDeltaTime - SimulationSettings.TickStepSize >= 0)
            {
                TotalTime += SimulationSettings.TickStepSize;
                PerformTick(SimulationSettings.TickStepSize);
                RemainingDeltaTime -= SimulationSettings.TickStepSize;
            }
        }

        private void PerformTick(float deltaTime)
        {
            World.Step(deltaTime);
            foreach (var simulationEntity in SimulationEntities)
            {
                simulationEntity.PreWorldStep(deltaTime, TotalTime);
            }
            TimeStepCompleted?.Invoke();
        }

        /// <summary>
        /// Performs Tick operations until the given time is reached.
        /// </summary>
        /// <param name="time">Time in seconds</param>
        public void RunFor(float time)
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds * 1000 < time)
            {
                PerformTick(SimulationSettings.TickStepSize);
            }
        }

        public async Task<double> GetBoneWeightAsync(Bone bone, float sclae)
        {
            return await new Task<double>(() => GetBoneWeight(bone, sclae));
        }

        public double GetBoneWeight(Bone bone, float sclae)
        {
            var tempWorld = new World(World.Gravity);
            float length = (float)(bone.FirstJoint.Position - bone.SecondJoint.Position).Length;
            var rectangle = BodyFactory.CreateRectangle(tempWorld, length * sclae, bone.Width, bone.Density);
            tempWorld.ProcessChanges();
            tempWorld.Step(1);
            return rectangle.Mass;
        }

        public event Action TimeStepCompleted;
        public event Action SimulationReset;
    }
}