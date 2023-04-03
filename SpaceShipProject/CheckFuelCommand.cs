using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;

namespace SpaceShipProject
{
    public class CheckFuelCommand : ICommand
    {
        private readonly IFuelinfo m_fuelinfo;

        public CheckFuelCommand(IFuelinfo fuelinfo)
        {
            m_fuelinfo = fuelinfo;
        }

        public void Execute()
        {
            if (m_fuelinfo.CurrentLevel <= 0)
                throw new FuelEmptyException("The fuel level is empty.");
        }
    }
}
