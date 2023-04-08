using Moq;
using SpaceShipProject;
using SpaceShipProject.Contracts;
using SpaceShipProject.Contracts.Commands;
using SpaceShipProject.Exceptions;
using SpaceShipProject.Handler;
using SpaceShipProject.Solid_Exception;
using System;
using System.Collections.Generic;
using Xunit;

namespace TestSpaceShipProject.Solid_Exception
{
    public class Solid_ExceptionTest
    {
        [Fact]
        public void TestHandler_CommandThrowException_AddCommandToQueue()
        {
            var queueCommands = new Queue<ICommand>();

            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(s => s.Execute())
                .Throws(new Exception("Sample exception."));

            var handler = CreateExceptionHandler(queueCommands)
                .Setup<Exception>(mockCommand.Object.GetType(), (cmd, ex) => cmd);

            new ExceptionCatchCommand(mockCommand.Object, handler).Execute();

            Assert.True(queueCommands.Count == 1);
            Assert.True(queueCommands.Peek() == mockCommand.Object);
        }

        [Fact]
        public void TestHandler_CommandNotNaveHandler_ThrowException()
        {
            var queueCommands = new Queue<ICommand>();

            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(s => s.Execute())
                .Throws(new NotDefinePropertyException("Sample exception."));

            var handler = CreateExceptionHandler(queueCommands);

            Assert.Throws<NotDefinePropertyException>(new ExceptionCatchCommand(mockCommand.Object, handler).Execute);
        }

        /// <summary>
        /// Реализовать Команду, которая записывает информацию о выброшенном исключении в лог.
        /// Реализовать обработчик исключения, который ставит Команду, пишущую в лог в очередь Команд.
        /// </summary>
        [Fact]
        public void TestHandler_CommandThrowException_AddLogCommandToQueueAndWriteToLog()
        {
            var queueCommands = new Queue<ICommand>();

            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(s => s.Execute())
                .Throws(new NotDefinePropertyException("Sample exception."));

            var mockLogger = new Mock<ILogger>();
            var handler = CreateExceptionHandler(queueCommands)
                .Setup<NotDefinePropertyException>(mockCommand.Object.GetType(), (cmd, ex) => new LogCommand(mockLogger.Object, ex.Message));

            new ExceptionCatchCommand(mockCommand.Object, handler).Execute();

            Assert.True(queueCommands.Count == 1);
            Assert.True(queueCommands.Peek() is LogCommand);

            while (queueCommands.Count > 0)
                queueCommands.Dequeue().Execute();

            mockLogger.Verify(s => s.Log(It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// Реализовать Команду, которая повторяет Команду, выбросившую исключение.
        /// Реализовать обработчик исключения, который ставит в очередь Команду - повторитель команды, выбросившей исключение.
        /// </summary>
        [Fact]
        public void TestHandler_SingleRepeatCommandThrowException_ThrowException()
        {
            var queueCommands = new Queue<ICommand>();

            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(s => s.Execute())
                .Throws(new NotDefinePropertyException("Sample exception."));

            var handler = CreateExceptionHandler(queueCommands)
                .Setup<NotDefinePropertyException>(mockCommand.Object.GetType(), (cmd, ex) => new FirstRepeatExecuteCommand(cmd));

            new ExceptionCatchCommand(mockCommand.Object, handler).Execute();


            Assert.Throws<NotDefinePropertyException>(queueCommands.Dequeue().Execute);

        }

        /// <summary>
        /// С помощью Команд из пункта 4 и пункта 6 реализовать следующую обработку исключений:
        /// при первом выбросе исключения повторить команду, при повторном выбросе исключения записать информацию в лог.
        /// Реализовать стратегию обработки исключения - повторить два раза, потом записать в лог. Указание: создать новую команду, точно такую же как в пункте 6. 
        /// Тип этой команды будет показывать, что Команду не удалось выполнить два раза.
        /// </summary>
        [Fact]
        public void TestHanlder_TwiceRepeatCommandThrowException_WriteExceptionToLog()
        {
            var queueCommands = new Queue<ICommand>();

            var mockCommand = new Mock<ICommand>();
            mockCommand
                .Setup(s => s.Execute())
                .Throws(new NotDefinePropertyException("Sample exception."));

            var mockLogger = new Mock<ILogger>();

            var handler = CreateExceptionHandler(queueCommands)
                .Setup<NotDefinePropertyException>(mockCommand.Object.GetType(), (cmd, ex) => new FirstRepeatExecuteCommand(cmd))
                .Setup<FirstRepeatExecuteCommand, NotDefinePropertyException>((cmd, ex) => new SecondRepeatExecuteCommand(cmd))
                .Setup<SecondRepeatExecuteCommand, NotDefinePropertyException>((cmd, ex) => new LogCommand(mockLogger.Object, ex.Message));

            new ExceptionCatchCommand(mockCommand.Object, handler).Execute();

            Assert.True(queueCommands.Count == 1);

            int variantCommand = 0;
            while (queueCommands.Count > 0)
            {
                switch (variantCommand)
                {
                    case 0:
                        Assert.True(queueCommands.Peek() is FirstRepeatExecuteCommand);
                        break;
                    case 1:
                        Assert.True(queueCommands.Peek() is SecondRepeatExecuteCommand);
                        break;
                    case 2:
                        Assert.True(queueCommands.Peek() is LogCommand);
                        break;
                    default:
                        Assert.True(variantCommand <= 2);
                        break;
                }

                ++variantCommand;
                new ExceptionCatchCommand(queueCommands.Dequeue(), handler).Execute();
            }

            mockLogger.Verify(s => s.Log(It.IsAny<string>()), Times.Once);
        }

        private static ExceptionHandler CreateExceptionHandler(Queue<ICommand> queueCommands)
        {
            var mockExceptionHandlerStrategy = new Mock<IExceptionHandlerStrategy>();

            mockExceptionHandlerStrategy
                .Setup(s => s.Handle(It.IsAny<ICommand>(), It.IsAny<Exception>()))
                .Callback((ICommand cmd, Exception ex) => queueCommands.Enqueue(cmd));

            return new ExceptionHandler(mockExceptionHandlerStrategy.Object);
        }
    }
}
