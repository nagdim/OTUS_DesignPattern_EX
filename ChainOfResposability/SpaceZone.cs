using ChainOfResposability.Interface;
using SpaceShipProject.Contracts.Common;
using System.Collections.Generic;

namespace ChainOfResposability
{
    public class SpaceZone : ISpaceZone
    {
        private readonly ISectorOfZone[,] _zoneAreas;

        public int SizeZone
        {
            get; private set;
        }

        public SpaceZone(int sizeZone)
        {
            SizeZone = sizeZone;
            _zoneAreas = GenerateEmptySectors();
        }

        private ISectorOfZone[,] GenerateEmptySectors()
        {
            var zoneAreas = new SectorOfZone[SizeZone, SizeZone];

            for (int x = 0; x < SizeZone; x++)
            {
                for (int y = 0; y < SizeZone; y++)
                    zoneAreas[x, y] = new SectorOfZone(x, y, this);
            }

            return zoneAreas;
        }

        public ISectorOfZone FindSectorByGeoCoordinate(int geoX, int geoY)
        {
            int mapX = (int)(geoX / SizeZone);
            int mapY = (int)(geoY / SizeZone);

            return FindSectorByMapCoordinate(mapX, mapY);
        }

        private ISectorOfZone FindSectorByMapCoordinate(int mapX, int mapY)
        {
            if (mapX >= 0 && mapX < SizeZone && mapY >= 0 && mapY < SizeZone)
                return _zoneAreas[mapX, mapY];

            return null;
        }

        public IZoneObject AddObject(Vector point)
        {
            ISectorOfZone zone = FindSectorByGeoCoordinate(point.X, point.Y);

            if (zone != null)
                return zone.AddObject(point);

            return null;
        }

        public IZoneObject FindObject(Vector point)
        {
            ISectorOfZone targetZone = FindSectorByGeoCoordinate(point.X, point.Y);

            if (targetZone != null)
                return targetZone.GetObject(point);

            return null;
        }

        public IEnumerable<ISectorOfZone> EnumerateSectorsByPoint(Vector point)
        {
            var zones = new HashSet<ISectorOfZone>();

            foreach (var zone in EnumerateArroundSectorsByPoint(point, SizeZone / 2))
            {
                if (zone == null)
                    continue;

                zones.Add(zone);
            }

            return zones;
        }

        private IEnumerable<ISectorOfZone> EnumerateArroundSectorsByPoint(Vector point, int sizeZone)
        {
            yield return FindSectorByGeoCoordinate(point.X - sizeZone, point.Y - sizeZone);
            yield return FindSectorByGeoCoordinate(point.X - sizeZone, point.Y + sizeZone);

            yield return FindSectorByGeoCoordinate(point.X, point.Y - sizeZone);
            yield return FindSectorByGeoCoordinate(point.X, point.Y + sizeZone);

            yield return FindSectorByGeoCoordinate(point.X + sizeZone, point.Y - sizeZone);
            yield return FindSectorByGeoCoordinate(point.X + sizeZone, point.Y + sizeZone);

            yield return FindSectorByGeoCoordinate(point.X - sizeZone, point.Y);
            yield return FindSectorByGeoCoordinate(point.X + sizeZone, point.Y);
        }
    }
}
