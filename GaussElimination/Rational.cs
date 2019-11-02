using System.Numerics;

namespace GaussElimination
{
    public class Rational
    {
        public readonly BigInteger Numerator;
        public readonly BigInteger Denominator;

        #region Constructors
        public Rational(int value)
        {
            Numerator = value;
            Denominator = 1;
        }

        public Rational(BigInteger value)
        {
            Numerator = value;
            Denominator = 1;
        }

        public Rational(Rational number)
        {
            Numerator = number.Numerator;
            Denominator = number.Denominator;
        }

        public Rational(int numerator, int denominator)
        {
            var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        public Rational(BigInteger numerator, BigInteger denominator)
        {
            var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }
        #endregion

        #region Operator Overloads
        public static Rational operator + (Rational a, Rational b) =>
            new Rational(
                a.Numerator * b.Denominator + b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);

        public static Rational operator - (Rational a, Rational b) =>
            new Rational(
                a.Numerator * b.Denominator - b.Numerator * a.Denominator,
                a.Denominator * b.Denominator);

        public static Rational operator * (Rational a, Rational b) =>
            new Rational(
                a.Numerator * b.Numerator,
                a.Denominator * b.Denominator);

        public static Rational operator / (Rational a, Rational b) =>
            new Rational(
                a.Numerator * b.Denominator,
                a.Denominator * b.Numerator);
        #endregion

    }
}