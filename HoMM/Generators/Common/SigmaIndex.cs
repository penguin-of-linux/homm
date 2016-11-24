using System;
using System.Collections.Generic;

namespace HoMM.Generators
{
    public class SigmaIndex : Vector2i
    {
        public static readonly SigmaIndex Zero = new SigmaIndex(0, 0);

        public SigmaIndex(int y, int x) : base(x, y) { }

        public IEnumerable<SigmaIndex> Neighborhood
        {
            get
            {
                for (var dy = -1; dy < 2; dy++)
                    for (var dx = -1; dx < 2; dx++)
                        if (dx * dx + dy * dy != 0 && dy * dx * dx != 1)
                            yield return new SigmaIndex(Y + dy + (X % 2) * dx * dx, X + dx);
            }
        }

        public SigmaIndex DiagonalMirror(MapSize size)
        {
            return new SigmaIndex(size.Y - Y - 1, size.X - X - 1);
        }

        public bool IsInside(MapSize size)
        {
            return X >= 0 && X < size.X && Y >= 0 && Y < size.Y;
        }

        public bool IsAboveDiagonal(MapSize size)
        {
            return Y < size.Y - (float)X / size.X * size.Y - 1;
        }

        public SigmaIndex AboveDiagonal(MapSize size)
        {
            return IsAboveDiagonal(size) ? this : DiagonalMirror(size);
        }

        public static IEnumerable<SigmaIndex> Square(MapSize size)
        {
            for (int y = 0; y < size.Y; ++y)
                for (int x = 0; x < size.X; ++x)
                    yield return new SigmaIndex(y, x);
        }

        public double EuclideanDistance(SigmaIndex other)
        {
            var thisFixY = Y + 0.5 * (X % 2);
            var otherFixY = other.Y + 0.5 * (other.X % 2);
            return Math.Sqrt(Math.Pow(X-other.X, 2) + Math.Pow(thisFixY-otherFixY, 2));
        }

        public double ManhattanDistance(SigmaIndex other)
        {
            var thisFixY = Y + 0.5 * (X % 2);
            var otherFixY = other.Y + 0.5 * (other.X % 2);
            return Math.Abs(X-other.X) + Math.Abs(thisFixY-otherFixY);
        }
    }
}
