using System;

namespace ExpandableFactory.IoC
{
    public class IOC
    {
        private static IContainer s_container;

        static IOC()
        {
            s_container = new Container(new StrategyContainer());
        }

        private IOC()
        {
        }

        public static T Resolve<T>(string key, params object[] args)
        {
            return s_container.Resolve<T>(key, args);
        }

        public static void Register(string key, Func<object[], object> func)
        {
            Register(key, new ResolveInstanceIocStrategy(func));
        }

        public static void Register(string key, IResolveDependencyStrategy strategy)
        {
            s_container.Register(key, strategy);
        }
    }
}
