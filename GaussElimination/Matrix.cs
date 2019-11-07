using System;

namespace GaussElimination
{
    public class Matrix<T> where T : ICalculateable<T>, new()
    {
        private readonly Number<T>[,] _initialMatrix;
        private Number<T>[,] _matrix;

        public Matrix(Number<T>[,] matrix)
        {
            _initialMatrix = matrix;
            _matrix = _initialMatrix;
        }

        public Matrix<T> Solve(Func<Number<T>[,], int, (int, int)> choseNext)
        {
            _matrix = _initialMatrix;

            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                var next = choseNext(_matrix, i);
                // if next not in (i, i) position, swap

                // subtract row from all other rows with correct multiplier + zero i-th column
                for (var j = 0; j < _matrix.GetLength(0); j++)
                {
                    Eliminate(_matrix, j, i);
                }

            }
            //
            //
            return this;
        }

        private static void Eliminate(Number<T>[,] matrix, int targetRowIndex, int sourceRowIndex)
        {
            if (targetRowIndex == sourceRowIndex)
                return;

            var pivotColumn = sourceRowIndex;
            if (matrix[sourceRowIndex, pivotColumn] == new Number<T>(0))
                throw new ArithmeticException("Pivot value is 0");
            var multiplier = (matrix[targetRowIndex, pivotColumn] / matrix[sourceRowIndex, pivotColumn]).Negate();
            AddRows(sourceRowIndex, targetRowIndex, multiplier, matrix);
            matrix[targetRowIndex, pivotColumn] = new Number<T>(0);    // in some cases b - a*(b/a) might not produce exact 0 and without this the errors would be even higher
        }

        private static void SwapRows(int firstIndex, int secondIndex, Number<T>[,] matrix)
        {
            var matrixWidth = matrix.GetLength(1);
            var intSize = sizeof(int);
            var rowSize = intSize * matrixWidth;

            var tempRow = new Number<T>[matrixWidth];
            Buffer.BlockCopy(matrix, firstIndex*rowSize, tempRow, 0, rowSize);
            Buffer.BlockCopy(matrix, secondIndex*rowSize, matrix, firstIndex*rowSize, rowSize);
            Buffer.BlockCopy(tempRow, 0, matrix, secondIndex*rowSize, rowSize);
        }

        private static void AddRows(int sourceRowIndex, int targetRowIndex, Number<T> multiplier, Number<T>[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(1); i++)
                matrix[targetRowIndex, i] += matrix[sourceRowIndex, i] * multiplier;
        }

        private static void MultiplyRow(int index, Number<T> multiplier, Number<T>[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(1); i++)
                matrix[index, i] = multiplier * matrix[index, i];
        }

        #region Generators

        public static Matrix<T> GenerateRandomEquationMatrix(int size, int seed)
        {
            var rng = new Random(seed);
            var matrix = new Number<T>[size, size + 1];

            for(var x = 0; x < size + 1; x++)
                for(var y = 0; y < size; y++)
                    matrix[y, x] = new Number<T>(rng.Next(int.MinValue, int.MaxValue), int.MaxValue);

            return new Matrix<T>(matrix);
        }

        #endregion


        public void Print()
        {
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    Console.Write($"{_matrix[y, x]} ");
                }
                Console.WriteLine();
            }
        }
    }
}