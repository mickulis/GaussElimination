using System;
using GaussElimination;
using Xunit;

namespace GaussEliminationTests
{
    public class RationalTest
    {
        [Theory]
        [InlineData(1, 2, 1, 2)]
        [InlineData(2, 2, 1, 1)]
        [InlineData(4, -2, -2, 1)]
        [InlineData(-12, -8, 3, 2)]
        [InlineData(0, 30, 0, 1)]
        public void ParseTwoInts(int numeratorIn, int denominatorIn, int numeratorOut, int denominatorOut)
        {
            var rationalNumber = new Rational(numeratorIn, denominatorIn);
            Assert.Equal((numeratorOut, denominatorOut),
                (rationalNumber._numerator, rationalNumber._denominator));
        }


        [Theory]
        [InlineData(1, 2, 3, 2, 2, 1)]
        [InlineData(1, 2, 3, 4, 5, 4)]
        [InlineData(-1, 2, 1, 2, 0, 1)]
        [InlineData(-1, 3, -2, 4, -5, 6)]
        public void Add(int aNom, int aDen, int bNom, int bDen, int outNom, int outDen)
        {
            var first = new Rational(aNom, aDen);
            var second = new Rational(bNom, bDen);

            var result = first + second;
            Assert.Equal((outNom, outDen),
                (result._numerator, result._denominator));
        }

        [Theory]
        [InlineData(1, 2, 3, 2, 2, 1)]
        [InlineData(1, 2, 3, 4, 5, 4)]
        [InlineData(-1, 2, 1, 2, 0, 1)]
        [InlineData(-1, 3, -2, 4, -5, 6)]
        public void Subtract(int numeratorFirstIn, int denominatorFirstIn, int numeratorSecondIn, int denominatorSecondIn, int numeratorOut, int denominatorOut)
        {
            var first = new Rational(numeratorFirstIn, denominatorFirstIn);
            var second = new Rational(numeratorSecondIn, denominatorSecondIn);

            var result = first - second;
            Assert.Equal((numeratorOut, denominatorOut),
                (result._numerator, result._denominator));
        }

        [Theory]
        [InlineData(1, 2, 3, 2, 2, 1)]
        [InlineData(1, 2, 3, 4, 5, 4)]
        [InlineData(-1, 2, -1, 2, 0, 1)]
        [InlineData(-1, 3, -2, 4, -5, 6)]
        public void Multiply(int numeratorFirstIn, int denominatorFirstIn, int numeratorSecondIn, int denominatorSecondIn, int numeratorOut, int denominatorOut)
        {
            var first = new Rational(numeratorFirstIn, denominatorFirstIn);
            var second = new Rational(numeratorSecondIn, denominatorSecondIn);

            var result = first * second;
            Assert.Equal((numeratorOut, denominatorOut),
                (result._numerator, result._denominator));
        }

        [Theory]
        [InlineData(1, 2, 3, 2, 2, 1)]
        [InlineData(1, 2, 3, 4, 5, 4)]
        [InlineData(-1, 2, 1, 2, 0, 1)]
        [InlineData(-1, 3, -2, 4, -5, 6)]
        public void Divide(int numeratorFirstIn, int denominatorFirstIn, int numeratorSecondIn, int denominatorSecondIn, int numeratorOut, int denominatorOut)
        {
            var first = new Rational(numeratorFirstIn, denominatorFirstIn);
            var second = new Rational(numeratorSecondIn, denominatorSecondIn);

            var result = first / second;
            Assert.Equal((numeratorOut, denominatorOut),
                (result._numerator, result._denominator));
        }
    }
}