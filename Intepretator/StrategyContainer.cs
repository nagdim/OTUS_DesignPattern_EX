using System.Collections.Generic;

namespace Intepretator
{
    public class StrategyContainer : IStrategyContainer
    {
        private readonly Dictionary<string, IResolveDependencyStrategy> m_strategyStorage = new Dictionary<string, IResolveDependencyStrategy>();

        public StrategyContainer()
        {
            Register("Ioc.Register", new RegisterResolveDependencyStrategy(this));
        }

        public IResolveDependencyStrategy Resolve(string key)
        {
            return m_strategyStorage[key];
        }

        public void Register(string key, IResolveDependencyStrategy strategy)
        {
            m_strategyStorage[key] = strategy;
        }
    }
}
