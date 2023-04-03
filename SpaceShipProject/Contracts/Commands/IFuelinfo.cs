namespace SpaceShipProject.Contracts.Commands
{
    public interface IFuelinfo
    {
        int CurrentLevel { get; set; }

        int Consumption { get;  }
    }
}
