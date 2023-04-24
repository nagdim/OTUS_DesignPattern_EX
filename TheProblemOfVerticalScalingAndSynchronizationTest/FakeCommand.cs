using SpaceShipProject.Contracts.Commands;
using System;

namespace TheProblemOfVerticalScalingAndSynchronizationTest
{
    public class FakeCommand : ICommand
    {
        private readonly Func<bool> m_func;

        public bool WasCalled { get; private set; } = false;

        public FakeCommand(Func<bool> func)
        {
            this.m_func = func;
        }

        public FakeCommand() : this(() => { return true; })
        {

        }

        public void Execute()
        {
            WasCalled = m_func();
        }
    }
}
