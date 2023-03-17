using EX1_QuadraticFunction;
using System;
using Xunit;

namespace TestQuadraticFunction
{

    public class TestQuadraticFunctionSolve
    {

        /// <summary>
        /// Написать тест, который проверяет, что для уравнения x^2+1 = 0 корней нет (возвращается пустой массив)
        /// </summary>
        [Fact]
        public void Solve_1_NoRoot()
        {
            double[] res = QuadraticFunction.Solve(1.0, 0.0, 1.0);

            Assert.Empty(res);
        }

        /// <summary>
        /// Написать тест, который проверяет, что для уравнения x^2-1 = 0 есть два корня кратности 1 (x1=1, x2=-1)
        /// </summary>
        [Fact]
        public void Solve_2_TwoRootAnd()
        {
            double[] res = QuadraticFunction.Solve(1.0, 0.0, -1.0);

            Assert.Equal(2, res.Length);
            Assert.Equal(res, new double[] { 1.0, -1.0 });
        }

        /// <summary>
        /// Написать тест, который проверяет, что для уравнения x^2+2x+1 = 0 есть один корень кратности 2 (x1= x2 = -1).
        /// </summary>
        [Fact]
        public void Solve_3_TwoSameRoot()
        {
            double[] res = QuadraticFunction.Solve(1.0, 2.0, 1.0);

            Assert.Equal(2, res.Length);
            Assert.Equal(res, new double[] { -1.0, -1.0 });
        }

        /// <summary>
        /// Написать тест, который проверяет, что коэффициент a не может быть равен 0. В этом случае solve выбрасывает исключение.
        /// Примечание.Учесть, что a имеет тип double и сравнивать с 0 через == нельзя
        ///// </summary>
        [Fact]
        public void Solve_4_FirstArgumentEqualZeroOrTooSmall()
        {
            Assert.Throws<ArgumentException>(() => QuadraticFunction.Solve(0.0, 2.0, 1.0));
            Assert.Throws<ArgumentException>(() => QuadraticFunction.Solve(QuadraticFunction.MinDoubleLimit, 2.0, 1.0));
        }

        /// <summary>
        /// С учетом того, что дискриминант тоже нельзя сравнивать с 0 через знак равенства, 
        /// подобрать такие коэффициенты квадратного уравнения для случая одного корня кратности два, чтобы дискриминант был отличный от нуля, но меньше заданного эпсилон. 
        /// </summary>
        [Fact]
        public void Solve_5_DiscriminantLessThanMinDoubleLimit()
        {
            double[] res = QuadraticFunction.Solve(1.0, 2.0 + QuadraticFunction.MinDoubleLimit, 1.0 + QuadraticFunction.MinDoubleLimit);

            Assert.Equal(2, res.Length);
            Assert.Equal(res, new double[] { -1.0, -1.0 });
        }

        /// <summary>
        /// Посмотреть какие еще значения могут принимать числа типа double, кроме числовых и написать тест с их использованием на все коэффициенты. 
        /// solve должен выбрасывать исключение.
        /// </summary>
        [Fact]
        public void Solve_5_UndefinedArguments()
        {
            foreach (var value in new double[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity, double.MaxValue })
            {
                Assert.Throws<NotSupportedException>(() => QuadraticFunction.Solve(value, 1, 1));
                Assert.Throws<NotSupportedException>(() => QuadraticFunction.Solve(1, value, 1));
                Assert.Throws<NotSupportedException>(() => QuadraticFunction.Solve(1, 1, value));
            }
        }
    }
}
