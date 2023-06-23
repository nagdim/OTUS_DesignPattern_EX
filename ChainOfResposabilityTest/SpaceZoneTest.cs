
using ChainOfResposability;
using ChainOfResposability.Interface;
using Moq;
using SpaceShipProject.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChainOfResposabilityTest
{
    public class SpaceZoneTest
    {

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 9)]
        [InlineData(9, 1)]
        [InlineData(9, 9)]
        public void AddZoneObjectToZone_X0_Y0_Test(int x, int y)
        {
            SpaceZone spaceZone = new SpaceZone(10);

            var znObject = spaceZone.AddObject(new Vector(x, y));

            Assert.True(znObject.Sector.X == 0);
            Assert.True(znObject.Sector.Y == 0);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(10, 19)]
        [InlineData(19, 10)]
        [InlineData(19, 19)]
        public void AddZoneObjectToNotZone_X0_Y0_Test(int x, int y)
        {
            SpaceZone spaceZone = new SpaceZone(10);

            var znObject = spaceZone.AddObject(new Vector(x, y));

            Assert.False(znObject.Sector.X == 0);
            Assert.False(znObject.Sector.Y == 0);
        }

        [Fact]
        public void AddZoneObjectInTheSameSingleZone_Test()
        {
            SpaceZone spaceZone = new SpaceZone(10);

            ISectorOfZone storezone = null;
            foreach (var point in Enumerable.Range(0, 4).Select(s => new Vector(0, 2 * s)))
            {
                var zone = spaceZone.AddObject(point);

                Assert.True(zone != null);
                Assert.True(zone.Sector != null);

                if (storezone != null)
                    Assert.True(storezone == zone.Sector);

                storezone = zone.Sector;
            }
        }

        [Theory]
        [InlineData(4, 9, 9)]
        [InlineData(1, 1, 1)]
        [InlineData(2, 1, 9)]
        [InlineData(2, 9, 1)]
        [InlineData(4, 5, 5)]
        public void TryGetNotnullUniqueZonesArrountPoint_Test(int size, int x, int y)
        {
            var spaceZone = new SpaceZone(10);
            var storezone = new SectorOfZone[size];

            int enumerateCount = 0;
            foreach (SectorOfZone zone in spaceZone.EnumerateSectorsByPoint(new Vector(x, y)))
            {
                enumerateCount++;

                Assert.NotNull(zone);
                storezone[(zone.X * size / 2) + zone.Y] = zone;
            }

            Assert.True(enumerateCount == size);

            foreach (var zone in storezone)
                Assert.NotNull(zone);
        }

        [Fact]
        public void TryMovePointNearlyPoint_ThrowCollisionObjectException_Test()
        {
            var spaceZone = CreateSpaceZone(new Vector(5, 5), new Vector(15, 5));

            Assert.Throws<CollisionObjectException>(() =>
            {
                new CollisionCommand(spaceZone, new Vector(5, 5), new Vector(5, 6)).Execute();
            });

            Assert.Throws<CollisionObjectException>(() =>
            {
                new CollisionCommand(spaceZone, new Vector(5, 5), new Vector(15, 6)).Execute();
            });
        }

        [Fact]
        public void TryMovePointInTheSameSector_SuccessMove_Test()
        {
            var before = new Vector(5, 5);
            var after = new Vector(5, 7);

            var spaceZone = CreateSpaceZone(before);
            var beforeObject = spaceZone.FindObject(before);

            new CollisionCommand(spaceZone, before, after).Execute();

            var afterObject = spaceZone.FindObject(after);

            Assert.Null(spaceZone.FindObject(before));
            Assert.NotNull(beforeObject);
            Assert.NotNull(afterObject);
            Assert.True(beforeObject.Sector == afterObject.Sector);
        }

        [Fact]
        public void TryMovePointInTheAnotherSector_SuccessMove_Test()
        {
            var before = new Vector(5, 5);
            var after = new Vector(15, 7);

            var spaceZone = CreateSpaceZone(before);

            var beforeObject = spaceZone.FindObject(before);

            new CollisionCommand(spaceZone, before, after).Execute();

            var afterObject = spaceZone.FindObject(after);

            Assert.Null(spaceZone.FindObject(before));
            Assert.NotNull(beforeObject);
            Assert.NotNull(afterObject);
            Assert.True(beforeObject.Sector != afterObject.Sector);
        }


        private static SpaceZone CreateSpaceZone(params Vector[] points)
        {
            SpaceZone spaceZone = new SpaceZone(10);

            foreach (var point in points)
                spaceZone.AddObject(point);

            return spaceZone;
        }
    }
}
