using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Contracts.Common;
using SpaceShipProject.Exceptions;
using System;
using Xunit;

namespace TestSpaceShipProject
{
    public class MoveCommandTest
    {
        /// <summary>
        /// Для объекта, находящегося в точке (12, 5) и движущегося со скоростью (-7, 3) движение меняет положение объекта на (5, 8)
        /// </summary>
        [Fact]
        public void TryMove_ChangePosition_NewPosition()
        {
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            new MoveCommand(mockMovable.Object).Execute();

            mockMovable.VerifySet(m => m.Position = new Vector(5, 8));
        }

        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_CanNotReadPosition_ThrowNotDefinePropertyException()
        {
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Throws(new NotDefinePropertyException("Position can not read "));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

           Assert.Throws<NotDefinePropertyException>(new MoveCommand(mockMovable.Object).Execute);
        }


        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_CanNotReadVelocity_ThrowNotDefinePropertyException()
        {
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupGet(m => m.Position).Returns(new Vector(12, 5));
            mockMovable.SetupGet(m => m.Velocity).Throws(new NotDefinePropertyException("Velocity can not read"));

            Assert.Throws<NotDefinePropertyException>(new MoveCommand(mockMovable.Object).Execute);
        }

        /// <summary>
        /// Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве, приводит к ошибке
        /// </summary>
        [Fact]
        public void TryMove_ObjectPositionCannotBeChanged_ThrowNotDefinePropertyException()
        {
            Mock<IMovable> mockMovable = new Mock<IMovable>();
            mockMovable.SetupSet<Vector>(m => m.Position = It.IsAny<Vector>()).Throws(new NotDefinePropertyException("Position can not be change"));
            mockMovable.SetupGet(m => m.Velocity).Returns(new Vector(-7, 3));

            Assert.Throws<NotDefinePropertyException>(new MoveCommand(mockMovable.Object).Execute);
        }
    }
}
