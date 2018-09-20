// Eike Stein: Fomore/Core/NeuralNetwork.cs (2018/09/18)

using System;
using System.Diagnostics;
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

        public NeuralNetwork MutateNetworkWeights(float mutationChance, float standardDeviation)
        {
            var weightLayers = WeightLayers.Select(w => w.GetClonedWeights()).ToArray();
            int weightLayerIndex = AdvancedRandom.Random.Next(0, weightLayers.Length);
            int colIndex = AdvancedRandom.Random.Next(0, weightLayers[weightLayerIndex].GetLength(0));
            int rowIndex = AdvancedRandom.Random.Next(0, weightLayers[weightLayerIndex].GetLength(1));

            int mutationVariant = GetMutationVariant(AdvancedRandom.Random.NextDouble());

            switch (mutationVariant)
            {
                case 0:
                    {
                        float nextNormal = (float)AdvancedRandom.NextNormal(1, standardDeviation);
                        float current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                        weightLayers[weightLayerIndex][colIndex, rowIndex] =current* nextNormal;
                        break;
                    }
                case 1:
                    {
                        float nextNormal = (float)AdvancedRandom.NextNormal(0, standardDeviation / 10);
                        float current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                        weightLayers[weightLayerIndex][colIndex, rowIndex] =current+ nextNormal;
                        break;
                    }
                case 2:
                    {
                        float current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                        weightLayers[weightLayerIndex][colIndex, rowIndex] =current* -1;
                        break;
                    }

                default:
                    throw new InvalidOperationException();
            }

            int unequalCount = 0;

            for (int i = 0; i < WeightLayers.Length; i++)
            {
                for (int j = 0; j < WeightLayers[i].ColumnCount; j++)
                {
                    for (int k = 0; k < WeightLayers[i].RowCount; k++)
                    {
                        if (!WeightLayers[i][j, k].Equals(weightLayers[i][j, k]))
                            unequalCount++;
                    }
                }
            }
            Debug.Assert(unequalCount == 1);

            return new NeuralNetwork(weightLayers);
        }

        private int GetMutationVariant(double nextDouble)
        {
            if (nextDouble < 0.9)
                return 0;
            if (nextDouble < 0.98)
                return 1;
            return 2;
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

        public static float Sigmoid(float t)
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