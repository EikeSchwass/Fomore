// Eike Stein: Fomore/Testing/MathTests.cs (2018/09/19)

using System;
using Core;
using NUnit.Framework;

namespace Testing.Helper
{
    [TestFixture]
    public class MathTests
    {
        [Test]
        public void GetAngleTest1()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(0, -1);
            double expected = 270 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }
        [Test]
        public void GetAngleTest2()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(1, -1);
            double expected = 315 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }
        [Test]
        public void GetAngleTest3()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(1, 0);
            double expected = 0 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }

        [Test]
        public void GetAngleTest4()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(1, 1);
            double expected = 45 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }

        [Test]
        public void GetAngleTest5()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(0, 1);
            double expected = 90 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }


        [Test]
        public void GetAngleTest6()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(-1, 1);
            double expected = 135 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }


        [Test]
        public void GetAngleTest7()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(-1, 0);
            double expected = 180 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }

        [Test]
        public void GetAngleTest8()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(-1, -1);
            double expected = 225 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }

        [Test]
        public void GetAngleTest9()
        {
            var from = new Vector2(0, 0);
            var to = new Vector2(0, -1);
            double expected = 270 * Math.PI / 180;
            double actual = from.GetAngleTowards(to);

            Assert.AreEqual(expected, actual, 0.001);
        }

    }
}