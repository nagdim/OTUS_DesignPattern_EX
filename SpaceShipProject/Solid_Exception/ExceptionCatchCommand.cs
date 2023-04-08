using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Handler;
using System;

namespace SpaceShipProject.Solid_Exception
{
    public class ExceptionCatchCommand : ICommand
    {
        private readonly ICommand m_command;
        private readonly ExceptionHandler m_exceptionHandler;

        public ExceptionCatchCommand(ICommand command, ExceptionHandler exceptionHandler)
        {
            m_command = command;
            m_exceptionHandler = exceptionHandler;
        }

        public void Execute()
        {
            try
            {
                m_command.Execute();
            }
            catch (Exception ex)
            {
                m_exceptionHandler.Handle(m_command, ex);
            }
        }
    }
}
