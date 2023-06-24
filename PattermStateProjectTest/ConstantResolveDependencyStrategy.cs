using ExpandableFactory.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattermStateProjectTest
{
    internal class ConstantResolveDependencyStrategy: IResolveDependencyStrategy
    {
        private readonly object _constant;

        public ConstantResolveDependencyStrategy(object constant)
        {
            _constant = constant;
        }

        public T Resolve<T>(params object[] args)
        {
            return (T)_constant;
        }
    }
}
