using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;

namespace SpaceShipProject.Handler
{
    public class ExceptionHandler
    {
        private readonly Dictionary<string, Dictionary<Type, Func<ICommand, Exception, ICommand>>> _handlers = new Dictionary<string, Dictionary<Type, Func<ICommand, Exception, ICommand>>>();
        private readonly IExceptionHandlerStrategy m_handlerStrategy;

        public ExceptionHandler(IExceptionHandlerStrategy handlerStrategy)
        {
            m_handlerStrategy = handlerStrategy;
        }

        public ExceptionHandler Setup<C, E>(Func<ICommand, Exception, ICommand> action)
            where C : ICommand
            where E : Exception
        {
            return Setup<E>(typeof(C), action);
        }

        public ExceptionHandler Setup<E>(Type commandType, Func<ICommand, Exception, ICommand> action) where E : Exception
        {
            var key = commandType.ToString();
            var exceptionType = typeof(E);

            if (_handlers.ContainsKey(key))
                _handlers[key].Add(exceptionType, action);
            else
                _handlers.Add(key, new Dictionary<Type, Func<ICommand, Exception, ICommand>>() { { exceptionType, action } });

            return this;
        }

        public void Handle(ICommand cmd, Exception ex)
        {
            if (cmd != null)
            {
                var key = cmd.GetType().ToString();

                if (_handlers.ContainsKey(key))
                {
                    var cmdExceptionHandler = _handlers[key];
                    var action = cmdExceptionHandler[ex.GetType()];

                    if (action != null)
                    {
                        m_handlerStrategy.Handle(action(cmd, ex), ex);
                        return;
                    }
                }
            }

            throw ex;
        }
    }
}
