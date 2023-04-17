using System;

namespace ExpandableFactory.IoC
{
    public class ResolveInstanceIocStrategy : IResolveDependencyStrategy
    {
        private readonly Func<object[], object> m_func;

        public ResolveInstanceIocStrategy(Func<object[], object> func)
        {
            m_func = func;
        }

        public T Resolve<T>(params object[] args)
        {
            return (T)m_func(args);
        }
    }
}
