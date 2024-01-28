using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Components
{
    internal static class Utils
    {
        public static Point Center(this Rectangle rect) {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

        public static Point TopLeft(this Rectangle rect) {
            return new Point(rect.Left, rect.Top);
        }

        public static Rectangle MoveByDelta(this Rectangle rect, Point delta) {
            return new Rectangle(rect.X + delta.X, rect.Y + delta.Y, rect.Width, rect.Height);
        }

        public static Rectangle Expand(this Rectangle rect, int size) {
            return new Rectangle(rect.X - size, rect.Y - size, rect.Width + size, rect.Height + size);
        }

        public static Point Multiply(this Point p, double mul) {
            return new Point((int)(p.X * mul), (int)(p.Y * mul));
        }

        public static Point Delta(this Point p, Point other) {
            return new Point(p.X - other.X, p.Y - other.Y);
        }

        public static Point Add(this Point p, Point other) {
            return new Point(p.X + other.X, p.Y + other.Y);
        }

        public static double Distance(this Point p1, Point p2) {
            int sideA = Math.Abs(p1.X - p2.X);
            int sideB = Math.Abs(p1.Y - p2.Y);

            return Math.Sqrt(sideA * sideA + sideB * sideB);
        }

        public static Point Lerp(this Point p1, Point p2, double amount) {
            amount = Math.Min(amount, 1);
            return p1.Add(p2.Delta(p1).Multiply(amount));
        }

        public static bool IsInRadius(this Point p1, Point p2, double radius) {
            return p2.Distance(p1) <= radius;
        }
    }
}
