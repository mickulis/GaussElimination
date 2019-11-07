using System;

namespace GaussElimination
{
    public static class PivotFunctions
    {
        public static (int, int) Simple<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new() =>
            (initialIndex, initialIndex);

        public static (int, int) Partial<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var y = initialIndex;
            for (var i = initialIndex; i < matrix.Length; i++)
                if (matrix[i, initialIndex] > matrix[y, initialIndex])
                    y = i;

            return (y, initialIndex);
        }

        public static (int, int) Full<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var (x, y) = (initialIndex, initialIndex);
            for (var i = initialIndex; i < matrix.Length; i++)
                for (var j = initialIndex; j < matrix.Length; j++)
                    if (matrix[j, i] > matrix[y, x])
                        (x, y) = (i, j);

            return (x, y);
        }
    }
}