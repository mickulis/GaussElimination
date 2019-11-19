using GaussElimination;
using Xunit;

namespace GaussEliminationTests
{
    public class MatrixVectorMultiplicationTest
    {
        [Fact]
        public void MultiplyMatrixAndVector()
        {
            Number<DoubleBox>[,] matrix =
            {
                { new Number<DoubleBox>(1), new Number<DoubleBox>(2), new Number<DoubleBox>(3) },
                { new Number<DoubleBox>(4), new Number<DoubleBox>(5), new Number<DoubleBox>(6) },
                { new Number<DoubleBox>(7), new Number<DoubleBox>(8), new Number<DoubleBox>(9) },
            };

            Number<DoubleBox>[] vector =
            {
                new Number<DoubleBox>(5),
                new Number<DoubleBox>(1),
                new Number<DoubleBox>(-10)
            };

            var output = Matrix<DoubleBox>.Multiply(matrix, vector);

            Assert.Equal("-23", output[0].ToString());
            Assert.Equal("-35", output[1].ToString());
            Assert.Equal("-47", output[2].ToString());
        }


    }
}