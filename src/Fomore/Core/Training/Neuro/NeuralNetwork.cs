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

        private NeuralNetwork(double[][,] weightLayers)
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
                var weightMatrix = new WeightMatrix(new double[fromCount, toCount]);
                WeightLayers[weightMatrixLayer] = weightMatrix;
            }
            InitializeWeights(mean, standardDeviation);
        }

        private void InitializeWeights(float mean, float standardDeviation)
        {
            for (int i = 0; i < WeightLayers.Length; i++)
            {
                var currentLayer = WeightLayers[i];
                var weights = new double[currentLayer.ColumnCount, currentLayer.RowCount];
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

        public NeuralNetwork MutateNetworkWeights(double mutationChance, double standardDeviation)
        {
            standardDeviation = AdvancedRandom.NextNormal(1, 0.5);
            var weightLayers = WeightLayers.Select(w => w.GetClonedWeights()).ToArray();
            int numberOfMutatedWeights = AdvancedRandom.Random.Next(0, 12);
            if (AdvancedRandom.Random.NextDouble() < 0.5)
                numberOfMutatedWeights = AdvancedRandom.Random.NextDouble() < 0.8 ? 1 : 0;

            for (int i = 0; i < numberOfMutatedWeights; i++)
            {
                int weightLayerIndex = AdvancedRandom.Random.Next(0, weightLayers.Length);
                int colIndex = AdvancedRandom.Random.Next(0, weightLayers[weightLayerIndex].GetLength(0));
                int rowIndex = AdvancedRandom.Random.Next(0, weightLayers[weightLayerIndex].GetLength(1));

                int mutationVariant = GetMutationVariant(AdvancedRandom.Random.NextDouble());

                switch (mutationVariant)
                {
                    case 0:
                        {
                            double nextNormal = (AdvancedRandom.Random.NextDouble() * standardDeviation) + 1 - standardDeviation / 2;
                            double current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                            weightLayers[weightLayerIndex][colIndex, rowIndex] = current * nextNormal;
                            break;
                        }
                    case 1:
                        {
                            double nextNormal = (AdvancedRandom.Random.NextDouble() - 0.5) * standardDeviation;
                            double current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                            weightLayers[weightLayerIndex][colIndex, rowIndex] = current + nextNormal;
                            break;
                        }
                    case 2:
                        {
                            double current = weightLayers[weightLayerIndex][colIndex, rowIndex];
                            weightLayers[weightLayerIndex][colIndex, rowIndex] = current * -1;
                            break;
                        }

                    default:
                        throw new InvalidOperationException();
                }
            }

            return new NeuralNetwork(weightLayers);
        }

        private int GetMutationVariant(double nextDouble)
        {
            if (nextDouble < 0.5)
                return 0;
            if (nextDouble < 0.9)
                return 1;
            return 2;
        }

        /// <summary>
        /// Calculates the output of the neural network with Sigmoid-Activation and Bias Neurons.
        /// </summary>
        /// <param name="input">Pre normalized input</param>
        /// <returns>The normalized output of the network</returns>
        public double[] CalculateNetworkOutput(double[] input)
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

        public static double Sigmoid(double t)
        {
            double exp = Exp(t);
            return exp / (exp + 1);
        }

        public NeuralNetwork Clone()
        {
            return new NeuralNetwork(WeightLayers.Select(w => w.GetClonedWeights()).ToArray());
        }
    }
}