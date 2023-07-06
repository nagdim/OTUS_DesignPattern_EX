using ExpandableFactory.IoC;
using ExpandableFactory.IoC.Exceptions;
using SpaceShipProject.Contracts.Commands;
using System;

namespace EndPointProject
{
    public class IntepretDependencyStrategy : IResolveDependencyStrategy
    {
        public T Resolve<T>(params object[] args)
        {
            var property = IOC.Resolve<IResolveDependencyStrategy>($"find_object_in_storage");

            return (T)
                (
                    ThrowWhenNotFound<ICommand>(property, args[0],
                            ThrowWhenNotFound<Game>(property, args[0]),
                            ThrowWhenNotFound<UObject>(property, args[0]),
                            IOC.Resolve<object>("read_property_value", args[0], "param")
                        )
                ); ;
        }

        private T ThrowWhenNotFound<T>(IResolveDependencyStrategy strategy, params object[] args) where T : class
        {
            return strategy.Resolve<T>(args) ?? throw new NotFoundValueByPropertyException(NameCreator.Create<T>(null));
        }
    }
}
