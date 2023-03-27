using System;

namespace EX1_QuadraticFunction
{
    public static class QuadraticFunction
    {
        private const int c_delimeter = 5;
        private const double c_epsilon = 1E-6;
        public static double MinDoubleLimit = 0.000001;//1E-16;


        public static double[] Solve(double a, double b, double c)
        {
            ValidateArgument(nameof(a), a);
            ValidateArgument(nameof(b), b);
            ValidateArgument(nameof(c), c);

            if (Math.Abs(a) <= c_epsilon)
                throw new ArgumentException($"Argument {nameof(a)} cannot equal to zero.");

            var d = b * b - 4 * a * c;

            ValidateArgument("discriminant", d);

            if (Math.Abs(d) < c_epsilon)
            {
                var x = RoundValue(- b / (2 * a));
                return new double[] { x, x };
            }

            if (d < c_epsilon)
                return Array.Empty<double>();

            var x1 = (-b + Math.Sqrt(d)) / (2 * a);
            var x2 = (-b - Math.Sqrt(d)) / (2 * a);

            return new double[] { RoundValue(x1), RoundValue(x2) };
        }

        private static double RoundValue(double value)
        {
            return Math.Round(value, c_delimeter);
        }

        private static void ValidateArgument(string argumentName, double value)
        {
            ValidateValueByFunc(double.IsNaN, value, argumentName);
            ValidateValueByFunc(double.IsInfinity, value, argumentName);
            ValidateValueByFunc(double.IsNegativeInfinity, value, argumentName);
            ValidateValueByFunc(double.IsPositiveInfinity, value, argumentName);
        }

        private static void ValidateValueByFunc(Predicate<double> validatefunc, double value, string valueName)
        {
            if (validatefunc(value))
                throw new QuadraticFunctionException($"Value {valueName} cannot equal to {value}");
        }
    }
}
