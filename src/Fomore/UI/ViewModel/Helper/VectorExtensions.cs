using System.Windows;
using Core;

namespace Fomore.UI.ViewModel.Helper
{
    public static class VectorExtensions
    {
        public static bool IsInsideRect(this Vector2 vector, double x, double y, double width, double height) =>
            IsInsideRect(vector, new Rect(x, y, width, height));

        public static bool IsInsideRect(this Vector2 vector, Rect rect)
        {
            if (vector.X < rect.Left)
                return false;
            if (vector.Y < rect.Top)
                return false;
            if (vector.X > rect.Right)
                return false;
            if (vector.Y > rect.Bottom)
                return false;
            return true;
        }
    }
}