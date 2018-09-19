using System;
using System.Collections.Generic;
using System.Linq;
using Core.Training;
using Core.Training.Evolution;
using Core.Training.Neuro;
using NUnit.Framework;

namespace Testing.Traning
{
    [TestFixture]
    public class NeuralNetworkTests
    {
        [Test]
        public void NeuralNetworkOutputTest()
        {
            var neuralNetwork = new NeuralNetwork(0, 1, 2, 3, 2);

            var input = new[] { 0.4f, 0.12f };
            var calculateNetworkOutput = neuralNetwork.CalculateNetworkOutput(input);
            Assert.NotNull(calculateNetworkOutput);
            Assert.AreEqual(2, calculateNetworkOutput.Length);
        }
    }
}