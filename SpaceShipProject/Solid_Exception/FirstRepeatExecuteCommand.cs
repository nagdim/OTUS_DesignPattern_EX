using SpaceShipProject.Contracts.Commands;

namespace SpaceShipProject.Solid_Exception
{
    public class FirstRepeatExecuteCommand : ICommand
    {
        private readonly ICommand m_command;

        public FirstRepeatExecuteCommand(ICommand command)
        {
            m_command = command;
        }

        public void Execute()
        {
            m_command.Execute();
        }
    }
}
