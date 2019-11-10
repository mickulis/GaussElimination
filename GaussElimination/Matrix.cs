using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GaussElimination
{
    public class Matrix<T> where T : ICalculateable<T>, new()
    {
        private readonly Number<T>[,] _initialMatrix;
        private Number<T>[,] _matrix;
        private int[] _variables;
        private Number<T>[] _output;
        public static bool debugInfo = false;

        public Matrix(Number<T>[,] matrix)
        {
            _initialMatrix = matrix;
            _matrix = (Number<T>[,])_initialMatrix.Clone();
        }

        public Matrix<T> Solve(Func<Number<T>[,], int, (int, int)> choseNextPivot)
        {
            _matrix = (Number<T>[,])_initialMatrix.Clone();
            _variables = new int[_matrix.GetLength(0)];
            _output = new Number<T>[_matrix.GetLength(0)];
            for (var i = 0; i < _variables.Length; i++)
                _variables[i] = i;

            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                if (debugInfo)
                {
                    Console.WriteLine("Next loop");
                    Print();
                }

                var (nextRow, nextColumn) = choseNextPivot(_matrix, i);
                if (nextColumn != i)
                {
                    if (debugInfo)
                        Console.WriteLine($"Swap columns: {i}, {nextColumn} -----------------");

                    SwapColumns(i, nextColumn, _matrix, _variables);
                    if (debugInfo)
                    {
                        Print();
                        Console.WriteLine("/////////////////////////////");
                    }
                }

                if(nextRow != i)
                {
                    if (debugInfo)
                        Console.WriteLine($"Swap rows: {i}, {nextRow} -----------------");

                    SwapRows(i, nextRow, _matrix);
                    if (debugInfo)
                    {
                        Print();
                        Console.WriteLine("/////////////////////////////");
                    }
                }

                if(debugInfo)
                    Console.WriteLine($"Subtracting {i} row from others");

                for (var j = 0; j < _matrix.GetLength(0); j++)
                {
                    Eliminate(_matrix, j, i);
                }
            }

            for(var i = 0; i < _output.Length; i++)
            {
                var position = _variables[i];
                _output[position] = _matrix[i, _matrix.GetLength(1) - 1] / _matrix[i, i];
            }
            if(debugInfo)
                Print();

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

        private static void SwapRows(int firstRowIndex, int secondRowIndex, Number<T>[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                var temp = matrix[firstRowIndex, i];
                matrix[firstRowIndex, i] = matrix[secondRowIndex, i];
                matrix[secondRowIndex, i] = temp;
            }
        }

        private static void SwapColumns(int firstColumnIndex, int secondColumnIndex, Number<T>[,] matrix, int[] variables)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var temp = matrix[i, firstColumnIndex];
                matrix[i, firstColumnIndex] = matrix[i, secondColumnIndex];
                matrix[i, secondColumnIndex] = temp;
            }

            var tempVariable = variables[firstColumnIndex];
            variables[firstColumnIndex] = variables[secondColumnIndex];
            variables[secondColumnIndex] = tempVariable;
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

        public Number<T> GetTotalRelativeError(Func<Number<T>, Number<T>> errorSumMethod)
        {
            var total = new Number<T>(0);
            var result = Multiply(_initialMatrix, _output);
            for(var i = 0; i < _output.Length; i++)
            {
                var expectedResult = _initialMatrix[i, _initialMatrix.GetLength(1) - 1];
                var actualResult = result[i];
                var error = (actualResult - expectedResult) / expectedResult;
                total += errorSumMethod(error);
            }
            return total;
        }

        public static Number<T>[] Multiply(Number<T>[,] matrix, Number<T>[] vector)
        {
            var output = new Number<T>[vector.Length];

            for (var row = 0; row < output.Length; row++)
            {
                var result = new Number<T>(0);
                for (var column = 0; column < output.Length; column++)
                    result += matrix[row, column] * vector[column];
                output[row] = result;
            }

            return output;
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

        public static Matrix<T> GenerateRandomEquationMatrixTEST(int size, int seed, int min, int max)
        {
            var rng = new Random(seed);
            var matrix = new Number<T>[size, size + 1];

            for(var x = 0; x < size + 1; x++)
            for(var y = 0; y < size; y++)
                matrix[y, x] = new Number<T>(rng.Next(min, max), 1);

            return new Matrix<T>(matrix);
        }

        #endregion


        public void Print()
        {
            for (var i = 0; i < _variables.Length; i++)
                Console.Write($"x{_variables[i]} ");
            Console.WriteLine();
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    Console.Write($"{_matrix[y, x]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }
    }
}