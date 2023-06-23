using ChainOfResposability.Interface;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Vector = SpaceShipProject.Contracts.Common.Vector;

namespace ChainOfResposability
{
    public class CollisionCommand : ICommand
    {
        private readonly SpaceZone _spaceZone;
        private readonly Vector _pointFrom;
        private readonly Vector _pointTo;

        public CollisionCommand(SpaceZone spaceZone, Vector pointFrom, Vector pointTo)
        {
            _spaceZone = spaceZone;

            _pointFrom = pointFrom;
            _pointTo = pointTo;
        }

        public void Execute()
        {
            var objectFrom = _spaceZone.FindObject(_pointFrom);

            if (objectFrom == null)
                throw new ArgumentException($"Not found object point X:{_pointFrom.X} Y:{_pointFrom.Y}.");

            var sector = FindTargetSector(objectFrom.Sector);

            var success = objectFrom.Sector.RemoveObject(objectFrom);
            if (success)
                sector.AddObject(_pointTo);
        }

        private ISectorOfZone FindTargetSector(ISectorOfZone sector)
        {
            ISectorOfZone targetSector = _spaceZone.FindSectorByGeoCoordinate(_pointTo.X, _pointTo.Y);

            if (targetSector != null)
            {
                Validate(targetSector);

                if (sector != targetSector)
                {
                    foreach (var zone in _spaceZone.EnumerateSectorsByPoint(_pointTo))
                        Validate(zone);
                }
            }

            return targetSector;
        }

        private void Validate(ISectorOfZone sector)
        {
            var obj = sector.GetObject(_pointTo);

            if (obj != null)
                throw new CollisionObjectException($"Point X:{_pointTo.X} Y:{_pointTo.Y} cross to point X:{obj.Coordinate.X} Y:{obj.Coordinate.Y}.");
        }
    }
}
