namespace GaussElimination
{
    public interface ICalculateable<T>
    {
        T Add(T b);
        T Subtract(T b);
        T Multiply(T b);
        T Divide(T b);

        T ParseFraction(int numerator, int denominator);
        T ParseInt(int a);

        bool GreaterThan(T b);
    }
}