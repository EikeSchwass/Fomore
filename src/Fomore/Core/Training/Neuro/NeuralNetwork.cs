// Eike Stein: Fomore/Core/NeuralNetwork.cs (2018/09/18)

using System;

namespace Core.Training.Neuro
{
    [Serializable]
    public class NeuralNetwork
    {
        private WeightMatrix[] WeightLayers { get; }

        public NeuralNetwork(params int[] layers)
        {
            if (layers.Length <= 1)
                throw new ArgumentException(nameof(layers));
            WeightLayers = new WeightMatrix[layers.Length - 1];
            CreateStructure(layers);

        }

        private void CreateStructure(int[] layers)
        {
            for (int weightMatrixLayer = 0; weightMatrixLayer < layers.Length - 1; weightMatrixLayer++)
            {
                int fromCount = layers[weightMatrixLayer];
                int toCount = layers[weightMatrixLayer];
                var weightMatrix = new WeightMatrix(new float[fromCount, toCount]);
                WeightLayers[weightMatrixLayer] = weightMatrix;
            }
        }
    }
}