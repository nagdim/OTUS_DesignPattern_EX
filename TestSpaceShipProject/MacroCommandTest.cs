using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using System;
using Xunit;

namespace TestSpaceShipProject
{
    public class MacroCommandTest
    {

        [Fact]
        public void TryExecute_TwoCommandsWereCalled_ReturnCountCalledExecute()
        {
            Mock<ICommand> mockCommand1 = new Mock<ICommand>();
            Mock<ICommand> mockCommand2 = new Mock<ICommand>();

            int executeCmdCount = 0;

            mockCommand1.Setup(s => s.Execute()).Callback(() => { ++executeCmdCount; });
            mockCommand2.Setup(s => s.Execute()).Callback(() => { ++executeCmdCount; });

            new MacroCommand(new[] { mockCommand1.Object, mockCommand2.Object }).Execute();

            Assert.True(executeCmdCount == 2);
        }

        [Fact]
        public void TryExecute_FirstCommandWasCalledSecondCommandWasntCalled_ThrowCommandException()
        {
            Mock<ICommand> mockCommand1 = new Mock<ICommand>();
            Mock<ICommand> mockCommand2 = new Mock<ICommand>();

            int executeCmdCount = 0;

            mockCommand1.Setup(s => s.Execute()).Throws(new NotImplementedException());
            mockCommand2.Setup(s => s.Execute()).Callback(() => { ++executeCmdCount; });

            Assert.Throws<CommandException>(new MacroCommand(new[] { mockCommand1.Object, mockCommand2.Object }).Execute);
            Assert.True(executeCmdCount == 0);
        }
    }
}
