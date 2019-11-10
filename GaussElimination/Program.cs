using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            int o = 3;
            for (int i = 3; i <= 100; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                var matrix = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                watch.Stop();
                Console.WriteLine($"{i} Simple: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix.GetTotalRelativeError(x => x * x)}");

                watch = System.Diagnostics.Stopwatch.StartNew();
                var matrix2 = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Partial);
                watch.Stop();

                Console.WriteLine($"{i} Partial: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix2.GetTotalRelativeError(x => x * x)}");

                //Matrix<Rational>.debugInfo = true;
                watch = System.Diagnostics.Stopwatch.StartNew();
                var matrix3 = Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Full);
                watch.Stop();

                Console.WriteLine($"{i} Full: {(watch.ElapsedMilliseconds/1000).ToString()} // error: {matrix3.GetTotalRelativeError(x => x * x)}");
            }
        }
    }
}