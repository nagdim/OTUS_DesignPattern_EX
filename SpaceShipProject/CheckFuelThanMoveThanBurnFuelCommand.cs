using SpaceShipProject.Contracts.Commands;

namespace SpaceShipProject
{
    public class CheckFuelThanMoveThanBurnFuelCommand : ICommand
    {
        private readonly IMovable m_movable;
        private readonly IFuelinfo m_fuelinfo;
        private readonly IRotable m_rotable;

        public CheckFuelThanMoveThanBurnFuelCommand(IMovable movable, IFuelinfo fuelinfo)
        {
            m_movable = movable;
            m_fuelinfo = fuelinfo;
        }

        public void Execute()
        {
            new MacroCommand(new ICommand[] { new CheckFuelCommand(m_fuelinfo), new MoveCommand(m_movable), new BurnFuelCommand(m_fuelinfo) }).Execute();
        }
    }
}
