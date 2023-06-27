using System;

namespace Intepretator
{
    public class Container : IContainer
    {
        private readonly IStrategyContainer m_strategyContainer;

        public Container(IStrategyContainer strategyContainer)
        {
            m_strategyContainer = strategyContainer;
        }

        public T Resolve<T>(string key, params object[] args)
        {
            try
            {
                var strategy = m_strategyContainer.Resolve(key);
                return strategy.Resolve<T>(args);
            }
            catch (Exception ex)
            {
                throw;// new ResolveContainerException("Resolve container exception", ex);
            }
        }


        public void Register(string key, IResolveDependencyStrategy strategy)
        {
            try
            {
                m_strategyContainer.Register(key, strategy);
            }
            catch (Exception ex)
            {
                throw;// new RegisterContainerException("Register container exception", ex);
            }
        }
    }
}
