using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using System;

namespace SpaceShipProject
{
    public class MacroCommand : ICommand
    {
        private readonly ICommand[] m_commands;

        public MacroCommand(ICommand[] commands)
        {
            m_commands = commands ?? throw new ArgumentNullException();
        }

        public void Execute()
        {
            try
            {
                foreach (var command in m_commands)
                    command.Execute();
            }
            catch (Exception ex)
            {
                throw new CommandException(ex.ToString());
            }
        }
    }
}
