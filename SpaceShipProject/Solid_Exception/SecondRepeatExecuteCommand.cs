using SpaceShipProject.Contracts.Commands;

namespace SpaceShipProject.Solid_Exception
{
    public class SecondRepeatExecuteCommand : ICommand
    {
        private readonly ICommand m_command;

        public SecondRepeatExecuteCommand(ICommand command)
        {
            m_command = command;
        }

        public void Execute()
        {
            m_command.Execute();
        }
    }
}
