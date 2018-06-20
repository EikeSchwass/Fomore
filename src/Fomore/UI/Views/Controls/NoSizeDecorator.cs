using System.Windows;
using System.Windows.Controls;

namespace Fomore.UI.Views.Controls
{
    public class NoSizeDecorator : Decorator
    {
        /// <inheritdoc />
        protected override Size MeasureOverride(Size constraint)
        {
            Child.Measure(new Size(0, 0));
            return new Size(0, 0);
        }
    }
}