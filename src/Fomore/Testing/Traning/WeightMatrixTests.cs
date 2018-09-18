// Eike Stein: Fomore/Testing/WeightMatrixTests.cs (2018/09/18)

using Core.Training.Neuro;
using NUnit.Framework;

namespace Testing.Traning
{
    [TestFixture]
    public class WeightMatrixTests
    {
        [Test]
        public void WeightMatrixMultiplyTest1()
        {
            var weights = new float[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            var weightMatrix = new WeightMatrix(weights);

            var values = new float[] { 5, 7 };

            var result = values * weightMatrix;

            Assert.AreEqual(33, result[0]);
            Assert.AreEqual(45, result[1]);
            Assert.AreEqual(57, result[2]);
        }

        [Test]
        public void WeightMatrixMultiplyTest2()
        {
            var weights = new float[,] { { 2, 2, 2 }, { 4, 4, 4 } };
            var weightMatrix = new WeightMatrix(weights);

            var values = new float[] { 2, 3 };

            var result = values * weightMatrix;

            Assert.AreEqual(16, result[0]);
            Assert.AreEqual(16, result[1]);
            Assert.AreEqual(16, result[2]);
        }
    }

}