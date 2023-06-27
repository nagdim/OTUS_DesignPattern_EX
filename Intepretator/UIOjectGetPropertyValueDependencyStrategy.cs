namespace Intepretator
{
    public class UIOjectGetPropertyValueDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly string _propertyName;

        public UIOjectGetPropertyValueDependencyStrategy(string propertyName)
        {
            _propertyName = propertyName;
        }

        public T Resolve<T>(params object[] args)
        {
            return (T)((UIObject)args[0]).GetPropertyValue(_propertyName);
        }
    }
}
