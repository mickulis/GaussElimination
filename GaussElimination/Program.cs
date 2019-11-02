using System;

namespace GaussElimination
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Number<double>(2.0);
            var b = new Number<double>(3.0);
            Console.WriteLine((a+b).Value);
        }
    }
}