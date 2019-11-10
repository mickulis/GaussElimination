using System;
using System.Diagnostics;
using System.Dynamic;

namespace GaussElimination
{
    public static class PivotFunctions
    {
        public static Func<Number<T>[,], int, (int, int)> Get<T>(PivotChoiceMethod method)
            where T : ICalculateable<T>, new()
        {
            switch (method)
            {
                case PivotChoiceMethod.Simple: return Simple;
                case PivotChoiceMethod.Partial: return Partial;
                case PivotChoiceMethod.Full: return Full;
            }
            throw new ArgumentException("Invalid method");
        }


        public static (int, int) Simple<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new() =>
            (initialIndex, initialIndex);

        public static (int, int) Partial<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var maxRow = initialIndex;
            for (var currentRow = initialIndex; currentRow < matrix.GetLength(0); currentRow++)
                if (matrix[currentRow, initialIndex].Absolute() > matrix[maxRow, initialIndex].Absolute())
                    maxRow = currentRow;

            return (maxRow, initialIndex);
        }

        public static (int, int) Full<T>(Number<T>[,] matrix, int initialIndex) where T : ICalculateable<T>, new()
        {
            var (maxRow, maxColumn) = (initialIndex, initialIndex);
            for (var currentColumn = initialIndex; currentColumn < matrix.GetLength(0); currentColumn++)
                for (var currentRow = initialIndex; currentRow < matrix.GetLength(0); currentRow++)
                    if (matrix[currentRow, currentColumn].Absolute() > matrix[maxRow, maxColumn].Absolute())
                        (maxRow, maxColumn) = (currentRow, currentColumn);

            return (maxRow, maxColumn);
        }
    }
}