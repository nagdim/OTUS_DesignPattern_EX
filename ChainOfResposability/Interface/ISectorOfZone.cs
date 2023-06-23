using SpaceShipProject.Contracts.Common;

namespace ChainOfResposability.Interface
{
    public interface ISectorOfZone
    {
        int X { get; }
        int Y { get; }
        SpaceZone SpaceZone { get; }
        IZoneObject AddObject(Vector point);
        IZoneObject GetObject(Vector point);
        bool RemoveObject(IZoneObject zoneObject);
        void Verify(Vector point);
    }
}