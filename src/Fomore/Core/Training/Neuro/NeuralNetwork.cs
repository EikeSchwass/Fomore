// Eike Stein: Fomore/Core/NeuralNetwork.cs (2018/09/18)

using System;
using System.Linq;
using static System.Math;

namespace Core.Training.Neuro
{
    [Serializable]
    public class NeuralNetwork : ICloneable<NeuralNetwork>
    {
        private WeightMatrix[] WeightLayers { get; }

        public NeuralNetwork(float meanWeight, float standardDeviation, params int[] layers)
        {
            if (layers.Length <= 1)
                throw new ArgumentException(nameof(layers));
            WeightLayers = new WeightMatrix[layers.Length - 1];
            CreateStructure(meanWeight, standardDeviation, layers);
        }

        private NeuralNetwork(float[][,] weightLayers)
        {
            WeightLayers = new WeightMatrix[weightLayers.Length];
            for (int i = 0; i < WeightLayers.Length; i++)
            {
                WeightLayers[i] = new WeightMatrix(weightLayers[i]);
            }
        }

        private void CreateStructure(float mean, float standardDeviation, int[] layers)
        {
            for (int weightMatrixLayer = 0; weightMatrixLayer < layers.Length - 1; weightMatrixLayer++)
            {
                int fromCount = layers[weightMatrixLayer] + 1;
                int toCount = layers[weightMatrixLayer + 1];
                var weightMatrix = new WeightMatrix(new float[fromCount, toCount]);
                WeightLayers[weightMatrixLayer] = weightMatrix;
            }
            InitializeWeights(mean, standardDeviation);
        }

        private void InitializeWeights(float mean, float standardDeviation)
        {
            for (int i = 0; i < WeightLayers.Length; i++)
            {
                var currentLayer = WeightLayers[i];
                var weights = new float[currentLayer.ColumnCount, currentLayer.RowCount];
                for (int c = 0; c < weights.GetLength(0); c++)
                {
                    for (int r = 0; r < weights.GetLength(1); r++)
                    {
                        weights[c, r] = (float)AdvancedRandom.NextNormal(mean, standardDeviation);
                    }
                }
                WeightLayers[i] = new WeightMatrix(weights);
            }
        }

        public NeuralNetwork MutateNetworkWeights() => MutateNetworkWeights(0.25f);

        public NeuralNetwork MutateNetworkWeights(float standardDeviation)
        {
            var weightLayers = WeightLayers.Select(w => w.GetClonedWeights()).ToArray();
            for (int i = 0; i < WeightLayers.Length; i++)
            {
                var currentLayer = weightLayers[i];
                for (int c = 0; c < currentLayer.GetLength(0); c++)
                {
                    for (int r = 0; r < currentLayer.GetLength(1); r++)
                    {
                        float nextNormal = (float)AdvancedRandom.NextNormal(0, standardDeviation);
                        currentLayer[c, r] += nextNormal;
                    }
                }
                weightLayers[i] = currentLayer;
            }
            return new NeuralNetwork(weightLayers);
        }

        /// <summary>
        /// Calculates the output of the neural network with Sigmoid-Activation and Bias Neurons.
        /// </summary>
        /// <param name="input">Pre normalized input</param>
        /// <returns>The normalized output of the network</returns>
        public float[] CalculateNetworkOutput(float[] input)
        {
            var currentValues = input;
            for (int i = 0; i < WeightLayers.Length; i++)
            {
                Array.Resize(ref currentValues, currentValues.Length + 1);
                currentValues[currentValues.Length - 1] = 1;
                var summedValues = currentValues * WeightLayers[i];
                for (int j = 0; j < summedValues.Length; j++)
                {
                    summedValues[j] = Sigmoid(summedValues[j]);
                }

                currentValues = summedValues;
            }

            return currentValues;
        }

        private float Sigmoid(float t)
        {
            double exp = Exp(t);
            return (float)(exp / (exp + 1));
        }

        public NeuralNetwork Clone()
        {
            return new NeuralNetwork(WeightLayers.Select(w => w.GetClonedWeights()).ToArray());
        }
    }
}