using System;

namespace GaussElimination
{
    public static class PivotFunctions
    {
        public static (int, int) Simple<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new() =>
            (initialIndex, initialIndex);

        public static (int, int) Partial<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var col = initialIndex;
            for (var i = initialIndex; i < matrix.GetLength(0); i++)
                if (matrix[i, initialIndex] > matrix[col, initialIndex])
                    col = i;

            return (col, initialIndex);
        }

        public static (int, int) Full<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var (col, row) = (initialIndex, initialIndex);
            for (var i = initialIndex; i < matrix.GetLength(0); i++)
                for (var j = initialIndex; j < matrix.GetLength(0); j++)
                    if (matrix[j, i] > matrix[col, row])
                        (col, row) = (i, j);

            return (col, row);
        }
    }
}