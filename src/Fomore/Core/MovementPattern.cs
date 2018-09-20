using System;
using System.Linq;
using Core.Physics;
using Core.Training.Neuro;
using FarseerPhysics.Dynamics;

namespace Core
{
    [Serializable]
    public class MovementPattern
    {
        public MovementPattern Parent { get; }

        public string Name { get; set; }

        public int Iterations => Parent?.Iterations + 1 ?? 1;

        public DateTime CreationDate { get; }
        public NeuralNetwork NeuralNetwork { get; }

        public MovementPattern(MovementPattern parent, NeuralNetwork neuralNetwork)
        {
            Parent = parent;
            NeuralNetwork = neuralNetwork;
            CreationDate = DateTime.Now;
        }

        public MovementPattern Clone() => new MovementPattern(Parent?.Clone(), NeuralNetwork.Clone()) { Name = Name };

        public static MovementPattern CreateFromCreature(Creature creature)
        {
            var simulationEntity = new SimulationEntity(new World(Microsoft.Xna.Framework.Vector2.Zero),
                                                        new CreatureMovementPattern(creature, new MovementPattern(null, new NeuralNetwork(0, 1, 1, 1))));
            int outputs = simulationEntity.RevoluteJointsCount;
            int inputs = simulationEntity.GetNeuralInputs(0).ToList().Count; 

            int hiddenLayerCount = (int)Math.Floor(Math.Pow((inputs + outputs) / 2.0, 1 / 2.0));
            hiddenLayerCount = Math.Max(1, hiddenLayerCount);
            var hiddenLayers = new int[hiddenLayerCount];
            if (hiddenLayerCount > 0)
            {
                double decline = (outputs - inputs - 0.0) / hiddenLayerCount;


                for (int i = 0; i < hiddenLayerCount; i++)
                {
                    double current = Math.Ceiling((i + 1) * decline) + inputs;
                    hiddenLayers[i] = (int)current;
                }

            }
            var layers = new int[hiddenLayerCount + 1];

            Array.Copy(hiddenLayers, 0, layers, 1, hiddenLayers.Length - 1);
            layers[0] = inputs;
            layers[layers.Length - 1] = outputs;
            var neuralNetwork = new NeuralNetwork(0, 8f, layers);
            return new MovementPattern(null, neuralNetwork);
        }
    }
}