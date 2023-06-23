using SpaceShipProject.Contracts.Common;
using System.Collections.Generic;

namespace ChainOfResposability.Interface
{
    public interface ISpaceZone
    {
        int SizeZone { get; }

        IZoneObject AddObject(Vector point);
        IEnumerable<ISectorOfZone> EnumerateSectorsByPoint(Vector point);
        IZoneObject FindObject(Vector point);
        ISectorOfZone FindSectorByGeoCoordinate(int geoX, int geoY);
    }
}