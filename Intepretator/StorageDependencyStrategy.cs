using System.Collections;

namespace Intepretator
{
    public class StorageDependencyStrategy : IResolveDependencyStrategy
    {
        private readonly Hashtable _hashtable = new Hashtable();
        private readonly IResolveDependencyStrategy _argKeyFunc;

        public StorageDependencyStrategy(IResolveDependencyStrategy argKeyFunc)
        {
            this._argKeyFunc = argKeyFunc;
        }

        public T Resolve<T>(params object[] args)
        {
            var key = _argKeyFunc.Resolve<object>((args));

            if (_hashtable.ContainsKey(key))
                return ((IResolveDependencyStrategy)_hashtable[key]).Resolve<T>(args);

            return default(T);
        }

        public StorageDependencyStrategy Add(object key, IResolveDependencyStrategy strategy)
        {
            _hashtable.Add(key, strategy);
            return this;
        }
    }
}
