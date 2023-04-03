using SpaceShipProject.Contracts.Commands;

namespace SpaceShipProject
{
    public class RotateThanChangeVelocityCommand : ICommand
    {
        private readonly IMovable m_movable;
        private readonly IRotable m_rotable;

        public RotateThanChangeVelocityCommand(IMovable movable, IRotable rotable)
        {
            m_movable = movable;
            m_rotable = rotable;
        }

        public void Execute()
        {
            new MacroCommand(new ICommand[] { new RotateCommand(m_rotable), new ChangeVelocityComamnd(m_movable, m_rotable) }).Execute();
        }
    }
}
