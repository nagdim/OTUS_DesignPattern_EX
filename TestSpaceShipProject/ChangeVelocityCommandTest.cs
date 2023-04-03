using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Contracts.Common;
using SpaceShipProject.Exceptions;
using Xunit;

namespace TestSpaceShipProject
{
    public class ChangeVelocityCommandTest
    {

        [Fact]
        public void TryChange_ChangeVelocity_Success()
        {
            Mock<IMovable> mockMovalble = new Mock<IMovable>();
            Mock<IRotable> mockRoutable = new Mock<IRotable>();

            mockMovalble.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovalble.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockRoutable.SetupGet(r => r.Direction).Returns(1);
            mockRoutable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRoutable.SetupGet(r => r.DirectionsNumber).Returns(8);

            new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute();

            mockMovalble.VerifySet(m => m.Velocity = new Vector(-3, 2));
        }

        [Fact]
        public void TryChange_VelocityMustBeSet_ThrowNotDefinedPropertyException()
        {
            Mock<IMovable> mockMovalble = new Mock<IMovable>();
            Mock<IRotable> mockRoutable = new Mock<IRotable>();

            Assert.Throws<NotDefinePropertyException>(new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute);
        }

        [Fact]
        public void TryChange_ValidateDirectionNumberOrDirection_ThrowNotDefinedPropertyException()
        {
            Mock<IMovable> mockMovalble = new Mock<IMovable>();
            Mock<IRotable> mockRoutable = new Mock<IRotable>();

            mockMovalble.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));
            mockRoutable.SetupGet(r => r.DirectionsNumber).Returns(-1);

            Assert.Throws<NotDefinePropertyException>(new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute);

            mockRoutable.SetupGet(r => r.DirectionsNumber).Returns(8);
            mockRoutable.SetupGet(r => r.Direction).Returns(-1);

            Assert.Throws<NotDefinePropertyException>(new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute);

            mockRoutable.SetupGet(r => r.Direction).Returns(8);

            Assert.Throws<NotDefinePropertyException>(new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute);
        }

        [Fact]
        public void TryChange_DirectionEqualZero_Success()
        {
            Mock<IMovable> mockMovalble = new Mock<IMovable>();
            Mock<IRotable> mockRoutable = new Mock<IRotable>();

            mockMovalble.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            mockRoutable.SetupGet(r => r.Direction).Returns(0);
            mockRoutable.SetupGet(r => r.DirectionsNumber).Returns(8);

            new ChangeVelocityComamnd(mockMovalble.Object, mockRoutable.Object).Execute();

            mockMovalble.VerifySet(m => m.Velocity = new Vector(-7, 0));
        }
    }
}
