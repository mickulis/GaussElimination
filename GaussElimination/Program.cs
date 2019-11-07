using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 1000; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                Matrix<Rational>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                //Matrix<DoubleBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                //Matrix<FloatBox>.GenerateRandomEquationMatrix(i, 3).Solve(PivotFunctions.Simple);
                watch.Stop();

                Console.WriteLine($"{i}: {(watch.ElapsedMilliseconds/1000).ToString()}");
            }
        }
    }
}