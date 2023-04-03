using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xunit;

namespace TestSpaceShipProject
{
    public class CheckFuelCommandTest
    {
        [Fact]
        public void TryCheck_FuelEmpty_ThrowFuelEmptyException()
        {
            Mock<IFuelinfo> mockObject = new Mock<IFuelinfo>();

            mockObject
                .SetupGet((s) => s.CurrentLevel)
                .Returns(0);

            Assert.Throws<FuelEmptyException>(new CheckFuelCommand(mockObject.Object).Execute);
        }

        [Fact]
        public void TryCheck_FuelNotEmpty_NotThrowFuelEmptyException()
        {
            Mock<IFuelinfo> mockObject = new Mock<IFuelinfo>();

            mockObject
                .SetupGet((s) => s.CurrentLevel)
                .Returns(10);

            try
            {
                new CheckFuelCommand(mockObject.Object).Execute();
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

    }
}
