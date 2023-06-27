using System.ComponentModel;
using System;

namespace Intepretator
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

        public static void Register(string key, IResolveDependencyStrategy strategy)
        {
            s_container.Register(key, strategy);
        }

    }
}
