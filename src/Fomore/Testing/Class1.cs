using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class Class1
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
        public void MethodTest() { }
    }
}
