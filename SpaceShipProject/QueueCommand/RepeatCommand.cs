using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using System;
using System.Collections.Generic;

namespace SpaceShipProject
{
    public class RepeatCommand : ICommand
    {
        private readonly ICommand[] m_commands;

        public RepeatCommand(ICommand[] commands)
        {
            m_commands = commands;
        }

        public void Execute()
        {
            try
            {
                Queue<ICommand> queueCommands = new Queue<ICommand>();

                foreach (var command in m_commands)
                {
                    queueCommands.Enqueue(new ExecuteCommandAndAddToQueue(command, queueCommands));

                    while (queueCommands.Count > 0)
                        queueCommands.Dequeue().Execute();
                }
            }
            catch (Exception ex)
            {
                throw new CommandException(ex.ToString());
            }
        }
    }
}
