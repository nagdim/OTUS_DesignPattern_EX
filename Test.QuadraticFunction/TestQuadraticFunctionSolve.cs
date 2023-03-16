using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EX1_QuadraticFunction;
using Xunit;

namespace TestQuadraticFunction
{
    public class TestQuadraticFunctionSolve
    {

        /// <summary>
        /// Написать тест, который проверяет, что для уравнения x^2+1 = 0 корней нет (возвращается пустой массив)
        /// </summary>
        [Fact]
        public void Solve_NoRootOfTheEquation()
        {
            double[] res = QuadraticFunction.Solve(1.0, 0.0, 1.0);

            Assert.Empty(res);
        }
    }
}
