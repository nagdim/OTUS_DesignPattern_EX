using System;
using System.Linq;

namespace EX1_QuadraticFunction
{
    public static class QuadraticFunction
    {
        private const int c_delimeter = 5;
        private static readonly Predicate<double>[] IndicateSpecifiedValueFunctions = new Predicate<double>[] { double.IsNaN, double.IsNegativeInfinity, double.IsPositiveInfinity, double.IsInfinity };

        public static double MinDoubleLimit = 0.000001;//1E-16;


        public static double[] Solve(double a, double b, double c)
        {
            ValidateArgument(nameof(a), a);
            ValidateArgument(nameof(b), b);
            ValidateArgument(nameof(c), c);

            if (RoundLimit(a) == 0.0)
                throw new ArgumentException($"Argument {nameof(a)} cannot equal to zero.");

            var d = RoundLimit(b * b - 4 * a * c);

            ValidateArgument("discriminant", d);

            if (d < 0.0)
                return Array.Empty<double>();

            var sqrt_d = 0.0;

            if (d > 0.0)
                sqrt_d = Math.Sqrt(d);

            var x1 = RoundLimit((-b + sqrt_d) / (2 * a));
            var x2 = RoundLimit((-b - sqrt_d) / (2 * a));

            return new double[] { Math.Round(x1, c_delimeter), Math.Round(x2, c_delimeter) };
        }

        private static double RoundLimit(double value)
        {
            return Math.Abs(value) <= MinDoubleLimit ? 0.0 : value;
        }

        private static void ValidateArgument(string argumentName, double value)
        {
            foreach (var func in IndicateSpecifiedValueFunctions)
            {
                if (func(value))
                    throw new NotSupportedException($"Argument {argumentName} not supporterd to {value}");
            }
        }
    }
}
