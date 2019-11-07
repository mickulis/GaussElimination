namespace GaussElimination
{
    public class DoubleBox : ICalculateable<DoubleBox>
    {
        private readonly double _value;

        public DoubleBox()
        {
            _value = 0;
        }

        public DoubleBox(double a)
        {
            _value = a;
        }

        public DoubleBox Add(DoubleBox b) => new DoubleBox(this._value + b._value);


        public DoubleBox Subtract(DoubleBox b) => new DoubleBox(this._value - b._value);


        public DoubleBox Multiply(DoubleBox b) => new DoubleBox(this._value * b._value);


        public DoubleBox Divide(DoubleBox b) => new DoubleBox(this._value / b._value);


        public DoubleBox ParseFraction(int numerator, int denominator) => new DoubleBox(1.0 * numerator / denominator);

        public DoubleBox ParseInt(int a) => new DoubleBox(1.0 * a);

        public bool GreaterThan(DoubleBox b) => _value > b._value;

        public override string ToString() => _value.ToString();

    }
}