namespace Intepretator
{
    public interface IContainer
    {
        T Resolve<T>(string key, params object[] args);

        void Register(string key, IResolveDependencyStrategy strategy);
    }
}
