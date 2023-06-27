namespace Intepretator
{
    public interface IStrategyContainer
    {
        IResolveDependencyStrategy Resolve(string key);
        void Register(string key, IResolveDependencyStrategy strategy);
    }
}
