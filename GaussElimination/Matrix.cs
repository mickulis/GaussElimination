using System;

namespace GaussElimination
{
    public class Matrix<T>
    {
        private Number<T>[,] _matrix;

        private Matrix(Number<T>[,] matrix)
        {
            _matrix = matrix;
        }

        public static Matrix<double> GenerateRandomDoubleEquationMatrix(int size, int seed)
        {
            var rng = new Random(seed);
            var matrix = new Number<double>[size, size + 1];

            for(var x = 0; x < size + 1; x++)
                for(var y = 0; y < size + 1; y++)
                    matrix[y, x] = new Number<double>(1.0*rng.Next()/Int32.MaxValue);

            return new Matrix<double>(matrix);
        }

        public static Matrix<float> GenerateRandomFloatEquationMatrix(int size, int seed)
        {
            var rng = new Random(seed);
            var matrix = new Number<float>[size, size + 1];

            for(var x = 0; x < size + 1; x++)
            for(var y = 0; y < size + 1; y++)
                matrix[y, x] = new Number<float>(1.0f*rng.Next()/Int32.MaxValue);

            return new Matrix<float>(matrix);
        }

        public static Matrix<Rational> GenerateRandomRationalEquationMatrix(int size, int seed)
        {
            var rng = new Random(seed);
            var matrix = new Number<Rational>[size, size + 1];

            for(var x = 0; x < size + 1; x++)
            for(var y = 0; y < size + 1; y++)
                matrix[y, x] = new Number<Rational>(new Rational(rng.Next(), Int32.MaxValue));

            return new Matrix<Rational>(matrix);
        }
    }
}