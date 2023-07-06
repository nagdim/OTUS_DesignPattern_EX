using ExpandableFactory.IoC;
using System;

namespace EndPointProject
{
    public class StorageInstanceDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly Func<object, IResolveDependencyStrategy> _creator;

        private StorageInstanceDependencyStrategy(Func<object, IResolveDependencyStrategy> creator)
        {
            _creator = creator;
        }

        public static IResolveDependencyStrategy Instace()
        {
            return new StorageInstanceDependencyStrategy(arg => new InstanceDependencyStrategy(arg));
        }

        public static IResolveDependencyStrategy Func()
        {
            return new StorageInstanceDependencyStrategy(arg => new ResolveInstanceIocStrategy((Func<object[], object>)arg));
        }

        public T Resolve<T>(params object[] args)
        {
            IOC.Register($"storage.{NameCreator.Create<T>(args[0])}", _creator(args[1]));
            return default(T);
        }
    }
}
