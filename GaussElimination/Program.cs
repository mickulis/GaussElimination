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
            //CalculateAll();
            CalculateAverageErrors();
        }

        private static void CalculateAverageErrors()
        {
            for (var i = 1; i < 500; i++)
            {
                using (StreamWriter w = File.AppendText("sum_of_errors.csv"))
                {
                    Console.Out.WriteLine(i);
                    var doubleTotalSimple = new DoubleBox(0);
                    var doubleTotalPartial = new DoubleBox(0);
                    var doubleTotalFull = new DoubleBox(0);

                    var floatTotalSimple = new FloatBox(0);
                    var floatTotalPartial = new FloatBox(0);
                    var floatTotalFull = new FloatBox(0);
                    for (var j = 0; j < 10; j++)
                    {
                        var doubleErrorSimple = SolveAndGetError<DoubleBox>(i, j, PivotChoiceMethod.Simple);
                        doubleTotalSimple = doubleTotalSimple.Add(doubleErrorSimple);

                        var doubleErrorPartial = SolveAndGetError<DoubleBox>(i, j, PivotChoiceMethod.Partial);
                        doubleTotalPartial = doubleTotalPartial.Add(doubleErrorPartial);

                        var doubleErrorFull = SolveAndGetError<DoubleBox>(i, j, PivotChoiceMethod.Full);
                        doubleTotalFull = doubleTotalFull.Add(doubleErrorFull);

                        var floatErrorSimple = SolveAndGetError<FloatBox>(i, j, PivotChoiceMethod.Simple);
                        floatTotalSimple = floatTotalSimple.Add(floatErrorSimple);

                        var floatErrorPartial = SolveAndGetError<FloatBox>(i, j, PivotChoiceMethod.Partial);
                        floatTotalPartial = floatTotalPartial.Add(floatErrorPartial);

                        var floatErrorFull = SolveAndGetError<FloatBox>(i, j, PivotChoiceMethod.Full);
                        floatTotalFull = floatTotalFull.Add(floatErrorFull);
                    }

                    var outputLine =
                        $"{i}; {doubleTotalSimple}; {doubleTotalPartial}; {doubleTotalFull};{floatTotalSimple}; {floatTotalPartial}; {floatTotalFull}";
                    w.WriteLine(outputLine);
                }
            }
        }

        private static T SolveAndGetError<T>(int i, int seed, PivotChoiceMethod method) where T : ICalculateable<T>, new()
        {
            var matrix = Matrix<T>.GenerateRandomEquationMatrix(i, seed);
            matrix = matrix.Solve(PivotFunctions.Get<T>(method));
            var error = matrix.GetTotalRelativeError(x => x*x);
            return error.Absolute().Value;
        }

        private static void CalculateAll()
        {
            for (var i = 1; i <= 500; i++)
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
            var matrix = Matrix<T>.GenerateRandomEquationMatrix(i, seed);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            matrix = matrix.Solve(PivotFunctions.Get<T>(method));
            watch.Stop();
            var error = matrix.GetTotalRelativeError(x => x * x);
            return (watch.ElapsedMilliseconds, error.ToString(), $"Type: {typeof(T)}, size: {i},  method: {method}");
        }

        private static void Save(List<(long time, string error, string info)> list, int size)
        {
            using (StreamWriter w = File.AppendText("output_optimized_without_timing_generation.csv"))
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