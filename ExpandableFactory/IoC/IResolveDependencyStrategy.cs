namespace ExpandableFactory.IoC
{
    public interface IResolveDependencyStrategy
    {
        T Resolve<T>(params object[] args);
    }
}
