using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;

namespace SpaceShipProject
{
    public interface IExceptionHandlerStrategy
    {
        void Handle(ICommand command, Exception ex);
    }

    public class SampleExceptionHandlerStrategy : IExceptionHandlerStrategy
    {
        private readonly Queue<ICommand> m_queeCommands;

        public SampleExceptionHandlerStrategy(Queue<ICommand> commands)
        {
            m_queeCommands = commands;
        }

        public void Handle(ICommand command, Exception ex)
        {
            if (command != null)
                m_queeCommands.Enqueue(command);
        }
    }
}
