using ExpandableFactory.IoC;

namespace EndPointProject
{
    public class InstanceDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly object _instance;

        public InstanceDependencyStrategy(object instance)
        {
            _instance = instance;
        }
        public T Resolve<T>(params object[] args)
        {
            return (T)_instance;
        }
    }
}
