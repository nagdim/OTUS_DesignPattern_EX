using System;
using System.Collections;

namespace Intepretator
{
    public interface IResolveDependencyStrategy
    {
        T Resolve<T>(params object[] args);
    }


    public class RegisterResolveDependencyStrategy : IResolveDependencyStrategy
    {
        public readonly string _name;
        private readonly StrategyContainer _strategyContainer;

        public RegisterResolveDependencyStrategy(StrategyContainer strategyContainer)
        {
            _name = "Ioc.Register";
            _strategyContainer = strategyContainer;
        }

        public T Resolve<T>(params object[] args)
        {
            if (args.Length == 2)
                throw new ArgumentOutOfRangeException();

            _strategyContainer.Register((string)args[0], (IResolveDependencyStrategy)args[1]);
            return default(T);
        }
    }

    public class FunctionResolveDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly Func<object[], object> _creator;

        public FunctionResolveDependencyStrategy(Func<object[], object> creator)
        {
            _creator = creator;
        }

        public T Resolve<T>(params object[] args)
        {
            return (T)_creator(args);
        }
    }

    public class InstanceResolveDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly object _instance;

        public InstanceResolveDependencyStrategy(object instnace)
        {
            _instance = instnace;
        }

        public T Resolve<T>(params object[] args)
        {
            return (T)_instance;
        }
    }

    public class HashTableIntanceDependencyStrategy<TKey> : InstanceResolveDependencyStrategy
    {
        public HashTableIntanceDependencyStrategy() : base(new Hashtable())
        {
        }
    }
}
