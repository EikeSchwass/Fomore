using System.Linq;
using Core.TerrainGenerator;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class DummyTests
    {
        [PreTest]
        public void PreTest() { }

        [PostTest]
        public void PostTest() { }

        [OneTimeSetUp]
        public void PreClass() { }

        [OneTimeTearDown]
        public void PostClass() { }

        [Test]
        public void MethodTest()
        {
            /*
            var generator = new LinearGenerator
            {
                Inclination = 1,
                StepSize = 10
            };
            generator.Generate().Take(200); */
        }
    }
}
