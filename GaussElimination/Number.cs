using System;

namespace GaussElimination
{
    public class Number<T> where T : ICalculateable<T>, new()
    {
        public readonly T Value;

        public Number(T number)
        {
            Value = number;
        }

        public Number(int number)
        {
            Value = new T().ParseInt(number);
        }

        public Number(int numerator, int denominator)
        {
            Value = new T().ParseFraction(numerator, denominator);
        }

        public Number()
        {
            Value = typeof(T).IsPrimitive
                ? default(T)
                : Value = new T();
        }

        public static Number<T> operator +(Number<T> i, Number<T> j) =>
            new Number<T>(i.Value.Add(j.Value));

        public static Number<T> operator -(Number<T> i, Number<T> j) =>
            new Number<T>(i.Value.Subtract(j.Value));

        public static Number<T> operator *(Number<T> i, Number<T> j) =>
            new Number<T>(i.Value.Multiply(j.Value));

        public static Number<T> operator /(Number<T> i, Number<T> j) =>
            new Number<T>(i.Value.Divide(j.Value));

        public static bool operator >(Number<T> i, Number<T> j) =>
            i.Value.GreaterThan(j.Value);

        public static bool operator <(Number<T> i, Number<T> j) =>
            j.Value.GreaterThan(i.Value);

        public override string ToString() => Value.ToString();

        public Number<T> Negate() => this * new Number<T>(-1);

    }
}