using ExpandableFactory.IoC;

namespace EndPointProject
{
    public class ReadPropertyValueDependencyStrategy : IResolveDependencyStrategy
    {
        public T Resolve<T>(params object[] args)
        {
            return (T)((PropertyValueObject)args[0]).GetPropertyValue((string)args[1]);
        }
    }
}
