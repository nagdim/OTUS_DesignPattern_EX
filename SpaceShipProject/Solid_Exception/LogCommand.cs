using SpaceShipProject.Contracts;
using SpaceShipProject.Contracts.Commands;

namespace SpaceShipProject.Solid_Exception
{
    public class LogCommand : ICommand
    {
        private readonly ILogger m_logger;
        private readonly string m_message;

        public LogCommand(ILogger logger, string message)
        {
            m_logger = logger;
            m_message = message;
        }

        public void Execute()
        {
            m_logger.Log(m_message);
        }
    }
}
