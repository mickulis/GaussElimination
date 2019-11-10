using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 3; i <= 20; i++)
            {
                if (i < 100 || i % 10 == 0)
                {
                    Console.Out.WriteLine(i);
                    try
                    {
                        var output = new List<(long time, string error, string info)>();
                        output.Add(SolveRandomSetOfEquations<Rational>(i, i, PivotChoiceMethod.Simple));
                        output.Add(SolveRandomSetOfEquations<DoubleBox>(i, i, PivotChoiceMethod.Simple));
                        output.Add(SolveRandomSetOfEquations<FloatBox>(i, i, PivotChoiceMethod.Simple));

                        output.Add(SolveRandomSetOfEquations<Rational>(i, i, PivotChoiceMethod.Partial));
                        output.Add(SolveRandomSetOfEquations<DoubleBox>(i, i, PivotChoiceMethod.Partial));
                        output.Add(SolveRandomSetOfEquations<FloatBox>(i, i, PivotChoiceMethod.Partial));

                        output.Add(SolveRandomSetOfEquations<Rational>(i, i, PivotChoiceMethod.Full));
                        output.Add(SolveRandomSetOfEquations<DoubleBox>(i, i, PivotChoiceMethod.Full));
                        output.Add(SolveRandomSetOfEquations<FloatBox>(i, i, PivotChoiceMethod.Full));

                        Save(output, i);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private static (long time, string error, string info) SolveRandomSetOfEquations<T>(int i, int seed, PivotChoiceMethod method) where T : ICalculateable<T>, new()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var matrix = Matrix<T>.GenerateRandomEquationMatrix(i, seed).Solve(PivotFunctions.Get<T>(method));
            watch.Stop();
            var error = matrix.GetTotalRelativeError(x => x * x);
//            Console.WriteLine(
//                $"Type: {typeof(T)}, size: {i},  method: {method} -> {(1.0f * watch.ElapsedMilliseconds / 1000).ToString()} // error: {error}");
            return (watch.ElapsedMilliseconds, error.ToString(), $"Type: {typeof(T)}, size: {i},  method: {method}");
        }

        private static void Save(List<(long time, string error, string info)> list, int size)
        {
            using (StreamWriter w = File.AppendText("output.csv"))
            {
                var outputLine = $"{size}";
                foreach ((var time, var error, var info) in list)
                {
                    outputLine = $"{outputLine}; {time}; {error}";
                }
                w.WriteLine(outputLine);
            }
        }
    }
}