using Moq;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject;
using Xunit;
using SpaceShipProject.Exceptions;

namespace TestSpaceShipProject
{
    public class RotateCommandTest
    {
        /// <summary>
        /// Объект, находящийся под углом 1 (45 градусов), совершающий поворот с угловой скоростью 2 (90 градусов за раз) направление движения изменяется на 3 (135 градусов)
        /// </summary>
        [Fact]
        public void TryRotate_ChangeDirectionV1_NewDirection()
        {
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(1);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            new RotateCommand(mockRotable.Object).Execute();

            mockRotable.VerifySet(r => r.Direction = 3);
        }

        /// <summary>
        /// Объект, находящийся под углом 7 (315 градусов), совершающий поворот с угловой скоростью 3 (135 градусов за раз) направление движения изменяется на 2 (90 градусов)
        /// </summary>
        [Fact]
        public void TryRotate_ChangeDirectionV2_NewDirection()
        {
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(7);
            mockRotable.SetupGet(r => r.AngularVelocity).Returns(3);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            new RotateCommand(mockRotable.Object).Execute();

            mockRotable.VerifySet(r => r.Direction = 2);
        }


        [Fact]
        public void TryRotate_DirectionsNumberCanNotLessOrEqualZero_ThrowNotDefinePropertyException()
        {
            Mock<IRotable> mockRotable = new Mock<IRotable>();

            mockRotable
                .SetupGet(r => r.Direction)
                .Returns(1);

            mockRotable
                .SetupGet(r => r.AngularVelocity)
                .Returns(1);

            mockRotable
                .SetupGet(r => r.DirectionsNumber)
                .Returns(0);

            Assert.Throws<NotDefinePropertyException>(new RotateCommand(mockRotable.Object).Execute);
        }

        [Fact]
        public void TryRotate_DirectionCannotBeLessZeroOrGreaterDirectionsNumber_ThrowNotDefinePropertyException()
        {
            Mock<IRotable> mockRotable = new Mock<IRotable>();

            mockRotable
                .SetupGet(r => r.DirectionsNumber)
                .Returns(8);

            mockRotable
                .SetupGet(r => r.Direction)
                .Returns(-1);

            Assert.Throws<NotDefinePropertyException>(new RotateCommand(mockRotable.Object).Execute);

            mockRotable
                .SetupGet(r => r.Direction)
                .Returns(mockRotable.Object.DirectionsNumber);

            Assert.Throws<NotDefinePropertyException>(new RotateCommand(mockRotable.Object).Execute);
        }

        /// <summary>
        /// Попытка повернуть объект, у которого невозможно прочитать количество направлений, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryRotate_AngularVelocityCanNotBeLessZeroOrGreaterDirectionNumber_ThrowNotDefinePropertyException()
        {
            Mock<IRotable> mockRotable = new Mock<IRotable>();
            mockRotable.SetupGet(r => r.Direction).Returns(2);
            mockRotable.SetupGet(r => r.DirectionsNumber).Returns(8);

            mockRotable
                .SetupGet(r => r.AngularVelocity)
                .Returns(-1);

            Assert.Throws<NotDefinePropertyException>(new RotateCommand(mockRotable.Object).Execute);

            mockRotable
                .SetupGet(r => r.AngularVelocity)
                .Returns(mockRotable.Object.DirectionsNumber);

            Assert.Throws<NotDefinePropertyException>(new RotateCommand(mockRotable.Object).Execute);
        }
    }
}
