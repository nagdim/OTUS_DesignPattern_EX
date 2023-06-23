using ChainOfResposability.Interface;
using SpaceShipProject.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ChainOfResposability
{
    public class SectorOfZone : ISectorOfZone
    {
        private readonly List<IZoneObject> _objects;

        public int X { get; private set; }
        public int Y { get; private set; }
        public SpaceZone SpaceZone { get; private set; }

        public SectorOfZone(int x, int y, SpaceZone spaceZone)
        {
            X = x;
            Y = y;

            SpaceZone = spaceZone;
            _objects = new List<IZoneObject>();
        }

        public void Verify(Vector point)
        {
            foreach (var zn in _objects)
            {
                if (zn.Verify(point))
                    throw new CollisionObjectException($"Point X:{point.X} Y:{point.Y} cross to point X:{zn.Coordinate.X} Y:{zn.Coordinate.Y}.");
            }
        }

        public IZoneObject GetObject(Vector point)
        {
            return _objects.FirstOrDefault(s => s.Verify(point));
        }

        public IZoneObject AddObject(Vector point)
        {
            if (VerifyCoordinate(point))
            {
                var zoneObject = new ZoneObject(point, 1.99) { Sector = this };

                _objects.Add(zoneObject);
                return zoneObject;
            }

            return null;
        }

        private bool VerifyCoordinate(Vector point)
        {
            return (Verify(point.X, X) && Verify(point.Y, Y));
        }

        public bool RemoveObject(IZoneObject zoneObject)
        {
            if (zoneObject.Sector != this)
                return false;

            _objects.Remove(zoneObject);
            return true;
        }

        private bool Verify(double currentValue, int minValue = 0)
        {
            if (currentValue >= minValue * SpaceZone.SizeZone && currentValue < minValue * SpaceZone.SizeZone + SpaceZone.SizeZone)
                return true;

            return false;
        }
    }
}
