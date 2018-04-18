using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
