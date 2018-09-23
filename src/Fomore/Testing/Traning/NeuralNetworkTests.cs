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

            var input = new[] { 0.4, 0.12 };
            var calculateNetworkOutput = neuralNetwork.CalculateNetworkOutput(input);
            Assert.NotNull(calculateNetworkOutput);
            Assert.AreEqual(2, calculateNetworkOutput.Length);
        }
    }
}