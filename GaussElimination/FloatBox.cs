namespace GaussElimination
{
    public class FloatBox : ICalculateable<FloatBox>
    {
        private readonly float _value;

        public FloatBox()
        {
            _value = 0;
        }

        public FloatBox(float a)
        {
            _value = a;
        }

        public FloatBox Add(FloatBox b) => new FloatBox(this._value + b._value);


        public FloatBox Subtract(FloatBox b) => new FloatBox(this._value - b._value);


        public FloatBox Multiply(FloatBox b) => new FloatBox(this._value * b._value);


        public FloatBox Divide(FloatBox b) => new FloatBox(this._value / b._value);


        public FloatBox ParseFraction(int numerator, int denominator) => new FloatBox(1.0f * numerator / denominator);


        public FloatBox ParseInt(int a) => new FloatBox(1.0f * a);

        public bool GreaterThan(FloatBox b) => _value > b._value;

        public override string ToString() => _value.ToString();
    }
}