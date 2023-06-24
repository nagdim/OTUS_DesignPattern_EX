using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;

namespace PattermStateProject
{
    public class ContextState
    {
        private readonly Dictionary<Type, ICommandState> _commandOfStates = new Dictionary<Type, ICommandState>();
        private readonly Queue<ICommand> _queueCommands = new Queue<ICommand>();
        private ICommandState _currentState;

        public ContextState(ICommandState defState)
        {
            ChangeState(null, defState);
        }

        public ICommandState Run()
        {
            return _currentState = _currentState?.Next(this);
        }

        public ICommandState ChangeState(ICommand command, ICommandState defState)
        {
            ICommandState value;

            if (command != null && _commandOfStates.TryGetValue(command.GetType(), out value))
                _currentState = value;
            else
                _currentState = defState;

            return _currentState;
        }

        public ICommand AddCommandToQueue(ICommand command)
        {
            _queueCommands.Enqueue(command);
            return command;
        }

        public ICommand GetCommandFromQueue()
        {
            if (_queueCommands.Count == 0)
                return null;
            return _queueCommands.Dequeue();
        }

        public ContextState Register<T>(ICommandState state) where T : ICommand
        {
            var commandType = typeof(T);

            if (commandType.IsAbstract || commandType.IsInterface)
                return this;

            _commandOfStates.Add(commandType, state);
            return this;
        }
    }
}
