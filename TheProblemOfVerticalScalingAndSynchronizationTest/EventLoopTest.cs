using Castle.DynamicProxy.Generators;
using Moq;
using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TheProblemOfVerticalScalingAndSynchronization;
using Xunit;

namespace TheProblemOfVerticalScalingAndSynchronizationTest
{
    public class EventLoopTest
    {
        [Fact]
        public void RunThreadAndWaitWhenCommnadAddToEventLoop()
        {
            var manualResetEvent = new ManualResetEvent(false);
            var eventLoop = new EventLoop();

            var fakeCmd = new FakeCommand(() => { 
                eventLoop.SetFuncToStop(() => { return false; });
                return true;
            });

            var mockfakeCmd = new Mock<ICommand>();
            mockfakeCmd.Setup(s => s.Execute())
                .Callback(() =>
                {
                    manualResetEvent.WaitOne();
                    eventLoop.Put(fakeCmd);
                });

            eventLoop.Put(mockfakeCmd.Object);

            manualResetEvent.Set();
            eventLoop.Wait();

            Assert.True(fakeCmd.WasCalled);
            Assert.True(eventLoop.QueueIsEmpty);
        }

        [Fact]
        public void HardStopCommnadCalledAndStopEventLoop()
        {
            var manualResetEvent = new ManualResetEvent(false);
            var eventLoop = new EventLoop();

            var hardCmd = new Mock<ICommand>();
            hardCmd.Setup(s => s.Execute())
                .Callback(() =>
                {
                    manualResetEvent.WaitOne();
                    new HardStopCommand(eventLoop).Execute();
                });

            var fakeCmd = new FakeCommand();

            eventLoop.Put(hardCmd.Object);
            eventLoop.Put(fakeCmd);

            manualResetEvent.Set();
            eventLoop.Wait();

            Assert.False(fakeCmd.WasCalled);
            Assert.False(eventLoop.QueueIsEmpty);
        }

        [Fact]
        public void SoftStopCommnadCalledAndWaitWhenQueueIsEmpty()
        {
            var manualResetEvent = new ManualResetEvent(false);
            var eventLoop = new EventLoop();

            var hardCmd = new Mock<ICommand>();
            hardCmd.Setup(s => s.Execute())
                .Callback(() =>
                {
                    manualResetEvent.WaitOne();
                    new SoftStopCommand(eventLoop).Execute();
                });

            var fakeCmd = new FakeCommand();

            eventLoop.Put(hardCmd.Object);
            eventLoop.Put(fakeCmd);

            manualResetEvent.Set();
            eventLoop.Wait();

            Assert.True(fakeCmd.WasCalled);
            Assert.True(eventLoop.QueueIsEmpty);
        }
    }
}
