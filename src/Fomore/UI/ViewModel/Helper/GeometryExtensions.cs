// Eike Stein: Fomore/UI/GeometryExtensions.cs (2018/06/25)

using System.Diagnostics;
using System.Windows;
using Core;
using static System.Math;

namespace Fomore.UI.ViewModel.Helper
{
    public static class GeometryExtensions
    {
        public static double GetDistanceToBone(this Vector2 p, Bone bone)
        {
            //See https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line#Cartesian_coordinates

            double x0 = p.X;
            double y0 = p.Y;
            double x1 = bone.FirstJoint.Position.X;
            double y1 = bone.FirstJoint.Position.Y;
            double x2 = bone.SecondJoint.Position.X;
            double y2 = bone.SecondJoint.Position.Y;
            double a = y2 - y1;
            double b = x1 - x2;
            double c = x2 * y1 - y2 * x1;

            double shortestDistance = Abs(a * x0 + b * y0 + c) / Sqrt(a * a + b * b);

            double shortestX = (b * (b * x0 - a * y0) - a * c) / (a * a + b * b);
            double shortestY = (a * (-b * x0 + a * y0) - b * c) / (a * a + b * b);

            if (ValueInBetween(shortestX, x1, x2) && ValueInBetween(shortestY, y1, y2))
                return shortestDistance;

            double distanceToFirstJoint = (bone.FirstJoint.Position - p).Length;
            double distanceToSecondJoint = (bone.SecondJoint.Position - p).Length;

            Debug.Assert(distanceToFirstJoint > shortestDistance);
            Debug.Assert(distanceToSecondJoint > shortestDistance);

            return Min(distanceToFirstJoint, distanceToSecondJoint);
        }

        public static bool IsBoneInside(this Rect rect, Bone bone)
        {
            if (bone.FirstJoint.Position.IsInsideRect(rect) || bone.SecondJoint.Position.IsInsideRect(rect))
                return true;

            bool lineAIntersects = LinesIntersect(bone.FirstJoint.Position,
                                                  bone.SecondJoint.Position,
                                                  rect.TopLeft.GetVector(),
                                                  rect.TopRight.GetVector());
            bool lineBIntersects = LinesIntersect(bone.FirstJoint.Position,
                                                  bone.SecondJoint.Position,
                                                  rect.TopRight.GetVector(),
                                                  rect.BottomRight.GetVector());
            bool lineCIntersects = LinesIntersect(bone.FirstJoint.Position,
                                                  bone.SecondJoint.Position,
                                                  rect.BottomRight.GetVector(),
                                                  rect.BottomLeft.GetVector());
            bool lineDIntersects = LinesIntersect(bone.FirstJoint.Position,
                                                  bone.SecondJoint.Position,
                                                  rect.BottomLeft.GetVector(),
                                                  rect.TopLeft.GetVector());

            return lineAIntersects || lineBIntersects || lineCIntersects || lineDIntersects;
        }

        private static Vector2 GetVector(this Point point) => new Vector2(point.X, point.Y);

        public static bool LinesIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            const double floatingPointTolerance = 0.0001;
            var r = a2 - a1;
            var s = b2 - b1;

            double uNumerator = CrossProduct(b1 - a1, r);
            double denominator = CrossProduct(r, s);

            if (Abs(uNumerator) <= floatingPointTolerance && Abs(denominator) <= floatingPointTolerance)
            {
                // They are coLlinear

                // Do they touch? (Are any of the points equal?)
                if (Equals(a1, b1) || Equals(a1, b2) || Equals(a2, b1) || Equals(a2, b2)) return true;
                // Do they overlap? (Are all the point differences in either direction the same sign)
                return !AllEqual(b1.X - a1.X < 0, b1.X - a2.X < 0, b2.X - a1.X < 0, b2.X - a2.X < 0) ||
                       !AllEqual(b1.Y - a1.Y < 0, b1.Y - a2.Y < 0, b2.Y - a1.Y < 0, b2.Y - a2.Y < 0);
            }

            if (Abs(denominator) <= floatingPointTolerance)
            {
                // lines are paralell
                return false;
            }

            double u = uNumerator / denominator;
            double t = CrossProduct(b1 - a1, s) / denominator;

            return t >= 0 && t <= 1 && u >= 0 && u <= 1;
        }

        private static bool AllEqual(params bool[] values)
        {
            bool firstValue = values[0];
            foreach (bool value in values)
            {
                if (value != firstValue)
                    return false;
            }

            return true;
        }

        private static double CrossProduct(Vector2 a, Vector2 b) => a.X * b.Y - a.Y * b.X;

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

        private static bool ValueInBetween(double shortestX, double x1, double x2)
        {
            if (x1 < x2)
                return shortestX > x1 && shortestX < x2;
            return shortestX < x1 && shortestX > x2;
        }
    }
}