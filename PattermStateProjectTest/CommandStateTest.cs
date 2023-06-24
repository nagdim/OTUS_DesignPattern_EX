using ExpandableFactory.IoC;
using Moq;
using PattermStateProject;
using PattermStateProject.Commands;
using PattermStateProject.States;
using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PattermStateProjectTest
{
    public class CommandStateTest
    {
        public CommandStateTest()
        {
            IOC.Register("another_queue", new ConstantResolveDependencyStrategy(new Queue<ICommand>()));

            IOC.Register("default_state", new ConstantResolveDependencyStrategy(new DefaultCommandState()));
            IOC.Register("hard_state", new ConstantResolveDependencyStrategy(new HardStopCommandState()));
            IOC.Register("run_state", new ConstantResolveDependencyStrategy(new RunCommandState()));
            IOC.Register("move_state", new ConstantResolveDependencyStrategy(new MoveToCommandState(IOC.Resolve<Queue<ICommand>>("another_queue"))));
        }

        [Fact]
        public void DefaultState_ExecuteCommand_TurnToYourself()
        {
            ContextState context = new ContextState(IOC.Resolve<ICommandState>("default_state"));

            var mock_cmd = CreateMockCommand(context);

            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("default_state"));

            mock_cmd.Verify(s => s.Execute(), Times.Once);
        }

        [Fact]
        public void TurmToHardStopState_ReturnNullState()
        {
            ContextState context = new ContextState(IOC.Resolve<ICommandState>("default_state"))
                .Register<HardStopCommand>(IOC.Resolve<ICommandState>("hard_state"));

            context.AddCommandToQueue(new HardStopCommand());

            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("hard_state"));
            Assert.Equal(context.Run(), null);

            ContextState context1 = new ContextState(IOC.Resolve<ICommandState>("move_state"))
                .Register<HardStopCommand>(IOC.Resolve<ICommandState>("hard_state"));

            context1.AddCommandToQueue(new HardStopCommand());

            Assert.Equal(context1.Run(), IOC.Resolve<ICommandState>("hard_state"));
            Assert.Equal(context1.Run(), null);
        }

        [Fact]
        public void DefaultState_TurnToMoveToState()
        {
            ContextState context = new ContextState(IOC.Resolve<ICommandState>("default_state"))
                 .Register<MoveToCommand>(IOC.Resolve<ICommandState>("move_state"));

            context.AddCommandToQueue(new MoveToCommand());

            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("move_state"));
            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("move_state"));
        }

        [Fact]
        public void MoveToState_AddToAnotherQueue_TurnToYourself()
        {
            ContextState context = new ContextState(IOC.Resolve<ICommandState>("move_state"));

            Mock<ICommand> mock_cmd = CreateMockCommand(context);
            Queue<ICommand> commands = GetAnotherQueue();

            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("move_state"));

            Assert.True(commands.Count == 1);
            Assert.Equal(commands.Dequeue(), mock_cmd.Object);
        }

        [Fact]
        public void MoveToState_AddToAnotherQueue_TurnToRunState()
        {
            ContextState context = new ContextState(IOC.Resolve<ICommandState>("move_state"))
                .Register<RunCommand>(IOC.Resolve<ICommandState>("run_state"));

            var cmd = context.AddCommandToQueue(new RunCommand());

            Queue<ICommand> commands = GetAnotherQueue();

            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("run_state"));
            Assert.Equal(context.Run(), IOC.Resolve<ICommandState>("default_state"));

            Assert.True(commands.Count == 1);
            Assert.Equal(commands.Dequeue(), cmd);
        }

        private static Mock<ICommand> CreateMockCommand(ContextState context)
        {
            var mock_cmd = new Mock<ICommand>();
            context.AddCommandToQueue(mock_cmd.Object);
            return mock_cmd;
        }

        private static Queue<ICommand> GetAnotherQueue()
        {
            Queue<ICommand> commands = IOC.Resolve<Queue<ICommand>>("another_queue");
            commands.Clear();
            return commands;
        }
    }
}
