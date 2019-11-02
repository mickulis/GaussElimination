namespace GaussElimination
{
    public class Number<T>
    {
        public readonly T Value;
        public Number(T number)
        {
            Value = number;
        }


        public static Number<T> operator + (Number<T> i, Number<T> j) => new Number<T>((dynamic) i.Value + (dynamic) j.Value);
        public static Number<T> operator - (Number<T> i, Number<T> j) => new Number<T>((dynamic) i.Value - (dynamic) j.Value);
        public static Number<T> operator * (Number<T> i, Number<T> j) => new Number<T>((dynamic) i.Value * (dynamic) j.Value);
        public static Number<T> operator / (Number<T> i, Number<T> j) => new Number<T>((dynamic) i.Value / (dynamic) j.Value);



//        public static Number<T> operator + (Number<T> i, Number<T> j) => new Number<T>(Sum(i.Value, j.Value));
//        private static T Sum(T a, T b) => (dynamic) a + (dynamic) b;
//        private static T Sum(T a, T b) => (dynamic) a + (dynamic) b;
//        private static T Sum(T a, T b) => (dynamic) a + (dynamic) b;
//        private static T Sum(T a, T b) => (dynamic) a + (dynamic) b;

    }
}