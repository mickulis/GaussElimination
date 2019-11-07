using System;
using System.Numerics;
using System.Xml;

namespace GaussElimination
{
    public class Rational : ICalculateable<Rational>
    {
        private readonly BigInteger _numerator;
        private readonly BigInteger _denominator;

        #region Constructors

        public Rational() : this(BigInteger.Zero, BigInteger.One)
        { }

        public Rational(int value) : this(new BigInteger(value), BigInteger.One)
        {}

        public Rational(BigInteger value) : this(value, BigInteger.One)
        {}

        public Rational(Rational number)
        {
            _numerator = number._numerator;
            _denominator = number._denominator;
        }

        public Rational(int numerator, int denominator) : this(new BigInteger(numerator), new BigInteger(denominator))
        {}

        public Rational(BigInteger numerator, BigInteger denominator)
        {
            if(denominator == 0)
                throw new Exception("DENOMINATOR = 0");
            var gcd = BigInteger.GreatestCommonDivisor(numerator, denominator);
            var sign = 1;
            if (denominator < 0)
                sign = -1;
            _numerator = sign * numerator / gcd;
            _denominator = sign * denominator / gcd;
        }
        #endregion

        #region Operator Overloads

        public static Rational operator +(Rational a, Rational b) => a.Add(b);

        public static Rational operator -(Rational a, Rational b) => a.Subtract(b);

        public static Rational operator *(Rational a, Rational b) => a.Multiply(b);

        public static Rational operator /(Rational a, Rational b) => a.Divide(b);
        #endregion

        #region Mathematic Operation Methods
        public Rational Add(Rational b) =>
            new Rational(
                this._numerator * b._denominator + b._numerator * this._denominator,
                this._denominator * b._denominator);

        public Rational Subtract(Rational b) =>
            new Rational(
            this._numerator * b._denominator - b._numerator * this._denominator,
            this._denominator * b._denominator);


        public Rational Multiply(Rational b) =>
            new Rational(
                this._numerator * b._numerator,
                this._denominator * b._denominator);

        public Rational Divide(Rational b) =>
            new Rational(
                this._numerator * b._denominator,
                this._denominator * b._numerator);
        #endregion

        #region Parsers

        public Rational ParseFraction(int numerator, int denominator) => new Rational(numerator, denominator);
        public Rational ParseInt(int a) => new Rational(a);

        #endregion

        public override string ToString() =>
            _denominator == 1
                ? _numerator.ToString()
                : $"{_numerator.ToString()}/{_denominator.ToString()}";

        protected bool Equals(Rational other)
        {
            return _numerator.Equals(other._numerator) && _denominator.Equals(other._denominator);
        }

        public bool GreaterThan(Rational b) => this._numerator * b._denominator > b._numerator * this._denominator;

        protected bool Equals(long other) =>
            _denominator == 1
                ? _numerator.Equals(other)
                : false;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rational) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_numerator.GetHashCode() * 397) ^ _denominator.GetHashCode();
            }
        }
    }
}