using SpaceShipProject.Contracts.Commands;
using System.Collections.Generic;

namespace SpaceShipProject
{
    public class ExecuteCommandAndAddToQueue : ICommand
    {
        private readonly ICommand m_command;
        private readonly ICommand m_commandEnqueue;

        public ExecuteCommandAndAddToQueue(ICommand command, Queue<ICommand> commands)
        {
            m_command = command;
            m_commandEnqueue = new AddCommandToQueue(commands, this);
        }

        public void Execute()
        {
            m_command.Execute();
            m_commandEnqueue.Execute();
        }
    }
}
