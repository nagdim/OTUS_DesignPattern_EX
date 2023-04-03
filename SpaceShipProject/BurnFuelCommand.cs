using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;

namespace SpaceShipProject
{
    public class BurnFuelCommand : ICommand
    {
        private readonly IFuelinfo m_fuelinfo;

        public BurnFuelCommand(IFuelinfo fuelinfo)
        {
            m_fuelinfo = fuelinfo;

        }

        public void Execute()
        {
            if (m_fuelinfo.CurrentLevel <= 0)
                throw new FuelEmptyException("The fuel level cannot change because empty.");

            if (m_fuelinfo.CurrentLevel - m_fuelinfo.Consumption < 0)
                throw new FuelEmptyException("The fuel level cannot change because consumption greater than current.");

            m_fuelinfo.CurrentLevel -= m_fuelinfo.Consumption;
        }
    }
}
