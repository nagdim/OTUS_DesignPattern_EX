using SpaceShipProject.Contracts.Common;

namespace ChainOfResposability.Interface
{
    public interface IZoneObject
    {
        Vector Coordinate { get; }
        ISectorOfZone Sector { get; set; }

        bool Verify(Vector point);
    }
}