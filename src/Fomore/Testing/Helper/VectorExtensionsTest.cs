
using System.Windows;
using Core;
using Fomore.UI.ViewModel.Helper;
using NUnit.Framework;

namespace Testing.Helper
{
    [TestFixture]
    public class VectorExtensionsTest
    {
        [Test]
        public void IsInsideRect_IsInsideRectTest_True()
        {
            bool isInsideRect = new Vector2(5, 10).IsInsideRect(new Rect(5, 5, 10, 10));
            Assert.True(isInsideRect);
        }
    }
}
