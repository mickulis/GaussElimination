using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            int o = 4;
            for (int i = o; i <= o; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var matrix = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                Matrix<DoubleBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                Matrix<FloatBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                watch.Stop();

                Console.WriteLine($"{i} Simple: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix.GetTotalRelativeError(x => x * x)}");

                watch = System.Diagnostics.Stopwatch.StartNew();
                matrix = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Partial);
                Matrix<DoubleBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                Matrix<FloatBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                watch.Stop();

                Console.WriteLine($"{i} Partial: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix.GetTotalRelativeError(x => x * x)}");

                watch = System.Diagnostics.Stopwatch.StartNew();
                matrix = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Full);
                watch.Stop();

                Console.WriteLine($"{i} Full: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix.GetTotalRelativeError(x => x * x)}");
            }
        }
    }
}