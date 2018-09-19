using System;
using Core.Training.Neuro;

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

        public MovementPattern Clone()
        {
            return new MovementPattern(Parent?.Clone(), Parent?.NeuralNetwork.Clone()) { Name = Name };
        }

        public MovementPattern(MovementPattern parent, NeuralNetwork neuralNetwork)
        {
            Parent = parent;
            NeuralNetwork = neuralNetwork;
            CreationDate = DateTime.Now;
        }
    }
}