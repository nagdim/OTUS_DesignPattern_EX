using SpaceShipProject.Contracts.Commands;
using System.Collections.Generic;

namespace SpaceShipProject
{
    public class AddCommandToQueue : ICommand
    {
        private readonly Queue<ICommand> m_commands;
        private readonly ICommand m_commandToAdd;

        public AddCommandToQueue(Queue<ICommand> commands, ICommand commandToAdd)
        {
            m_commands = commands;
            m_commandToAdd = commandToAdd;
        }

        public void Execute()
        {
            m_commands.Enqueue(m_commandToAdd);
        }
    }
}
