using SpaceShipProject.Contracts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheProblemOfVerticalScalingAndSynchronization
{
    public class HardStopCommand : ICommand
    {
        private readonly EventLoop m_eventLoop;

        public HardStopCommand(EventLoop eventLoop)
        {
            m_eventLoop = eventLoop;
        }

        public void Execute()
        {
            m_eventLoop.SetFuncToStop(
                () =>
                {
                    return false;
                });
        }
    }
}
