using ExpandableFactory.IoC;

namespace EndPointProject
{
    public class FindObjectInStorageDependencyStrategy : IResolveDependencyStrategy
    {
        public T Resolve<T>(params object[] args)
        {
            return IOC.Resolve<T>($"storage.{NameCreator.Create<T>(IOC.Resolve<object>("read_property_value", args[0], NameCreator.Create<T>(null)))}", args);
        }
    }
}
