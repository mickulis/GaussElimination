using System;

namespace GaussElimination
{
    public class Matrix<T> where T : ICalculateable<T>, new()
    {
        private readonly Number<T>[,] _initialMatrix;
        private Number<T>[,] _matrix;
        private int[] _variables;
        private Number<T>[] _output;

        public Matrix(Number<T>[,] matrix)
        {
            _initialMatrix = matrix;
            _matrix = (Number<T>[,])_initialMatrix.Clone();
        }

        public Matrix<T> Solve(Func<Number<T>[,], int, (int, int)> choseNext)
        {
            _matrix = (Number<T>[,])_initialMatrix.Clone();
            _variables = new int[_matrix.GetLength(0)];
            _output = new Number<T>[_matrix.GetLength(0)];
            for (var i = 0; i < _variables.Length; i++)
                _variables[i] = i;

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
            for(var i = 0; i < _output.Length; i++)
            {
                var position = _variables[i];
                _output[i] = _matrix[position, _matrix.GetLength(1) - 1] / _matrix[position, position];
            }
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

        private static (Number<T> absolute, Number<T> relative) CalculateErrors(int rowIndex, Number<T>[,] matrix, Number<T>[] outputVector)
        {
            var expectedResult = matrix[rowIndex, matrix.GetLength(1) - 1];

            var actualResult = new Number<T>(0);
            for (var i = 0; i < outputVector.Length; i++)
                actualResult += matrix[rowIndex, i] * outputVector[i];

            var absoluteError = expectedResult - actualResult;
            return (absoluteError, absoluteError/expectedResult);
        }

        public Number<T> GetTotalRelativeError(Func<Number<T>, Number<T>> errorCalculation)
        {
            var total = new Number<T>(0);
            for(var i = 0; i < _output.Length; i++)
            {
                var error = CalculateErrors(i, _initialMatrix, _output).relative;
                total += errorCalculation(error);
            }

            return total;
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