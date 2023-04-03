using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using Xunit;

namespace TestSpaceShipProject
{
    public class BurnFuelCommandTest
    {
        [Fact]
        public void TryBurn_ChangeCurrentLevelFuel_Success()
        {
            Mock<IFuelinfo> mockObject = new Mock<IFuelinfo>();

            mockObject
                .SetupGet(m => m.CurrentLevel)
                .Returns(10);

            mockObject
                .SetupGet(m => m.Consumption)
                .Returns(1);

            new BurnFuelCommand(mockObject.Object).Execute();

            mockObject.VerifySet(m => m.CurrentLevel = 9);
        }

        [Fact]
        public void TryBurn_ChangeCurrentLevelIsZero_TrowFuelemptyExceptioon()
        {
            Mock<IFuelinfo> mockObject = new Mock<IFuelinfo>();

            mockObject
                .SetupGet(m => m.CurrentLevel)
                .Returns(0);

            mockObject
                .SetupGet(m => m.Consumption)
                .Returns(1);

            Assert.Throws<FuelEmptyException>(new BurnFuelCommand(mockObject.Object).Execute);
        }

        [Fact]
        public void TryBurn_DiffBetweenCurrentLevelAndConsumptionLessOrEqualZero_TrowFuelemptyExceptioon()
        {
            Mock<IFuelinfo> mockObject = new Mock<IFuelinfo>();

            mockObject
                .SetupGet(m => m.CurrentLevel)
                .Returns(1);

            mockObject
                .SetupGet(m => m.Consumption)
                .Returns(2);

            Assert.Throws<FuelEmptyException>(new BurnFuelCommand(mockObject.Object).Execute);
        }
    }
}
